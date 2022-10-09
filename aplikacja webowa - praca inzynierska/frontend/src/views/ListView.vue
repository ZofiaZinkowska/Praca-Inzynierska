<template>
    <b-alert class="alert-danger my-3 shadow-sm" dismissible="true" v-bind:show="hasError">Wystąpił błąd</b-alert>
    <b-container class="shadow-sm rounded my-3 p-4 bg-body">
        <h4 class="border-bottom pb-2">Ewidencja</h4>
        <div class="d-flex justify-content-center mt-5 mb-3" v-if="isLoading">
            <b-spinner></b-spinner>
        </div>
        <b-table-simple v-else>
            <b-thead>
                <b-tr>
                    <b-th>Id</b-th>
                    <b-th>Nazwa</b-th>
                    <b-th>Data dodania</b-th>
                    <b-th>Edycja</b-th>
                    <b-th>Data edycji</b-th>
                </b-tr>
            </b-thead>
            <b-tbody>
                <b-tr v-for="item in items">
                    <b-td>{{item.id}}</b-td>
                    <b-td>{{item.name}}</b-td>
                    <b-td>{{item.addDate}}</b-td>
                    <b-td>
                        <router-link :to = "`/Edit/${item.id}`">Edytuj</router-link>
                    </b-td>
                    <b-td>{{item.modificationDate}}</b-td>
                </b-tr>
            </b-tbody>
        </b-table-simple>
    </b-container>
</template>
<script lang="ts">

import type { RegisterEntry } from '../contract/RegisterEntry';
import axios from 'axios';
import { defineComponent } from 'vue';

interface Data { items: RegisterEntry[]; hasError:boolean; isLoading:boolean}

export default defineComponent({
    data(): Data {
        return { items: [], hasError: false, isLoading: false };
    },
    async mounted(){
        try{
            this.isLoading = true;
            var response = await axios.get<RegisterEntry[]>('https://localhost:5001/Register/List');
            this.items = response.data;
        }
        catch{
            this.hasError = true;
        }
        finally{
            this.isLoading = false;
        }
    }
});

</script>