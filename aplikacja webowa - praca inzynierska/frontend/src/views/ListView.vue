<template>
    <b-container class="shadow-sm rounded my-3 p-4 bg-body">
        <h4 class="border-bottom pb-2">Moje roślinki</h4>
        <b-table-simple>
            <b-thead>
                <b-tr>
                    <b-th>Id</b-th>
                    <b-th>Nazwa</b-th>
                    <b-th>Data dodania</b-th>
                </b-tr>
            </b-thead>
            <b-tbody>
                <b-tr v-for="item in items">
                    <b-td>{{item.id}}</b-td>
                    <b-td>{{item.name}}</b-td>
                    <b-td>{{item.addDate}}</b-td>
                </b-tr>
            </b-tbody>
        </b-table-simple>
    </b-container>
</template>
<script lang="ts">

import type { RegisterEntry } from '../contract/RegisterEntry';
import axios from 'axios';
import { defineComponent } from 'vue';

interface Data { items: RegisterEntry[] }

export default defineComponent({
    data(): Data {
        return { items: [] };
    },
    async mounted(){
        var response = await axios.get<RegisterEntry[]>('https://localhost:5001/Register/List');
        this.items= response.data;
    }
});

</script>