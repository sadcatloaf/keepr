<script setup>
import { AppState } from '@/AppState';
import { keepsService } from '@/services/KeepsService';
import { logger } from '@/utils/Logger';
import Pop from '@/utils/Pop';
import { Modal } from 'bootstrap';
import { computed, ref } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute();
const keeps = computed(() => AppState.keeps.filter(keep => keep.creatorId != AppState.account?.id))

const editableKeepData = ref({
    name: '',
    description: '',
    img: null,
    views: 0,
    kept: 0,
    creatorId: '',
})
// async function createKeep() {
//     try {
//         await keepsService.createKeep(editableKeepData.value)
//         editableKeepData.value = {
//             name: '',
//             description: '',
//             img: '',
//             views: 0,
//             kept: 0,
//             creatorId: ''
//         }
//         Modal.getInstance('#keepDetailModal').hide()
//     } catch (error) {
//         Pop.meow(error)
//         logger.log('[Saving Keep]', error.message)
//     }
// }


</script>

<template>
    <!-- <form @submit.prevent="createKeep()"> -->
    <div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog"
        aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="keepDetailModal">Modal title</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    ...
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Save changes</button>
                </div>
            </div>
        </div>
    </div>
    <!-- </form> -->
</template>

<style scoped lang="scss"></style>