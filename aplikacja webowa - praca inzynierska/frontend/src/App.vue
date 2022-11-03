<template>
  <div class="d-flex justify-content-center" style="background-color: #b8a4b1">
    <b-img src="praca_inżynierska1.png"></b-img>
  </div>
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