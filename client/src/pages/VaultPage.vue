<script setup>
import { AppState } from '@/AppState';
import VaultCard from '@/components/VaultCard.vue';
import { vaultsService } from '@/services/VaultsService';
import { logger } from '@/utils/Logger';
import Pop from '@/utils/Pop';
import { computed, onMounted } from 'vue';

const vaults = computed(() => AppState.vaults)

onMounted(() => {
    getVaults()
})

async function getVaults() {
    try {
        await vaultsService.getVaults()
    }
    catch (error) {
        Pop.meow(error)
        logger.error("[Getting Vaults]", error.message)
    }

}

</script>

<template>
    <div class="container-fluid">
        <section class="row">
            <div class="col-md-12">
                <div class="row">
                    <div v-for="vault in vaults" :key="vault.id" class="col-md-3 p-md-2">
                        <VaultCard :vault="vault" />
                    </div>
                </div>
            </div>
        </section>
    </div>
    <VaultDetailModal />
</template>

<style scoped lang="scss"></style>