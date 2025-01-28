<script setup>
import { Vault } from '@/models/Vault';
import { vaultsService } from '@/services/VaultsService';


const props = defineProps({
    vault: { type: Vault, required: true }
})

function setActiveVault() {
    vaultsService.setActiveVault(props.vault)
}

</script>

<template>
    <div @click="setActiveVault()" class="vault-card text-light fs-4  mb-5" data-bs-toggle="modal"
        data-bs-target="#vaultDetailModal" :style="{ backgroundImage: `url(${vault})` }">
        <div class="inner-card d-flex flex-column justify-content-between p-3">
            <div class="text-end">
                <span>{{ vault.name }}</span>
            </div>
        </div>
    </div>
</template>

<style scoped lang="scss">
.vault-card {
    min-height: 50dvh;
    background-size: cover;
    background-position: center;
    border-radius: 10px;

    .inner-card {
        min-height: 50dvh;
        width: 100%;
    }
}
</style>