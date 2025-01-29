<script setup>
import { AppState } from '@/AppState';
import { computed } from 'vue';


const VaultDetail = computed(() => AppState.activeVaults)

</script>

<template>
    <div class="modal fade" id="vaultDetailModal" tabindex="-1" role="dialog" aria-labelledby="vaultDetailModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="vaultDetailModal">{{ VaultDetail?.name }}</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="card mb-3" style="max-width: 540px;">
                        <div class="row g-0">
                            <div class="col-md-4">
                                <img :src="VaultDetail?.img" class="img-fluid rounded-start" alt="">
                            </div>
                            <div class="col-md-8">
                                <div class="card-body">
                                    <p class="card-text"><small class="text-body-secondary">{{ VaultDetail?.name
                                            }}</small>
                                    </p>
                                    <p class="card-text"><small class="text-body-secondary">{{ VaultDetail?.isPrivate
                                            }}</small>
                                    </p>
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