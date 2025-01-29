<script setup>
import { onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';

const theme = ref(loadState('theme') || 'light')

onMounted(() => {
  document.documentElement.setAttribute('data-bs-theme', theme.value)
})

function toggleTheme() {
  theme.value = theme.value == 'light' ? 'dark' : 'light'
  document.documentElement.setAttribute('data-bs-theme', theme.value)
  saveState('theme', theme.value)
}

</script>

<template>
  <nav class="navbar navbar-expand-sm navbar-dark px-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <button class="d-flex flex-column align-items-center round-pill">
        Home
      </button>
      <hr>
    </router-link>
    <!-- Example split danger button -->
    <div class="btn-group">
      <button type="button" class="btn btn-light">Action</button>
      <button type="button" class="btn btn-light dropdown-toggle dropdown-toggle-split" data-bs-toggle="dropdown"
        aria-expanded="false">
        <span class="visually-hidden">Toggle Dropdown</span>
      </button>
      <ul class="dropdown-menu">
        <li><a class="dropdown-item" href="#">new keep</a></li>
        <hr class="dropdown-divider">
        <li><a class="dropdown-item" href="#">new vault</a></li>
        <li>
        </li>
      </ul>
    </div>

    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <div>
        <button class="btn text-light" @click="toggleTheme"
          :title="`Enable ${theme == 'light' ? 'dark' : 'light'} theme.`">
          <Icon :name="theme == 'light' ? 'weather-sunny' : 'weather-night'" />
        </button>
      </div>
      <Login />
    </div>
  </nav>
</template>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.round-pill {
  height: 2rem;
  border-radius: 10px;
  background-color: rgb(174, 165, 153);
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 576px) {
  nav {
    height: 64px;
  }
}
</style>
