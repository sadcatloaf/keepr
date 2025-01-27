<script setup>
import { AppState } from '@/AppState';
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
      <div class="col-12">
        {{ keeps }}
      </div>
    </section>
  </div>
</template>

<style scoped lang="scss"></style>
