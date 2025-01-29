<script setup>
import { AppState } from '@/AppState';
import { computed } from 'vue';


const keepDetail = computed(() => AppState.activeKeeps)
const account = computed(() => AppState.account)
// const keep = computed(() => AppState.keeps)
</script>

<template>
    <div class="modal fade" id="keepDetailModal" tabindex="-1" role="dialog" aria-labelledby="keepDetailModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="keepDetailModal"></h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="card mb-3" style="">
                        <div class="row g-0">
                            <div class="col-md-4">
                                <img :src="keepDetail?.img" class="img-fluid img-keep" alt="">
                            </div>
                            <div class="col-md-8">
                                <div class="card-body">
                                    <div class="d-flex justify-content-center">
                                        <p class="card-text"><small class="text-body-secondary"><i class="mdi mdi-eye
                                                    p-2"></i>{{ keepDetail?.kept }}</small></p>
                                        <p class="card-text"><small class="text-body-secondary"><i class="mdi mdi-alpha
                                                    p-2"></i>{{ keepDetail?.views }}</small></p>
                                    </div>
                                    <h5 class="card-title text-center p-2">{{ keepDetail?.name }}</h5>
                                    <p class="card-text p-2">{{ keepDetail?.description }}</p>
                                    <button>Something to ask about!!</button> <button class="round-pill">Save</button>
                                    <img :src="account?.picture" alt="" class="creator-img img-fluid">
                                    <b class="p-2">{{ account?.name }}</b>

                                    <!-- <img :src="keep.creator.picture" alt="" class="creator-img img-fluid"> -->
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

<style scoped lang="scss">
.round-pill {
    height: 2rem;
    border-radius: 10px;
    background-color: rgb(84, 76, 101);
}

.img-keep {
    background-image: cover;
    background-position: center;
}

.creator-img {
    height: 3rem;
    border-radius: 50%;
    aspect-ratio: 1/1;
}
</style>