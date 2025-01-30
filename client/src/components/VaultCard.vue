<script setup>
import { Vault } from '@/models/Vault';



defineProps({
    vault: { type: Vault, required: true }
})



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