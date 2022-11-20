<template>
    <div class="root">
        <slot></slot>
        <div class="spinner d-flex justify-content-center align-items-center" v-if="isVisible" >
            <b-spinner></b-spinner>
        </div>
    </div>
</template>
<style scoped>
.root{
    position: relative;
}
.spinner{
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(255,255,255,0.7);
}
</style>
<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
    props: {
        isVisible: {type: Boolean, default: false}
    }
});
</script>