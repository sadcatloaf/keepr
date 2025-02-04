<script setup>


import { AppState } from '@/AppState';
import { Keep } from '@/models/Keep';
import { keepsService } from '@/services/KeepsService';
import Pop from '@/utils/Pop';
import { computed } from 'vue';
// import { useRouter } from 'vue-router';

const account = computed(() => AppState.account)
// const router = useRouter()

const props = defineProps({
    keep: { type: Keep, required: true }
})

function setActiveKeep() {
    keepsService.setActiveKeep(props.keep)
    keepsService.IncrementViews(props.keep.id);
}
async function deleteKeep() {
    try {
        const confirmed = await Pop.confirm(`Are you sure you want to delete?`)
        if (!confirmed) return
        const keepId = props.keep.id
        await keepsService.deleteKeep(keepId)
        // router.push({ name: 'Home' })
    }
    catch (error) {
        Pop.meow(error);
    }
}


</script>


<template>
    <div @click="setActiveKeep()" class="keep-card text-light fs-4  mb-5" data-bs-toggle="modal"
        data-bs-target="#keepDetailModal">
        <div class="inner-card d-flex flex-column justify-content-between">
            <img :src="keep.img" alt="">
            <div class="keep-info d-flex justify-content-between align-items-end p-2 pt-4">
                <span class="">{{ keep.name }}</span>
                <img :src="keep.creator.picture" alt="" class="creator-img ">
            </div>
            <div>
            </div>
            <div v-if="keep.creatorId == account?.id" class="delete-btn">
                <button @click="deleteKeep()" class="btn btn-danger"><i class="mdi mdi-delete-forever"></i></button>
            </div>
        </div>
    </div>

</template>

<style scoped lang="scss">
.keep-card {
    background-size: cover;
    background-position: center;
    border-radius: 10px;

    img {
        width: 100%;
        height: auto;
    }

    .inner-card {
        width: 100%;
        position: relative;
    }

    .keep-info {
        position: absolute;
        bottom: 0;
        width: 100%;
        background-image: linear-gradient(0deg, rgba(0, 0, 0, 0.568) 0%, transparent 100%);
    }

    .delete-btn {
        position: absolute;
        right: -10px;
        top: -10px;
    }

    .creator-img {
        height: 45px;
        width: 45px;
        border-radius: 50%;
        aspect-ratio: 1/1;
    }
}
</style>

<!-- <router-link :to="{name: 'Profile', params: { profileId: profile.id },}">
    <img :src="post.creator?.picture" alt="" class="creator-img me-2" />
  </router-link> -->