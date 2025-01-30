<script setup>
import { AppState } from '@/AppState';
// import { Keep } from '@/models/Keep';
import { Vault } from '@/models/Vault';
// import { keepsService } from '@/services/KeepsService';
import { vaultsService } from '@/services/VaultsService';
import { logger } from '@/utils/Logger';
import Pop from '@/utils/Pop';
import { computed } from 'vue';

const account = computed(() => AppState.account)

const props = defineProps({
    vault: { type: Vault, required: true },
    // keep: { type: Keep, required: true }
})


async function deleteVault() {
    try {
        const confirmed = await Pop.confirm(`Are you sure you want to delete?`)
        if (!confirmed) return
        const vaultId = props.vault.id
        logger.log('Id', vaultId)
        // const keepId = props.keep.id
        await vaultsService.deleteVault(vaultId)
        // router.push({ name: 'Home' })
    }
    catch (error) {
        Pop.meow(error);
    }
}


</script>

<template>
    <router-link :to="{ name: 'Vault', params: { vaultId: vault.id } }">
        <div class="vault-card text-light fs-4  mb-5" :style="{ backgroundImage: `url(${vault.img})` }">
            <div class="inner-card d-flex flex-column justify-content-between p-3">
                <div class="text-end">
                    <span>{{ vault.name }}</span>
                </div>
            </div>
        </div>
    </router-link>
    <div v-if="vault.creatorId == account?.id">
        <button @click="deleteVault()" class="btn btn-danger"><i class="mdi mdi-delete-forever"></i></button>
    </div>
</template>

<style scoped lang="scss">
.vault-card {
    min-height: 50dvh;
    background-size: cover;
    background-position: center;
    border-radius: 10px;
}

.inner-card {
    min-height: 50dvh;
    width: 100%;
}
</style>