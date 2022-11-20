<template>
    <page-component title="Dodawanie nowego wpisu" :alert="alert">
        <div class="mt-4">
            <b-form-group label="Zeskanuj kod rośliny">
                <b-form-input v-model="code" ref="codeInput" type="search" placeholder="Zeskanuj kod rośliny..."
                    @search="find()"></b-form-input>
            </b-form-group>
            <spinner-component :is-visible="isFinding">
                <b-form-group label="Klasyfikacja roślin">
                    <taxonomy-selector-component ref="taxonomyInput" @item-selected="taxonomyItem = $event"
                        placeholder="Wyszukaj pozycję w klasyfikacji..."></taxonomy-selector-component>
                </b-form-group>
            </spinner-component>
            <div class=" d-flex justify-content-end">
                <span v-if="!!code && !!taxonomyItem">Kod zostanie powiązany z wybraną rośliną</span>
                <b-button :disabled="isSaving" @click="$router.back()" variant="secondary" class="me-2">Anuluj
                </b-button>
                <b-button :loading="isSaving" :disabled="isSaving || !isValid" @click="save()" variant="success">Zapisz
                </b-button>
            </div>
        </div>
    </page-component>
</template>
<script lang="ts">
import axios from 'axios';
import type { SaveRegisterEntryRequest } from '../contract/SaveRegisterEntryRequest';
import { defineComponent } from 'vue';
import PageComponent from '../components/PageComponent.vue';
import type { Alert } from '@/components/Alert';
import TaxonomySelectorComponent from '../components/TaxonomySelectorComponent.vue';
import type { SearchTaxonomyItem } from '@/contract/SearchTaxonomyItem';
import type { SaveTaxonomyCodeRequest } from '@/contract/SaveTaxonomyCodeRequest';
import SpinnerComponent from '../components/SpinnerComponent.vue';

interface Data { isFinding: boolean; code?: string; taxonomyItem?: SearchTaxonomyItem; alert?: Alert; isSaving: boolean; }

export default defineComponent({
    data(): Data {
        return { isFinding: false, code: undefined, taxonomyItem: undefined, alert: undefined, isSaving: false };
    },
    mounted() {
        const input = this.$refs.codeInput as HTMLInputElement;
        input.focus();
    },
    computed: {
        isValid() {
            return !!this.taxonomyItem;
        }
    },
    methods: {
        async save() {
            try {
                this.alert = undefined;
                this.isSaving = true;
                const request: SaveRegisterEntryRequest = { taxonomyID: this.taxonomyItem!.taxonomyID };
                await axios.put("https://localhost:5001/Register/Add", request);
                if (!!this.code) {
                    const codeRequest: SaveTaxonomyCodeRequest = { taxonomyID: this.taxonomyItem!.taxonomyID, code: this.code };
                    await axios.put("https://localhost:5001/Taxonomy/AddCode", codeRequest);
                }
                this.$router.push({ name: "List" });
            }
            catch {
                this.alert = { type: "danger", text: "Wystąpił błąd" };
            }
            finally {
                this.isSaving = false;
            }
        },

        async find() {
            if (!this.code)
                return;
            try {
                this.alert = undefined;
                this.isFinding = true;
                const response = await axios.get<SearchTaxonomyItem[]>("https://localhost:5001/Taxonomy/Find", { params: { code: this.code } });
                const items = response.data;
                if (items.length === 0) {
                    return;
                }
                const input = this.$refs.taxonomyInput as any;
                input.selectItem(items[0]);
            }
            catch {
                this.alert = { type: "danger", text: "Wystąpił błąd" };
            }
            finally {
                this.isFinding = false;
            }
        }
    },
    components: { PageComponent, TaxonomySelectorComponent, SpinnerComponent }
});
</script>