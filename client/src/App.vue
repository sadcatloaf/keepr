<script setup>
import Navbar from './components/Navbar.vue';
import { AppState } from './AppState.js';
import KeepFormModal from './components/KeepFormModal.vue';
import VaultFormModal from './components/VAultFormModal.vue';

</script>

<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <footer class="bg-dark text-light">

  </footer>
  <KeepFormModal />
  <VaultFormModal />
</template>

<style lang="scss">
@import '@/assets/scss/main.scss';

:root {
  --main-height: calc(100vh - 32px - 64px);
}

footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
