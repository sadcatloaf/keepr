<script setup>
import { AppState } from '@/AppState';
import KeepCard from '@/components/KeepCard.vue';
import KeepDetailModal from '@/components/KeepDetailModal.vue';
import { keepsService } from '@/services/KeepsService';
import { logger } from '@/utils/Logger';
import Pop from '@/utils/Pop';
import { computed, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const keeps = computed(() => AppState.keeps)
const account = computed(() => AppState.account)
const route = useRoute()
const router = useRouter()

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

async function deleteKeep() {
  try {
    const confirmed = await Pop.confirm(`Are you sure you want to delete?`)
    if (!confirmed) return
    const keepId = route.params.keepId
    await keepsService.deleteKeep(keepId)
    router.push({ name: 'Home' })
  }
  catch (error) {
    Pop.meow(error);
    logger.error('[deleting keep by ID]', error.message)
  }
}

</script>

<template>
  <div class="container-fluid">
    <section class="row">
      <div class="col-md-12">
        <div class="row">
          <div v-for="keep in keeps" :key="keep.id" class="col-md-3 p-md-2">
            <KeepCard :keep="keep" />
            <div v-if="keep.creatorId == account?.id">
              <button @click="deleteKeep()" class="btn btn-danger"><i class="mdi mdi-delete-forever"></i></button>
            </div>
          </div>
        </div>
      </div>
    </section>
  </div>
  <KeepDetailModal />
</template>

<style scoped lang="scss"></style>
