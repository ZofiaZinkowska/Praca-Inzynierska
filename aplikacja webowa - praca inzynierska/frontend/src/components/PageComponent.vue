<template>
        <b-alert class="my-3 shadow-sm" :variant="alert?.type" dismissible="true" :show="!!alert">{{alert?.text}}</b-alert>
    <b-container class="shadow-sm rounded my-3 p-4 bg-body">
        <h4 class="border-bottom pb-2">{{title}}</h4>
        <slot name="before-spinner" :disabled="isBusy"></slot>
        <div class="d-flex justify-content-center mt-5 mb-3" v-if="isBusy">
            <b-spinner></b-spinner>
        </div>
        <slot v-else/>
    </b-container>
</template>
<script lang="ts">
import { defineComponent, type PropType } from 'vue';
import type {Alert} from './Alert';

export default defineComponent({
    props: {
        alert: Object as PropType<Alert>,
        title: String,
        isBusy: Boolean
    }
});
</script>