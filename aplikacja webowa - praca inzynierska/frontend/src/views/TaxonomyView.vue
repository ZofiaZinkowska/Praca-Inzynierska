<template>
    <page-component title="Klasyfikacja" :is-busy="isBusy">
      <template #before-spinner>
            <div class="d-flex justify-content-end">
            <b-input-group class="me-2">
                <taxonomy-selector-component @item-selected="" placeholder="Wyszukaj w klasyfikacji..."></taxonomy-selector-component>
            </b-input-group>
            </div>
        </template>   
    </page-component>
 </template>
 <script lang="ts">
 import PageComponent from "../components/PageComponent.vue";
 import { defineComponent } from 'vue';
 import TaxonomySelectorComponent from '../components/TaxonomySelectorComponent.vue';

 interface Data {isBusy:boolean;}

 export default defineComponent({
    data(): Data {
        return {isBusy: false};
    },
    components: { PageComponent,TaxonomySelectorComponent }
});
 </script>
 