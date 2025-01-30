<script setup>
import { AppState } from '@/AppState';
import { vaultKeepService } from '@/services/vaultKeepService';
import { logger } from '@/utils/Logger';
import Pop from '@/utils/Pop';
import { computed, ref } from 'vue';
import { useRoute } from 'vue-router';


const keepDetail = computed(() => AppState.activeKeeps)
const myVaults = computed(() => AppState.myVaults)
// const route = useRoute()

const editableVaultKeepData = ref({
    keepId: '',
    vaultId: '',
})

async function saveKeepToVault() {
    try {
        // @ts-ignore
        editableVaultKeepData.value.keepId = keepDetail.value.id
        logger.log('Saving keep', keepDetail.value.id, editableVaultKeepData.value)
        await vaultKeepService.createKeep(editableVaultKeepData.value)
        // TODO send editable to the vaultKeep service to POST vaultKeep
        // Modal.getInstance('#keepFormModal').hide()
    }
    catch (error) {
        Pop.meow(error);
        logger.error('[Creating Keep]', error)
    }
}
</script>

<template>
    <div class="modal fade" id="keepDetailModal" tabindex="-1" role="dialog" aria-labelledby="keepDetailModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="keepDetailModal"></h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="card mb-3" style="">
                        <div class="row g-0">
                            <div class="col-md-4">
                                {{ keepDetail?.id }}
                                <img :src="keepDetail?.img" class="img-fluid img-keep" alt="">
                            </div>
                            <div class="col-md-8">
                                <div class="card-body">
                                    <div class="d-flex justify-content-center">
                                        <p class="card-text"><small class="text-body-secondary"><i class="mdi mdi-eye
                                                    p-2"></i>{{ keepDetail?.views }}</small></p>
                                        <p class="card-text"><small class="text-body-secondary"><i class="mdi mdi-alpha
                                                    p-2"></i>{{ keepDetail?.kept }}</small></p>
                                    </div>
                                    <h5 class="card-title text-center p-2">{{ keepDetail?.name }}</h5>
                                    <p class="card-text p-2">{{ keepDetail?.description }}</p>
                                    <form action="">
                                        <select v-model="editableVaultKeepData.vaultId" class="form-select"
                                            aria-label="Pick a keep" required>
                                            <option value="" disabled selected></option>
                                            <option v-for="vault in myVaults" :key="'vaultForm' + vault.id"
                                                :value="vault.id">
                                                {{ vault.name }}</option>
                                        </select>
                                    </form>
                                    <button @click="saveKeepToVault()" class="round-pill">Save</button>
                                    <router-link v-if="keepDetail != null"
                                        :to="{ name: 'Profile', params: { profileId: keepDetail?.creatorId } }">
                                        <div data-bs-dismiss="modal">
                                            <img :src="keepDetail?.creator.picture" alt=""
                                                class="creator-img img-fluid">
                                            <b class="p-2">{{ keepDetail?.creator.name }}</b>
                                        </div>
                                    </router-link>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" data-bs-dismiss="modal">
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped lang="scss">
.round-pill {
    height: 2rem;
    border-radius: 10px;
    background-color: rgb(84, 76, 101);
}

.img-keep {
    background-image: cover;
    background-position: center;
}

.creator-img {
    height: 3rem;
    border-radius: 50%;
    aspect-ratio: 1/1;
}
</style>
// NOTE - USE THIS FOR YOUR SELECT VAULTS
<!-- <select v-model="editableReportData.restaurantId" class="form-select" aria-label="Pick a restaurant" required>
    <option value="" disabled selected>Select a restaurant</option>
    <option v-for="restaurant in restaurants" :key="'reportFrom' + restaurant.id" :value="restaurant.id">
      {{ restaurant.name }}
    </option>
  </select> -->