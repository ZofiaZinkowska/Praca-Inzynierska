<template>
    <simple-type-ahead ref="input" type="search" class="form-control" :items="items" :item-projection="projectItem" :min-input-length="3" @on-input="load($event.input)" @select-item="$emit('itemSelected', $event)"></simple-type-ahead>
</template>
<script lang="ts">
import { TaxonomyServiceKey } from '@/api/TaxonomyService';
import type { SearchTaxonomyItem } from '@/contract/SearchTaxonomyItem';
import { defineComponent, inject } from 'vue';
//@ts-ignore ta paczka nie udostępnia typescript typów
import simpleTypeAhead from 'vue3-simple-typeahead';
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
    setup() {
        const taxonomyService = inject(TaxonomyServiceKey)!;
        return {taxonomyService};
    },
    expose: ['selectItem'],
    methods: {
        projectItem(item?: SearchTaxonomyItem) {
            if(!item)
                return'';
            return `${item.scientificName} (${item.scientificNameAuthor})`;
        },

        selectItem(item?: SearchTaxonomyItem) {
            this.items=[];
            const input = this.$refs.input as simpleTypeAhead;
            input.selectItem(item);
        },

        async load(keyword: string) {
            if (keyword.length === 0){
                this.selectItem(undefined);
            }
            if (keyword.length < 3)
                return;
            this.items = await this.taxonomyService.search(keyword, 8);
        },
    },
    data(): Data {
        return { items: [] };
    }
});
</script>