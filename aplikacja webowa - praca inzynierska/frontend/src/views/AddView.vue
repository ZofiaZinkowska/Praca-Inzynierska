<template>
    <page-component title="Dodawanie nowego wpisu" :alert="alert">
        <div class="mt-4">
            <b-form-group label="Zeskanuj kod rośliny">
                <b-input-group>
                    <b-form-input v-model="currentCode" ref="codeInput" type="search" placeholder="Zeskanuj kod rośliny..."
                        @search="find(currentCode)"></b-form-input>
                    <b-button variant="success" @click="find(currentCode)"><font-awesome-icon icon="search"></font-awesome-icon></b-button>
                </b-input-group>
                <span v-if="isCodeUnmapped" class="form-text text-warning">&#9432; Do kodu '{{lastSearchedCode}}' nie znaleziono powiązanej rośliny</span>
            </b-form-group>
            <spinner-component :is-visible="isFinding">
                <b-form-group label="Klasyfikacja roślin">
                    <taxonomy-selector-component ref="taxonomyInput" @item-selected="taxonomyItem = $event"
                        placeholder="Wyszukaj pozycję w klasyfikacji..."></taxonomy-selector-component>
                </b-form-group>
            </spinner-component>
            <div class=" d-flex justify-content-end align-items-center">
                <b-form-text class="flex-grow-1" v-if="!!saveInfoText">&#9432; {{saveInfoText}}</b-form-text>
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

interface Data { 
    currentCode?: string;
    isFinding: boolean; 
    lastSearchedCode?: string; 
    taxonomyItem?: SearchTaxonomyItem; 
    alert?: Alert; 
    isSaving: boolean; 
    lastFoundMatch?: SearchTaxonomyItem;
}

export default defineComponent({
    data(): Data {
        return { 
            currentCode: undefined,
            isFinding: false, 
            lastSearchedCode: undefined, 
            taxonomyItem: undefined, 
            alert: undefined, 
            isSaving: false,
            lastFoundMatch: undefined, 
        };
    },
    mounted() {
        const input = this.$refs.codeInput as HTMLInputElement;
        input.focus();
    },
    computed: {
        isValid() {
            return !!this.taxonomyItem;
        },
        isCodeUnmapped() {
            return !this.lastFoundMatch && !!this.lastSearchedCode;
        },
        saveInfoText() {
            if (!this.lastSearchedCode || !this.taxonomyItem){
                return undefined;
            }
            if (!this.lastFoundMatch){
                return `Kod '${this.lastSearchedCode}' zostanie powiązany z wybraną rośliną`;
            }
            if (this.taxonomyItem.taxonomyID !== this.lastFoundMatch.taxonomyID){
                return `Istniejące powiązanie kodu '${this.lastSearchedCode}' zostanie połączone z nową rośliną`;
            }
            if (this.taxonomyItem.taxonomyID === this.lastFoundMatch.taxonomyID){
                return `Kod '${this.lastSearchedCode}' jest już powiązany z wybraną rośliną`;
            }
        },
    },
    methods: {
        async save() {
            try {
                this.alert = undefined;
                this.isSaving = true;
                const request: SaveRegisterEntryRequest = { taxonomyID: this.taxonomyItem!.taxonomyID };
                await axios.put("https://localhost:5001/Register/Add", request);
                if (!!this.lastSearchedCode) {
                    const codeRequest: SaveTaxonomyCodeRequest = { taxonomyID: this.taxonomyItem!.taxonomyID, code: this.lastSearchedCode };
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

        async find(code?: string) {
            this.lastSearchedCode = code;
            if (!code)
                return;
            try {
                this.alert = undefined;
                this.isFinding = true;
                this.lastFoundMatch = undefined;
                const response = await axios.get<SearchTaxonomyItem[]>("https://localhost:5001/Taxonomy/Find", { params: { code } });
                const items = response.data;
                const input = this.$refs.taxonomyInput as any;
                if (items.length === 0) {
                    input.selectItem(undefined);
                    return;
                }
                input.selectItem(items[0]);
                this.lastFoundMatch = items[0];
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