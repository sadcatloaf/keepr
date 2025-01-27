<script setup>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import Pop from '@/utils/Pop.js';
import { logger } from '@/utils/Logger.js';
import { keepsService } from '@/services/KeepsService.js';
import KeepCard from '@/components/KeepCard.vue';
import { vaultsService } from '@/services/VaultsService.js';

const account = computed(() => AppState.account)

const keeps = computed(() => AppState.keeps)
const vaults = computed(() => AppState.vaults)

onMounted(() => {
  getKeeps()
  getVaults()
})

async function getKeeps() {
  try {
    await keepsService.getKeeps()
  }
  catch (error) {
    Pop.meow(error)
    logger.error("[Getting Keeps]", error.message)
  }


}
async function getVaults() {
  try {
    await vaultsService.getVaults()
  }
  catch (error) {
    Pop.meow(error);
    logger.log("[Getting Vaults]", error.message)
  }
}

</script>

<template>
  <div class="about text-center">
    <div v-if="account">
      <h1>Welcome {{ account.name }}</h1>
      <img class="rounded" :src="account.picture" alt="" />
      <p>{{ account.email }}</p>
    </div>
    <div v-else>
      <h1>Loading... <i class="mdi mdi-loading mdi-spin"></i></h1>
    </div>
  </div>
  <div class="container-fluid">
    <section class="row">
      <div class="col-md-12">
        <div class="row">
          <h1>Keeps</h1>
          <div v-for="keep in keeps" :key="keep.id" class="col-md-3 p-md-2">
            <!-- <img :src="keep.img" alt="" class="img-fluid"> -->
            <KeepCard :keep="keep" />
          </div>
        </div>
      </div>
    </section>
  </div>
</template>

<style scoped lang="scss">
img {
  max-width: 100px;
}
</style>
