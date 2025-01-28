<script setup>
import { Keep } from '@/models/Keep';
import { keepsService } from '@/services/KeepsService';


const props = defineProps({
    keep: { type: Keep, required: true }
})

function setActiveKeep() {
    keepsService.setActiveKeep(props.keep)
}

</script>

<template>
    <div @click="setActiveKeep()" class="keep-card text-light fs-4  mb-5" data-bs-toggle="modal"
        data-bs-target="#keepDetailModal" :style="{ backgroundImage: `url(${keep.img})` }">
        <div class="inner-card d-flex flex-column justify-content-between p-3">
            <div class="text-end">
                <span>{{ keep.name }}</span>
            </div>
            <div>
                <img :src="keep.creator.picture" alt="" class="creator-img img-fluid">
            </div>
        </div>
    </div>
</template>

<style scoped lang="scss">
.keep-card {
    min-height: 50dvh;
    background-size: cover;
    background-position: center;
    border-radius: 10px;

    .inner-card {
        min-height: 50dvh;
        width: 100%;
    }

    .creator-img {
        height: 5rem;
        border-radius: 50%;
        aspect-ratio: 1/1;
    }
}
</style>