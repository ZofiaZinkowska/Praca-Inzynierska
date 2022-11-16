<template>
    <page-component title="Klasyfikacja" :is-busy="isBusy">
        <template #before-spinner>
            <div class="d-flex justify-content-end">
                <b-input-group class="me-2">
                    <taxonomy-selector-component @item-selected="load($event?.taxonomyID)"
                        placeholder="Wyszukaj w klasyfikacji...">
                    </taxonomy-selector-component>
                </b-input-group>
            </div>
        </template>
        <div v-if="!isBusy && !!details" class="mt-4">
            <h5>{{details.scientificName}}</h5>
            <b-table-simple class="w-auto details-table">
                <b-tr>
                    <b-th>Autor:</b-th>
                    <b-td>{{details.scientificNameAuthor}}</b-td>
                </b-tr>
                <b-tr>
                    <b-th>Opublikowano w:</b-th>
                    <b-td>{{details.namePublishedIn}}</b-td>
                </b-tr>
                <b-tr>
                    <b-th>Status:</b-th>
                    <b-td>{{details.taxonomicStatus}}</b-td>
                </b-tr>
                <b-tr>
                    <b-th>Rodzina:</b-th>
                    <b-td>{{details.family}}</b-td>
                </b-tr>
                <b-tr>
                    <b-th>Podrodzina:</b-th>
                    <b-td>{{details.subfamily}}</b-td>
                </b-tr>
                <b-tr>
                    <b-th>Gatunek:</b-th>
                    <b-td>{{details.genus}}</b-td>
                </b-tr>
                <b-tr>
                    <b-th>Epitet gatunkowy:</b-th>
                    <b-td>{{details.specificEpithet}}</b-td>
                </b-tr>
            </b-table-simple>
        </div>
    </page-component>
</template>
<style scoped>
.details-table th, .details-table td{
    padding: 6px;
}
</style>
<script lang="ts">
import PageComponent from "../components/PageComponent.vue";
import { defineComponent } from 'vue';
import TaxonomySelectorComponent from '../components/TaxonomySelectorComponent.vue';
import type { TaxonomyItemDetails } from '../contract/TaxonomyItemDetails';
import axios from 'axios';

interface Data { isBusy: boolean; details?: TaxonomyItemDetails }

export default defineComponent({
    data(): Data {
        return { isBusy: false, details: undefined };
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
            finally{
                this.isBusy = false;
            }
        }
    },
    components: { PageComponent, TaxonomySelectorComponent }
});
</script>
 