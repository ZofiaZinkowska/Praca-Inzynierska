<template>
    <page-component title="Wykaz roślin" :is-busy="isBusy" :alert="alert">
        <template #actions>
            <b-button variant="success" v-print="'#print-list'">Drukuj listę</b-button>
        </template>
        <template #before-spinner>
            <div class="d-flex justify-content-end">
                <taxonomy-selector-component @item-selected="load($event?.taxonomyID)"
                    placeholder="Wyszukaj w ewidencji..."></taxonomy-selector-component>
            </div>
        </template>
        <b-table-simple id="print-list">
            <b-thead>
                <b-tr>
                    <b-th>Id</b-th>
                    <b-th>
                        <sort-toggle-component sort-by="name" :value="sort" @toggled="onSort">Nazwa naukowa (Autor)
                        </sort-toggle-component>
                    </b-th>
                    <b-th>
                        <sort-toggle-component sort-by="date" :value="sort" @toggled="onSort">Data dodania
                        </sort-toggle-component>
                    </b-th>
                    <b-th class="hide-in-print"></b-th>
                </b-tr>
            </b-thead>
            <b-tbody>
                <b-tr v-for="item in items">
                    <b-td>{{ item.id }}</b-td>
                    <b-td>{{ item.scientificName }} ({{ item.scientificNameAuthor }})</b-td>
                    <b-td :title="formatDateTime(item.addDate)">{{ formatDate(item.addDate) }}</b-td>   
                    <b-td class="hide-in-print">
                        <div class="d-flex justify-content-end">
                            <b-button variant="danger" @click="remove(item)" class="btn-sm">Usuń</b-button>
                        </div>
                    </b-td>
                </b-tr>
            </b-tbody>
        </b-table-simple>
    </page-component>
</template>
<style scoped>
@media print {
    .hide-in-print {
        display: none !important;
    }
}
</style>
<script lang="ts">

import type { ListRegisterEntriesItem } from '../contract/ListRegisterEntriesItem';
import axios from 'axios';
import { defineComponent, type Directive } from 'vue';
import PageComponent from '../components/PageComponent.vue';
import type { Alert } from '@/components/Alert';
import { remove } from '@vue/shared';
import SortToggleComponent from '../components/SortToggleComponent.vue';
import type { SortDescription } from '@/components/SortDescription';
import TaxonomySelectorComponent from '../components/TaxonomySelectorComponent.vue';
import moment from 'moment';
//@ts-ignore
import print from 'vue3-print-nb';

interface Data {
    items: ListRegisterEntriesItem[]; alert?: Alert; isBusy: boolean;
    sort?: SortDescription;
}

export default defineComponent({
    data(): Data {
        return { items: [], alert: undefined, isBusy: false, sort: undefined };
    },
    methods: {
        formatDate(date: string) {
            return moment(date).format('DD.MM.yyyy');
        },
        formatDateTime(dateTime: string) {
            return moment(dateTime).format('DD.MM.yyyy HH:mm:ss')
        },
        async load(keyword?: string) {
            try {
                this.alert = undefined;
                this.isBusy = true;
                var response = await axios.get<ListRegisterEntriesItem[]>("https://localhost:5001/Register/List", {
                    params: { keyword, sortBy: this.sort?.by, sortDirection: this.sort?.direction }
                });
                this.items = response.data;
            }
            catch {
                this.alert = { type: "danger", text: "Wystąpił błąd" };;
            }
            finally {
                this.isBusy = false;
            }
        },
        async remove(item: ListRegisterEntriesItem) {
            try {
                this.alert = undefined;
                this.isBusy = true;
                const itemIndex = this.items.indexOf(item);
                await axios.delete(`https://localhost:5001/Register/Delete?id=${item.id}`);
                this.items.splice(itemIndex, 1);
            }
            catch {
                this.alert = { type: "danger", text: "Wystąpił błąd" };;
            }
            finally {
                this.isBusy = false;
            }
        },
        async onSort(sortDescription: SortDescription | undefined) {
            this.sort = sortDescription;
            await this.load();
        },
    },
    async mounted() {
        await this.load();
    },
    components: { PageComponent, SortToggleComponent, TaxonomySelectorComponent },
    directives: { print: print as Directive }
});

</script>