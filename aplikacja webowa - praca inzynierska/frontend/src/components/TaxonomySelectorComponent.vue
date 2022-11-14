<template>
    <simple-type-ahead class="form-control" :items="items" :item-projection="projectItem" :min-input-length="3" @on-input="load($event.input)" @select-item="$emit('itemSelected', $event)"></simple-type-ahead>
</template>
<script lang="ts">
import type { SearchTaxonomyItem } from '@/contract/SearchTaxonomyItem';
import { defineComponent } from 'vue';
//@ts-ignore ta paczka nie udostępnia typescript typów
import simpleTypeAhead from 'vue3-simple-typeahead';
import axios from 'axios';
import 'vue3-simple-typeahead/dist/vue3-simple-typeahead.css';

interface Data { items: SearchTaxonomyItem[] }
export default defineComponent({
    components: {
        simpleTypeAhead
    },
    emits: {
        itemSelected(item?: SearchTaxonomyItem) {
            return true;
        }
    },
    methods: {
        projectItem(item: SearchTaxonomyItem) {
            return `${item.scientificName} (${item.scientificNameAuthor})`;
        },

        async load(keyword: string) {
            if (keyword.length === 0){
                this.items=[];
                this.$emit('itemSelected', undefined);
            }
            if (keyword.length < 3)
                return;
            const params = {
                keyword, count: 8
            };
            var response = await axios.get<SearchTaxonomyItem[]>("https://localhost:5001/Taxonomy/Search",{
                params
            });
            this.items = response.data;
        },
    },
    data(): Data {
        return { items: [] };
    }
});
</script>