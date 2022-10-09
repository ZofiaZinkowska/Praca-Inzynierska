<template>
    <b-alert class="alert-danger my-3 shadow-sm" dismissible="true" v-bind:show="hasError">Wystąpił błąd</b-alert>
    <b-container class="shadow-sm rounded my-3 p-4 bg-body">
        <h4 class="border-bottom pb-2">Edycja</h4>
        <div class="d-flex justify-content-center mt-5 mb-3" v-if="isLoading">
            <b-spinner></b-spinner>
        </div>
        <div v-else-if="entry" class="mt-4">
            <b-form-group label="Id" label-for="id">
                <b-form-input id="id" v-model="entry.id" disabled></b-form-input>
            </b-form-group>
            <b-form-group label="Data dodania" label-for="addDate">
                <b-form-input id="addDate" v-model="entry.addDate" disabled></b-form-input>
            </b-form-group>
            <b-form-group label="Nazwa" label-for="name">
                <b-form-input id="name" v-model="entry.name"></b-form-input>
            </b-form-group>
            <b-button :loading="isSaving" :disabled="isSaving" v-on:click="save()" variant="primary">Zapisz</b-button>
        </div>
    </b-container>
</template>
<script lang="ts">
import type { RegisterEntry } from '../contract/RegisterEntry';
import axios from 'axios';
import { defineComponent } from 'vue';

interface Data {entry: RegisterEntry|null; hasError: boolean; isLoading: boolean; isSaving:boolean; }

export default defineComponent({
    data(): Data {
        return { entry:null, hasError: false, isLoading: false, isSaving:false };
    },
    methods: {
        async load(){
            try{
                this.isLoading = true;
                const id = this.$route.params.id;
                var response = await axios.get<RegisterEntry>(`https://localhost:5001/Register/Get?id=${id}`);
                this.entry = response.data;
            }
            catch{
                this.hasError = true;
            }
            finally{
                this.isLoading = false;
            }
        },
        async save(){
            try{
                this.isSaving = true;
                var response = await axios.post('https://localhost:5001/Register/Save',this.entry);
            }
            catch{
                this.hasError = true;
            }
            finally{
                this.isLoading = false; 
            }
        }
    },
    async mounted(){
        await this.load();
    }
});
</script>