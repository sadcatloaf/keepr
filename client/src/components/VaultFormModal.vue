<script setup>
import { vaultsService } from '@/services/VaultsService';
import { logger } from '@/utils/Logger';
import Pop from '@/utils/Pop';
import { Modal } from 'bootstrap';
import { ref } from 'vue';



const editableVaultData = ref({
    name: '',
    img: '',
    description: '',
    isPrivate: false

})


async function createVault() {
    try {
        await vaultsService.createVault(editableVaultData.value)
        editableVaultData.value = {
            name: '',
            img: '',
            description: '',
            isPrivate: false

        }
        Modal.getInstance('#vaultFormModal').hide()
    }
    catch (error) {
        Pop.meow(error);
        logger.error('[Creating Vault]', error)
    }
}

</script>

<template>
    <div class="modal fade" id="vaultFormModal" tabindex="-1" role="dialog" aria-labelledby="vaultFormModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Add your Vault</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="createVault()">
                        <div class="mb-3">
                            <input v-model="editableVaultData.name" type="text" class="form-control" id="name">
                            <label for="name" class="form-label">Title...</label>
                        </div>
                        <div class="mb-3">
                            <input v-model="editableVaultData.img" type="url" class="form-control" id="img">
                            <label for="img" class="form-label">URL...</label>
                        </div>
                        <div class="mb-3 form-check">
                            <input v-model="editableVaultData.isPrivate" type="checkbox" class="form-check-input"
                                id="is_private">
                            <label class="form-check-label" for="private">Make vault private</label>
                        </div>
                        <button type="submit" class="btn btn-primary">Create</button>
                    </form>
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped lang="scss"></style>
