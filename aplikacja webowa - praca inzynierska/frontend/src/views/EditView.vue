<template>

</template>
<script lang="ts">
import type { RegisterEntry } from '../contract/RegisterEntry';
import axios from 'axios';
import { defineComponent } from 'vue';

interface Data {entry: RegisterEntry|null; hasError: boolean; isLoading: boolean;}

export default defineComponent({
    data(): Data {
        return { entry:null, hasError: false, isLoading: false };
    },
    methods: {
        async load(){
            try{
                this.isLoading = true;
                const id = this.$route.params.id;
                var response = await axios.get<RegisterEntry>(`https://localhost:5001/Register/Get?id=${id}`);
            }
            catch{
                this.hasError = true;
            }
            finally{
                this.isLoading = false;
            }
        },
    },
    async mounted(){
        await this.load();
    }
});
</script>