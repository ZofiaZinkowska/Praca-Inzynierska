<template>
  <b-navbar variant="dark" class="navbar-dark" sticky="top" container="sm">
    <b-navbar-brand to="/">Ewidencja roślin botanicznych</b-navbar-brand>
    <b-navbar-nav class="navbar-collapse">
      <b-nav-item to="/List">Moje roślinki</b-nav-item>
      <b-nav-item to="/2">Two</b-nav-item>
      <b-nav-item to="/3">Three</b-nav-item>
    </b-navbar-nav>
  </b-navbar>

  <main role="main" class="container">
    <router-view />
  </main>
</template>