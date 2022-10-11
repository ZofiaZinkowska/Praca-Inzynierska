<template>
    <page-component title="Edycja" :is-busy="isLoading" :alert="alert">
        <div v-if="entry" class="mt-4">
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
    </page-component>
</template>
<script lang="ts">
import type { RegisterEntry } from '../contract/RegisterEntry';
import axios from 'axios';
import { defineComponent } from 'vue';
import PageComponent from '../components/PageComponent.vue';
import type { Alert } from '@/components/Alert';

interface Data {entry: RegisterEntry|null; alert?: Alert; isLoading: boolean; isSaving:boolean; }

export default defineComponent({
    data(): Data {
        return { entry: null, alert: undefined, isLoading: false, isSaving: false };
    },
    methods: {
        async load() {
            try {
                this.alert=undefined;
                this.isLoading = true;
                const id = this.$route.params.id;
                var response = await axios.get<RegisterEntry>(`https://localhost:5001/Register/Get?id=${id}`);
                this.entry = response.data;
            }
            catch {
                this.alert = {type:"danger", text:"Wystąpił błąd"};
            }
            finally {
                this.isLoading = false;
            }
        },
        async save() {
            try {
                this.alert=undefined;
                this.isSaving = true;
                await axios.post("https://localhost:5001/Register/Save", this.entry);
                this.$router.push({ name: "List" });
            }
            catch {
                this.alert = {type:"danger", text:"Wystąpił błąd"};
            }
            finally {
                this.isSaving = false;
            }
        }
    },
    async mounted() {
        await this.load();
    },
    components: { PageComponent }
});
</script>