<template>
    <page-component title="Ewidencja" :is-busy="isLoading">
        <b-table-simple>
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
    </page-component>
</template>
<script lang="ts">

import type { RegisterEntry } from '../contract/RegisterEntry';
import axios from 'axios';
import { defineComponent } from 'vue';
import PageComponent from '../components/PageComponent.vue';

interface Data { items: RegisterEntry[]; hasError:boolean; isLoading:boolean}

export default defineComponent({
    data(): Data {
        return { items: [], hasError: false, isLoading: false };
    },
    async mounted() {
        try {
            this.isLoading = true;
            var response = await axios.get<RegisterEntry[]>("https://localhost:5001/Register/List");
            this.items = response.data;
        }
        catch {
            this.hasError = true;
        }
        finally {
            this.isLoading = false;
        }
    },
    components: { PageComponent }
});

</script>