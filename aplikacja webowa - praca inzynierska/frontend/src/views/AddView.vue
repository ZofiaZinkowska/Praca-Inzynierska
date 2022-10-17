<template>
    <page-component title="Dodawanie nowego wpisu" :is-busy="isLoading" :alert="alert">
        <div class="mt-4">
            <b-form-group label="Nazwa" label-for="name">
                <b-form-input id="name" v-model="entry.name"></b-form-input>
            </b-form-group>
            <div class=" d-flex justify-content-end">
                <b-button :disabled="isSaving" @click="$router.back()" variant="secondary" class="me-2">Anuluj</b-button>
                <b-button :loading="isSaving" :disabled="isSaving" @click="save()" variant="success">Zapisz</b-button>
            </div>
        </div>
    </page-component>
</template>
<script lang="ts">
import type { NewRegisterEntry } from '../contract/NewRegisterEntry';
import axios from 'axios';
import { defineComponent } from 'vue';
import PageComponent from '../components/PageComponent.vue';
import type { Alert } from '@/components/Alert';

interface Data {entry: NewRegisterEntry; alert?: Alert; isLoading: boolean; isSaving:boolean; }

export default defineComponent({
    data(): Data {
        return { entry: {name:''}, alert: undefined, isLoading: false, isSaving: false };
    },
    methods: {
        async save() {
            try {
                this.alert=undefined;
                this.isSaving = true;
                await axios.put("https://localhost:5001/Register/Add", this.entry);
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
    components: { PageComponent }
});
</script>