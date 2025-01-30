<script setup>
import { keepsService } from '@/services/KeepsService';
import { logger } from '@/utils/Logger';
import Pop from '@/utils/Pop';
import { Modal } from 'bootstrap';
import { ref } from 'vue';



const editableKeepData = ref({
    name: '',
    img: '',
    description: '',

})


async function createKeep() {
    try {
        await keepsService.createKeep(editableKeepData.value)
        editableKeepData.value = {
            name: '',
            img: '',
            description: '',

        }
        Modal.getInstance('#keepFormModal').hide()

    }
    catch (error) {
        Pop.meow(error);
        logger.error('[Creating Keep]', error)
    }
}

</script>

<template>
    <div class="modal fade" id="keepFormModal" tabindex="-1" role="dialog" aria-labelledby="keepFormModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Add you keep</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="createKeep()">
                        <div class="mb-3">
                            <input v-model="editableKeepData.name" type="text" class="form-control" id="name">
                            <label for="name" class="form-label">Title...</label>
                        </div>
                        <div class="mb-3">
                            <input v-model="editableKeepData.img" type="url" class="form-control" id="img">
                            <label for="img" class="form-label">Umage URL...</label>
                        </div>
                        <div class="mb-3">
                            <input v-model="editableKeepData.description" type="text" class="form-control"
                                id="description">
                            <label for="description" class="form-label">Kept Description...</label>
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
