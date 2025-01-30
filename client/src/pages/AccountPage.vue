<script setup>
import { computed, onMounted, watch } from 'vue';
import { AppState } from '../AppState.js';
import Pop from '@/utils/Pop.js';
import { logger } from '@/utils/Logger.js';
import { keepsService } from '@/services/KeepsService.js';
import KeepCard from '@/components/KeepCard.vue';
import VaultCard from '@/components/VaultCard.vue';
import { accountService } from '@/services/AccountService.js';
import { useRoute } from 'vue-router';
import AccountEditModal from '@/components/AccountEditModal.vue';


const account = computed(() => AppState.account)

const keeps = computed(() => AppState.keeps)
const vaults = computed(() => AppState.vaults)

const route = useRoute()
watch(account, () => {
  getKeepsProfileId()
}, { immediate: true })

onMounted(() => {

  getVaults()
})

async function getKeepsProfileId() {
  try {
    // TODO end the function early so when there isn't an account available, it doesn't error

    // ---
    const profileId = account.value.id
    await keepsService.getKeepsByProfileId(profileId)
  }
  catch (error) {
    Pop.meow(error)
    logger.error("[Getting Keeps by creator]", error.message)
  }


}
async function getVaults() {
  try {
    await accountService.getVaults()
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
      <img class="cover-img" :src="account.coverImg" alt="" />
      <div>
        <img class="img-profile" :src="account.picture" alt="" />
        <button class="right-align-button" style="border: none; font-size: 40px" data-bs-toggle="modal"
          data-bs-target="#accountEditModal">...</button>
        <h1>{{ account.name }}</h1>
        <p>{{ vaults.length }} Vaults | {{ keeps.length }} Keeps</p>
      </div>
    </div>
    <div v-else>
      <h1>Loading... <i class="mdi mdi-loading mdi-spin"></i></h1>
    </div>
  </div>
  <div class="container-fluid">
    <section class="row">
      <div class="col-md-12">
        <div class="row">
          <h1>Vaults</h1>
          <div v-for="vault in vaults" :key="vault.id" class="col-md-3 p-md-2">
            <VaultCard :vault="vault" />
          </div>
        </div>
      </div>
    </section>
    <section class="row">
      <div class="col-md-12">
        <div class="row">
          <h1>Keeps</h1>
          <div v-for="keep in keeps" :key="keep.id" class="col-md-3 p-md-2">
            <KeepCard :keep="keep" />
          </div>
        </div>
      </div>
    </section>
  </div>
  <AccountEditModal />
</template>

<style scoped lang="scss">
.img-profile {
  max-width: 100px;
  border-radius: 50px;
}

.cover-img {
  width: 60%;
  height: 30dvh;
  object-fit: cover;
  object-position: center;
  border-radius: 40px;
}
</style>
