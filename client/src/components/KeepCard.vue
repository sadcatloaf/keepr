<script setup>
import { AppState } from '@/AppState';
import { Keep } from '@/models/Keep';
import { keepsService } from '@/services/KeepsService';
import { computed } from 'vue';


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
            <div class="">
                <span class="p-3">{{ keep.name }}</span>
                <img :src="keep.creator.picture" alt="" class="creator-img img-fluid">
            </div>
            <div>
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
        height: 3rem;
        border-radius: 50%;
        aspect-ratio: 1/1;
    }
}
</style>

<!-- <router-link :to="{name: 'Profile', params: { profileId: profile.id },}">
    <img :src="post.creator?.picture" alt="" class="creator-img me-2" />
  </router-link> -->