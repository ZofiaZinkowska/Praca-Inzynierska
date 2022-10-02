<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
</script>

<template>

  <b-navbar>
    <b-navbar-nav>
        <nav class ="navbar navbar-dark bg-secondary">
          <a class="navbar-brand" href="#">ewidencja roślin botanicznych</a>
          <RouterLink to = "/">
            <div class="collapse navbar-collapse" id="mainmenu">
            <ul class="navbar-nav">
              <li class="nav-item">
                <a class="nav-link" href="#">Start</a>
              </li>

              <li class="nav-item">
                <a class="nav-link" href="#">Start1</a>
              </li>
              
              <li class="nav-item">
                <a class="nav-link" href="#">Start3</a>
              </li>

            </ul>
          </div>
          </RouterLink>
        </nav>
    </b-navbar-nav>
  </b-navbar>
  <RouterView />
</template>