<script setup>
import { AppState } from '@/AppState';
import KeepCard from '@/components/KeepCard.vue';
import KeepDetailModal from '@/components/KeepDetailModal.vue';
import { keepsService } from '@/services/KeepsService';
import { logger } from '@/utils/Logger';
import Pop from '@/utils/Pop';
import { computed, onMounted } from 'vue';

const keeps = computed(() => AppState.keeps)

onMounted(() => {
  getKeeps()
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

</script>

<template>
  <div class="container-fluid">
    <section class="row">
      <div class="col-md-12">
        <div class="row">
          <div v-for="keep in keeps" :key="keep.id" class="col-md-3 p-md-2">
            <!-- <img :src="keep.img" alt="" class="img-fluid"> -->
            <KeepCard :keep="keep" />
          </div>
        </div>
      </div>
    </section>
  </div>
  <KeepDetailModal />
</template>

<style scoped lang="scss"></style>
