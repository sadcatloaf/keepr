<script setup>
import { AppState } from '@/AppState';
import KeepCard from '@/components/KeepCard.vue';
import VaultCard from '@/components/VaultCard.vue';
import { keepsService } from '@/services/KeepsService';
import { vaultsService } from '@/services/VaultsService';
import { logger } from '@/utils/Logger';
import Pop from '@/utils/Pop';
import { computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';

const keeps = computed(() => AppState.keeps)
const vault = computed(() => AppState.activeVaults)

const route = useRoute()

onMounted(() => {
    getVaultById()
    getKeepsByVaultId()
})

async function getVaultById() {
    try {
        const vaultId = route.params.vaultId
        await vaultsService.getVaultById(vaultId)
    } catch (error) {
        Pop.meow(error)
        logger.error('[GETTING PROFILE BY ID]', error)
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
                {{ vault }}
            </div>

            <div class="col-md-12">
                <div class="row">
                    <div v-for="keep in keeps" :key="keep.id" class="col-md-3 p-md-2">
                        <KeepCard :keep="keep" />
                    </div>
                </div>
            </div>
        </section>
    </div>
    <VaultDetailModal />
</template>

<style scoped lang="scss"></style>