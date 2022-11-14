<template>
    <page-component title="Dodawanie nowego wpisu" :is-busy="isLoading" :alert="alert">
        <div class="mt-4">
            <b-form-group label="Klasyfikacja roślin" label-for="name">
                <taxonomy-selector-component @item-selected="taxonomyItem=$event" placeholder="Wyszukaj pozycję w klasyfikacji..."></taxonomy-selector-component>
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

interface Data {taxonomyItem?: SearchTaxonomyItem; alert?: Alert; isLoading: boolean; isSaving:boolean; }

export default defineComponent({
    data(): Data {
        return { taxonomyItem: undefined, alert: undefined, isLoading: false, isSaving: false };
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
                const request: SaveRegisterEntryRequest={scientificNameID: this.taxonomyItem!.scientificNameID};
                await axios.put("https://localhost:5001/Register/Add", request);
                this.$router.push({ name: "List" });
            }
            catch {
                this.alert = {type:"danger", text:"Wystąpił błąd"};
            }
            finally {
                this.isSaving = false;
            }
        }
    },
    components: { PageComponent, TaxonomySelectorComponent }
});
</script>