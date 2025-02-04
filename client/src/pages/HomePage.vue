<script setup>
import { AppState } from '@/AppState';
import KeepCard from '@/components/KeepCard.vue';
import KeepDetailModal from '@/components/KeepDetailModal.vue';
import { keepsService } from '@/services/KeepsService';
import { logger } from '@/utils/Logger';
import Pop from '@/utils/Pop';
import { computed, onMounted } from 'vue';
// import { useRoute } from 'vue-router';

const keeps = computed(() => AppState.keeps)
// const account = computed(() => AppState.account)
// const route = useRoute()
// const router = useRouter()

onMounted(() => {
  getKeeps()
  // console.log(route)
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
        <div class="masonry">
          <div v-for="keep in keeps" :key="'home' + keep.id" class="masonry-item">
            <KeepCard :keep="keep" />
          </div>
        </div>
      </div>
    </section>
  </div>
  <KeepDetailModal />
</template>

<style scoped lang="scss">
.masonry {
  columns: 200px;
}

.masonry-item {
  display: inline-block;
}
</style>
