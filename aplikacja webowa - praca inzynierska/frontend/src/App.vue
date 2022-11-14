<template>
  <div class="d-flex justify-content-center" style="background-color: white">
    <b-img src="praca_inżynierska.png"></b-img>
  </div>
  <b-navbar variant="dark" class="navbar-dark" sticky="top" container="sm">
    <b-navbar-brand to="/">Ewidencja roślin botanicznych</b-navbar-brand>
    <b-navbar-nav class="navbar-collapse">
      <b-nav-item-dropdown text="Ewidencja" :dark="true">
        <b-link class="dropdown-item" to="/List">Wykaz roślin</b-link>
        <b-link class="dropdown-item" to="/Add">Dodaj</b-link>
      </b-nav-item-dropdown>
      <b-nav-item to="/PlantProtection">Ochrona roślin</b-nav-item>
    </b-navbar-nav>
  </b-navbar>

  <main role="main" class="container">
    <router-view />
  </main>
</template>