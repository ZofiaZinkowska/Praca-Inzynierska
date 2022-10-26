<template>
    <page-component title="Ewidencja" :is-busy="isBusy" :alert="alert">
        <div class="d-flex justify-content-end">
            <b-input-group class="me-2">
                <b-form-input type="search" v-model="keyword" @search="load"></b-form-input>
                <b-button type="button" class="btn btn-primary" variant="success" @click="load">
                    <font-awesome-icon icon="fa-search"></font-awesome-icon>
                </b-button>
            </b-input-group>
            <b-button to = "/Add" variant="success">Dodaj</b-button>

        </div>
        
        <b-table-simple>
            <b-thead>
                <b-tr>
                    <b-th>Id</b-th>
                    <b-th>Nazwa</b-th>
                    <b-th>Data dodania</b-th>
                    <b-th>Data edycji</b-th>
                    <b-th></b-th>
                </b-tr>
            </b-thead>
            <b-tbody>
                <b-tr v-for="item in items">
                    <b-td>{{item.id}}</b-td>
                    <b-td>{{item.name}}</b-td>
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

import type { RegisterEntry } from '../contract/RegisterEntry';
import axios from 'axios';
import { defineComponent } from 'vue';
import PageComponent from '../components/PageComponent.vue';
import type { Alert } from '@/components/Alert';
import { remove } from '@vue/shared';

interface Data { items: RegisterEntry[]; alert?: Alert; isBusy:boolean;
                keyword?:string; }

export default defineComponent({
    data(): Data {
        return { items: [], alert: undefined, isBusy: false, keyword: undefined };
    },
    methods:{
        async load(){
            try {
            this.alert=undefined;
            this.isBusy = true;
            var response = await axios.get<RegisterEntry[]>("https://localhost:5001/Register/List",{
                params: {keyword: this.keyword}
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
        async remove(item: RegisterEntry){
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
        }
    },
    async mounted() {
        await this.load();
    },
    components: { PageComponent }
});

</script>