<template>
    <page-component title="Wykaz roślin" :is-busy="isBusy" :alert="alert">
        <template #before-spinner>
            <div class="d-flex justify-content-end">
            <b-input-group class="me-2">
                <taxonomy-selector-component @item-selected="load($event?.scientificNameID)" placeholder="Wyszukaj w ewidencji..."></taxonomy-selector-component>
            </b-input-group>
            </div>
        </template>        
        <b-table-simple>
            <b-thead>
                <b-tr>
                    <b-th>Id</b-th>
                    <!--<b-th><sort-toggle-component sort-by="name" :value="sort" @toggled="onSort">Nazwa</sort-toggle-component></b-th>-->
                    <b-th>Nazwa naukowa (Autor)</b-th>
                    <b-th><sort-toggle-component sort-by="date" :value="sort" @toggled="onSort">Data dodania</sort-toggle-component></b-th>
                    <b-th>Data edycji</b-th>
                    <b-th></b-th>
                </b-tr>
            </b-thead>
            <b-tbody>
                <b-tr v-for="item in items">
                    <b-td>{{item.id}}</b-td>
                    <!--<b-td>{{item.name}}</b-td>-->
                    <b-td>{{item.scientificName}} ({{item.scientificNameAuthor}})</b-td>
                    <b-td>{{item.addDate}}</b-td>
                    <b-td>{{item.modificationDate}}</b-td>
                    <b-td class=" d-flex justify-content-end">
                        <b-button  :to = "`/Edit/${item.id}`" class="me-2 btn-sm">Edytuj</b-button>
                        <b-button @click="remove(item)" class="btn-sm">Usuń</b-button>
                    </b-td>
                </b-tr>
            </b-tbody>
        </b-table-simple>
    </page-component>
</template>
<script lang="ts">

import type { ListRegisterEntriesItem } from '../contract/ListRegisterEntriesItem';
import axios from 'axios';
import { defineComponent } from 'vue';
import PageComponent from '../components/PageComponent.vue';
import type { Alert } from '@/components/Alert';
import { remove } from '@vue/shared';
import SortToggleComponent from '../components/SortToggleComponent.vue';
import type { SortDescription } from '@/components/SortDescription';
import TaxonomySelectorComponent from '../components/TaxonomySelectorComponent.vue';

interface Data { items: ListRegisterEntriesItem[]; alert?: Alert; isBusy:boolean;
                sort?: SortDescription; }

export default defineComponent({
    data(): Data {
        return { items: [], alert: undefined, isBusy: false, sort: undefined };
    },
    methods:{
        async load(keyword?: string){
            try {
            this.alert=undefined;
            this.isBusy = true;
            var response = await axios.get<ListRegisterEntriesItem[]>("https://localhost:5001/Register/List",{
                params: {keyword, sortBy: this.sort?.by, sortDirection: this.sort?.direction}
            });
            this.items = response.data;
        }
        catch {
            this.alert = {type:"danger", text:"Wystąpił błąd"};;
        }
        finally {
            this.isBusy = false;
        }
        },
        async remove(item: ListRegisterEntriesItem){
            try {
            this.alert=undefined;
            this.isBusy = true;
            const itemIndex = this.items.indexOf(item);
            await axios.delete(`https://localhost:5001/Register/Delete?id=${item.id}`);
            this.items.splice(itemIndex, 1);
        }
        catch {
            this.alert = {type:"danger", text:"Wystąpił błąd"};;
        }
        finally {
            this.isBusy = false;
        }
        },
        async onSort(sortDescription: SortDescription|undefined){
        this.sort = sortDescription;
        await this.load();
    },
    },
    async mounted() {
        await this.load();
    },
    components: { PageComponent, SortToggleComponent, TaxonomySelectorComponent }
});

</script>