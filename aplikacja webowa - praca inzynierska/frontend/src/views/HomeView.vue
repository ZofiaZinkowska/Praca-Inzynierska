<template>
   <page-component title="Ewidencja roślin botanicznych">
        <p>O aplikacji</p>
        <p>Aplikacja kierowana jest do wszystkich osób, które chciałyby prowadzić ewidencję roślin w tym wszystkich podmiotów specjalizujących się w obsłudze hobbystycznego rynku ogrodniczego, a w szczególności do producentów, dystrybutorów, właścicieli centrów ogrodniczych.</p>
        <p>Ewidencja roślin botanicznych to aplikacja webowa, dzięki której w prosty sposób, na bieżąco można prowadzić ewidencję roślin.
        Aplikacja pozwala oprócz ewidencjonowania, na zapisywanie do fromatu *.pdf, skanowanie etykiet roślin oraz wydruk.
            Jest dostępna bezpłatnie w wersji online jak i offline.
        </p>
   </page-component>
</template>
<script lang="ts">
import { defineComponent } from "vue";
import PageComponent from "../components/PageComponent.vue";

export default defineComponent({components: {PageComponent}});

</script>
