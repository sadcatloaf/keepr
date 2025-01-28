<script setup>
import { AppState } from '@/AppState';
import { Keep } from '@/models/Keep';
import { keepsService } from '@/services/KeepsService';
import { computed } from 'vue';


const keepDetail = computed(() => AppState.activeKeeps)

</script>

<template>
    <div class="modal fade" id="keepDetailModal" tabindex="-1" role="dialog" aria-labelledby="keepDetailModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="keepDetailModal">{{ keepDetail?.name }}</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="card mb-3" style="max-width: 540px;">
                        <div class="row g-0">
                            <div class="col-md-4">
                                <img :src="keepDetail?.img" class="img-fluid rounded-start" alt="">
                            </div>
                            <div class="col-md-8">
                                <div class="card-body">
                                    <p class="card-text"><small class="text-body-secondary">{{
                                        keepDetail?.kept }}</small>
                                    </p>
                                    <p class="card-text"><small class="text-body-secondary">{{
                                        keepDetail?.views }}</small>
                                    </p>
                                    <h5 class="card-title">{{ keepDetail?.name }}</h5>
                                    <p class="card-text">{{ keepDetail?.description }}</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">

                </div>
            </div>
        </div>
    </div>
</template>

<style scoped lang="scss"></style>