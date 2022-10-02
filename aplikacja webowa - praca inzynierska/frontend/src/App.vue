<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
</script>

<template>

  <b-navbar>
    <b-navbar-nav>
      <RouterLink to = "/"> 
        start 
      </RouterLink>
    </b-navbar-nav>
  </b-navbar>
  <RouterView />
</template>