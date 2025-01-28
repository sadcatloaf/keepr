<script setup>
import { Keep } from '@/models/Keep';
import { keepsService } from '@/services/KeepsService';

const props = defineProps({
    keepProp: { type: Keep, required: true }
})

function setActiveKeep() {
    keepsService.setActiveKeep(props.keepProp)
}
</script>

<template>
    <div class="card mb-3" style="max-width: 540px;">
        <div class="row g-0">
            <div class="col-md-4">
                <img :src="keepProp.img" class="img-fluid rounded-start" alt="">
            </div>
            <div class="col-md-8">
                <div class="card-body">
                    <p class="card-text"><small class="text-body-secondary">{{ keepProp.kept }}</small></p>
                    <p class="card-text"><small class="text-body-secondary">{{ keepProp.views }}</small></p>
                    <h5 class="card-title">{{ keepProp.name }}</h5>
                    <p class="card-text">{{ keepProp.description }}</p>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped lang="scss"></style>