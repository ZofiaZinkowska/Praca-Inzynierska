<template>
  <b-navbar variant="dark" class="navbar-dark" sticky="top" container="sm">
    <b-navbar-brand to="/">Ewidencja roślin botanicznych</b-navbar-brand>
    <b-navbar-nav class="navbar-collapse">
      <b-nav-item to="/List">Ewidencja</b-nav-item>
      <b-nav-item to="/PlantProtection">Ochrona roślin</b-nav-item>
    </b-navbar-nav>
  </b-navbar>

  <main role="main" class="container">
    <router-view />
  </main>
</template>