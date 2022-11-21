<template>
    <page-component title="Klasyfikacja" :is-busy="isBusy">
        <template #before-spinner>
            <div class="mt-4">
                <b-form-group label="Zeskanuj kod rośliny">
                    <b-input-group>
                        <b-form-input v-model="currentCode" ref="codeInput" type="search"
                            placeholder="Zeskanuj kod rośliny..." @search="find(currentCode)"></b-form-input>
                        <b-button variant="success" @click="find(currentCode)">
                            <font-awesome-icon icon="search"></font-awesome-icon>
                        </b-button>
                    </b-input-group>
                    <span v-if="isCodeUnmapped" class="form-text text-warning">&#9432; Do kodu '{{ lastSearchedCode }}'
                        nie znaleziono powiązanej rośliny</span>
                </b-form-group>
                <spinner-component :is-visible="isFinding">
                    <b-form-group label="Wyszukaj roślinę w klasyfikacji">
                        <b-input-group class="me-2">
                            <taxonomy-selector-component ref="taxonomyInput" @item-selected="load($event?.taxonomyID)"
                                placeholder="Wyszukaj w klasyfikacji...">
                            </taxonomy-selector-component>
                        </b-input-group>
                    </b-form-group>
                </spinner-component>
            </div>
        </template>
        <div v-if="!isBusy && !!details" class="mt-4 border-top pt-4">
            <div id="print-label">
                <div class="d-flex align-items-center">
                    <h5 class="mb-0 text-primary">{{ details.scientificName }}</h5>
                    <b-button v-print="'#print-label'" variant="success" class="ms-auto print-button">Drukuj etykietę</b-button>
                </div>
                <div class="d-flex align-items-start mt-2">
                    <b-table-simple class="w-auto details-table">
                        <b-tr>
                            <b-th>Autor:</b-th>
                            <b-td>{{ details.scientificNameAuthor }}</b-td>
                        </b-tr>
                        <b-tr>
                            <b-th>Opublikowano w:</b-th>
                            <b-td>{{ details.namePublishedIn }}</b-td>
                        </b-tr>
                        <b-tr>
                            <b-th>Status:</b-th>
                            <b-td>{{ details.taxonomicStatus }}</b-td>
                        </b-tr>
                        <b-tr>
                            <b-th>Rodzina:</b-th>
                            <b-td>{{ details.family }}</b-td>
                        </b-tr>
                        <b-tr>
                            <b-th>Podrodzina:</b-th>
                            <b-td>{{ details.subfamily }}</b-td>
                        </b-tr>
                        <b-tr>
                            <b-th>Gatunek:</b-th>
                            <b-td>{{ details.genus }}</b-td>
                        </b-tr>
                        <b-tr>
                            <b-th>Epitet gatunkowy:</b-th>
                            <b-td>{{ details.specificEpithet }}</b-td>
                        </b-tr>
                    </b-table-simple>
                    <div class="ms-auto mt-auto mb-auto">
                        <qrcode-vue :value="details.id" level="H" :size="200"></qrcode-vue>
                    </div>
                </div>
            </div>
        </div>
    </page-component>
</template>
<style scoped>
.details-table th,
.details-table td {
    padding: 6px;
}
@media print{
    .print-button{
        display: none;
    }
    #print-label{
        height: 40mm;
        width: 80mm;
        margin: 1mm;
        overflow: hidden;
    }
    #print-label >*{
        zoom: 30%;
    }   
}
</style>
<script lang="ts">
import PageComponent from "../components/PageComponent.vue";
import { defineComponent } from 'vue';
import TaxonomySelectorComponent from '../components/TaxonomySelectorComponent.vue';
import type { TaxonomyItemDetails } from '../contract/TaxonomyItemDetails';
import axios from 'axios';
import type { SearchTaxonomyItem } from '../contract/SearchTaxonomyItem';
import SpinnerComponent from '../components/SpinnerComponent.vue';
import QrcodeVue from 'qrcode.vue';
//@ts-ignore
import print from 'vue3-print-nb';

interface Data {
    isBusy: boolean;
    details?: TaxonomyItemDetails;
    currentCode?: string;
    isFinding: boolean;
    lastSearchedCode?: string;
    lastFoundMatch?: SearchTaxonomyItem;
}

export default defineComponent({
    data(): Data {
        return {
            isBusy: false,
            details: undefined,
            currentCode: undefined,
            isFinding: false,
            lastSearchedCode: undefined,
            lastFoundMatch: undefined,
        };
    },
    computed: {
        isCodeUnmapped() {
            return !this.lastFoundMatch && !!this.lastSearchedCode;
        },
    },
    methods: {
        async load(id?: string) {
            if (!id) {
                this.details = undefined;
                return;
            }
            try {
                this.isBusy = true;
                var response = await axios.get<TaxonomyItemDetails>("https://localhost:5001/Taxonomy/Details", {
                    params: { id }
                });
                this.details = response.data;
            }
            finally {
                this.isBusy = false;
            }
        },
        async find(code?: string) {
            this.lastSearchedCode = code;
            if (!code)
                return;
            try {
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
            finally {
                this.isFinding = false;
            }
        }
    },
    components: { PageComponent, TaxonomySelectorComponent, SpinnerComponent, QrcodeVue },
    directives: { print }
});
</script>
 