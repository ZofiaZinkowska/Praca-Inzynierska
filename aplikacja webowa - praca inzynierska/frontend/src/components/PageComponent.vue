<template>
    <b-alert class="my-3 shadow-sm" :variant="alert?.type" dismissible="true" :show="!!alert">{{ alert?.text }}
    </b-alert>
    <b-container class="shadow-sm rounded my-3 p-4 bg-body">
        <div class="d-flex align-items-center mb-2 border-bottom pb-2">
            <h4 class="mb-0">{{ title }}</h4>
            <div class="ms-auto">
                <slot name="actions"></slot>
            </div>
        </div>
        <slot name="before-spinner" :disabled="isBusy"></slot>
        <spinner-component :is-visible="isBusy">
            <slot/>
        </spinner-component>
    </b-container>
</template>
<script lang="ts">
import { defineComponent, type PropType } from 'vue';
import type { Alert } from './Alert';
import SpinnerComponent from './SpinnerComponent.vue';

export default defineComponent({
    props: {
        alert: Object as PropType<Alert>,
        title: String,
        isBusy: Boolean
    },
    component: {
        SpinnerComponent
    },
    components: { SpinnerComponent }
});
</script>