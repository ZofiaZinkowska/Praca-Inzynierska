<script lang="ts">
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { defineComponent, type PropType } from 'vue';
import type {SortBy, SortDescription} from './SortDescription';
export default defineComponent({
    props: {
        sortBy: String as PropType<SortBy>,
            value: Object as PropType<SortDescription>,
    },
    computed: {
        icon(){
            return this.forValue({
                asc: 'fa-sort-asc',
                desc: 'fa-sort-desc',
                none: 'fa-sort',
            });
        },
        isAlwaysVisible(){
            return this.forValue({
                asc: true,
                desc: true,
                none: false,
            });
        }
    },
    emits: {
        toggled(_sortDescription: SortDescription|undefined){
            return true;
        }
    },
    methods: {
        forValue<T>(options: {asc?: T, desc?: T, none?: T}):T|undefined{
            if (this.sortBy !== this.value?.by){return options.none;}
            switch (this.value?.direction){
                case 'asc': return options.asc;
                case 'desc': return options.desc;
                default: return options.none;
            }
        },
        toggle(){
            const newValue = this.forValue<SortDescription>({
                none: {by: this.sortBy!, direction: 'asc'},
                asc: {by: this.sortBy!, direction: 'desc'}
            });
            this.$emit("toggled", newValue);
        }
    }
});
</script>
<style scoped>
.component:hover{cursor: pointer;}
.icon{opacity: 0;}
.component:hover .icon:not(.always-visible){opacity: 0.25;}
.icon.always-visible{opacity: 1;}
</style>
<template>
    <span @click="toggle()" class="component user-select-none">
        <slot></slot>
        <span class="ms-2">
            <font-awesome-icon :icon="icon" :class="{'icon':true, 'always-visible':isAlwaysVisible}"></font-awesome-icon>
        </span>
    </span>
</template>