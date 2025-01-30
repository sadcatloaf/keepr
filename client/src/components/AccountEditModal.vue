<script setup>
import { AppState } from '@/AppState';
import { accountService } from '@/services/AccountService';
import { logger } from '@/utils/Logger';
import Pop from '@/utils/Pop';
import { computed, onMounted, ref } from 'vue';


const account = computed(() => AppState.account)


onMounted(() => editableAccountData.value = { ...account.value })

const editableAccountData = ref({
    name: '',
    picture: '',
    coverImg: '',
})

async function updateAccount() {
    try {
        await accountService.updateAccount(editableAccountData.value)
    }
    catch (error) {
        Pop.meow(error);
        logger.error('[Updating Account]', error)
    }
}

</script>

<template>
    <div class="modal fade" id="accountEditModal" tabindex="-1" role="dialog" aria-labelledby="accountEditModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Edit account</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="updateAccount()">
                        <div class="mb-3">
                            <input v-model="editableAccountData.name" type="text" class="form-control" id="name">
                            <label for="name" class="form-label">Name...</label>
                        </div>
                        <div class="mb-3">
                            <input v-model="editableAccountData.coverImg" type="url" class="form-control" id="img">
                            <label for="img" class="form-label">Cover image URL...</label>
                        </div>
                        <div class="mb-3">
                            <input v-model="editableAccountData.picture" type="url" class="form-control"
                                id="description">
                            <label for="description" class="form-label">Picture...</label>
                        </div>

                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <button type="submit" class="btn btn-primary">Save changes</button>
                    </form>
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped lang="scss"></style>