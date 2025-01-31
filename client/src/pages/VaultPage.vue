<script setup>
import { AppState } from '@/AppState';
import KeepCard from '@/components/KeepCard.vue';
import VaultKeepDetailModal from '@/components/VaultKeepDetailModal.vue';
import { keepsService } from '@/services/KeepsService';
import { vaultsService } from '@/services/VaultsService';
import { logger } from '@/utils/Logger';
import Pop from '@/utils/Pop';
import { computed, onMounted, watch } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const keeps = computed(() => AppState.keeps)
// const vault = computed(() => AppState.activeVaults)

const route = useRoute()
const router = useRouter()

watch(route, () => {
    getVaultById()
    getKeepsByVaultId()
}, { immediate: true })
// onMounted(() => {
//     getVaultById()
//     getKeepsByVaultId()
// })

// TODO if this function errors (is caught), then we should push you back to the home page
async function getVaultById() {
    try {
        const vaultId = route.params.vaultId
        await vaultsService.getVaultById(vaultId)
    } catch (error) {
        Pop.meow(error)
        logger.error('[GETTING VAULT BY ID]', error)
        router.push({ name: 'Home' })
    }
}

async function getKeepsByVaultId() {
    try {
        const vaultId = route.params.vaultId
        await keepsService.getKeepsByVaultId(vaultId)
    }
    catch (error) {
        Pop.meow(error);
        logger.error('')
    }
}


</script>

<template>
    <div class="container-fluid">
        <section class="row">
            <div class="col-md-12">
                <div class="row">
                    <div v-for="keep in keeps" :key="'vault' + keep.id" class="col-md-3 p-md-2">
                        <KeepCard :keep="keep" />
                    </div>
                </div>
            </div>
        </section>
    </div>
    <VaultKeepDetailModal />
</template>

<style scoped lang="scss"></style>