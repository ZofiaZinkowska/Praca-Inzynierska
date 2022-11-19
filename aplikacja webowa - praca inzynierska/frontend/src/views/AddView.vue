<template>
    <page-component title="Dodawanie nowego wpisu" :alert="alert">
        <div class="mt-4">
            <b-form-group label="Zeskanuj kod rośliny">
               <b-form-input ref="codeInput" type="search" placeholder="Zeskanuj kod rośliny..." @search="find($event.target.value)"></b-form-input>
            </b-form-group>
            <b-form-group label="Klasyfikacja roślin">
                <taxonomy-selector-component ref="taxonomyInput" @item-selected="taxonomyItem=$event" placeholder="Wyszukaj pozycję w klasyfikacji..."></taxonomy-selector-component>
            </b-form-group>
            <div class=" d-flex justify-content-end">
                <b-button :disabled="isSaving" @click="$router.back()" variant="secondary" class="me-2">Anuluj</b-button>
                <b-button :loading="isSaving" :disabled="isSaving || !isValid" @click="save()" variant="success">Zapisz</b-button>
            </div>
        </div>
    </page-component>
</template>
<script lang="ts">
import axios from 'axios';
import type {SaveRegisterEntryRequest} from '../contract/SaveRegisterEntryRequest';
import { defineComponent } from 'vue';
import PageComponent from '../components/PageComponent.vue';
import type { Alert } from '@/components/Alert';
import TaxonomySelectorComponent from '../components/TaxonomySelectorComponent.vue';
import type { SearchTaxonomyItem } from '@/contract/SearchTaxonomyItem';

interface Data {taxonomyItem?: SearchTaxonomyItem; alert?: Alert; isSaving:boolean; }

export default defineComponent({
    data(): Data {
        return { taxonomyItem: undefined, alert: undefined, isSaving: false };
    },
    mounted() {
        const input = this.$refs.codeInput as HTMLInputElement;
        input.focus();
    },
    computed: {
        isValid() {
            return !! this.taxonomyItem;
        }
    },
    methods: {
        async save() {
            try {
                this.alert=undefined;
                this.isSaving = true;
                const request: SaveRegisterEntryRequest={taxonomyID: this.taxonomyItem!.taxonomyID};
                await axios.put("https://localhost:5001/Register/Add", request);
                this.$router.push({ name: "List" });
            }
            catch {
                this.alert = {type:"danger", text:"Wystąpił błąd"};
            }
            finally {
                this.isSaving = false;
            }
        },

        async find(code?:string){
            if (!code)
                return;
            try{
                this.alert = undefined;
                const response = await axios.get<SearchTaxonomyItem[]>("https://localhost:5001/Taxonomy/Find", {params:{code}});
                const items = response.data;
                if (items.length === 0) {
                    return;
                }
                const input = this.$refs.taxonomyInput as any;
                input.selectItem(items[0]);
            }
            catch {
                this.alert = {type:"danger", text:"Wystąpił błąd"};
            }
        }
    },
    components: { PageComponent, TaxonomySelectorComponent }
});
</script>