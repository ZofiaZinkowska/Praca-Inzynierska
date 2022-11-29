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
                            <taxonomy-selector-component ref="taxonomyInput" @item-selected="load($event)"
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
                    <b-button v-print="'#print-label'" variant="success" class="ms-auto print-button">Drukuj etykietę
                    </b-button>
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
                        <qrcode-vue :value="details.id" level="L" :size="200"></qrcode-vue>
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

@media print {
    .print-button {
        display: none;
    }

    #print-label {
        height: 40mm;
        width: 80mm;
        margin: 1mm;
        overflow: hidden;
    }

    #print-label>* {
        zoom: 30%;
    }
}
</style>
<script lang="ts">
import { TaxonomyServiceKey } from '@/api/TaxonomyService';
import PageComponent from "../components/PageComponent.vue";
import { defineComponent, inject, type Directive } from 'vue';
import TaxonomySelectorComponent from '../components/TaxonomySelectorComponent.vue';
import type { TaxonomyItemDetails } from '../contract/TaxonomyItemDetails';
import type { SearchTaxonomyItem } from '../contract/SearchTaxonomyItem';
import SpinnerComponent from '../components/SpinnerComponent.vue';
import QrcodeVue from 'qrcode.vue';
//@ts-ignore
import print from 'vue3-print-nb';
import { useRoute } from "vue-router";

interface Data {
    isBusy: boolean;
    details?: TaxonomyItemDetails;
    currentCode?: string;
    isFinding: boolean;
    lastSearchedCode?: string;
    lastFoundMatch?: SearchTaxonomyItem;
}

interface MatchedSearchTaxonomyItem extends SearchTaxonomyItem {
    matchedCode?: string;
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
    setup() {
        const taxonomyService = inject(TaxonomyServiceKey)!;
        return {taxonomyService};
    },
    computed: {
        isCodeUnmapped() {
            return !this.lastFoundMatch && !!this.lastSearchedCode;
        },
    },
    mounted() {
        const route = useRoute();
        const id = route.params.id as string;
        if (!!id){
            this.find(id);
        } else {
            const input = this.$refs.codeInput as HTMLInputElement;
            input.focus();
        }
    },
    methods: {
        async load(item?: MatchedSearchTaxonomyItem) {
            if (item?.matchedCode !== this.currentCode) {
                this.currentCode = undefined;
            }
            this.$router.replace({name:'Taxonomy', params:{id: this.currentCode}});
            const id = item?.taxonomyID;
            if (!id) {
                this.details = undefined;
                return;
            }
            try {
                this.isBusy = true;
                this.details = await this.taxonomyService.details(id);
            }
            finally {
                this.isBusy = false;
            }
        },
        async find(code?: string) {
            this.lastSearchedCode = code;
            this.$router.replace({name:'Taxonomy', params:{id: code}});
            if (!code)
                return;
            try {
                this.isFinding = true;
                this.lastFoundMatch = undefined;
                const items = await this.taxonomyService.find(code);
                const input = this.$refs.taxonomyInput as any;
                if (items.length === 0) {
                    input.selectItem(undefined);
                    return;
                }
                const matchedItem: MatchedSearchTaxonomyItem = items[0];
                matchedItem.matchedCode = code;
                input.selectItem(matchedItem);
                this.lastFoundMatch = matchedItem;
            }
            finally {
                this.isFinding = false;
            }
        }
    },
    components: { PageComponent, TaxonomySelectorComponent, SpinnerComponent, QrcodeVue },
    directives: { print: print as Directive }
});
</script>
 