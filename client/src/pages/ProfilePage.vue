<script setup>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import Pop from '@/utils/Pop.js';
import { logger } from '@/utils/Logger.js';
import { keepsService } from '@/services/KeepsService.js';
import KeepCard from '@/components/KeepCard.vue';
import VaultCard from '@/components/VaultCard.vue';
import { accountService } from '@/services/AccountService.js';
import { profilesService } from '@/services/ProfilesService.js';
import { useRoute } from 'vue-router';
import { vaultsService } from '@/services/VaultsService.js';

const account = computed(() => AppState.account)
const profile = computed(() => AppState.activeProfiles)
const keeps = computed(() => AppState.keeps)
const vaults = computed(() => AppState.vaults)

const route = useRoute()

onMounted(() => {
    getKeepsByProfileId()
    getVaults()
    getProfileById()
    console.log(route)
})

async function getKeepsByProfileId() {
    try {
        const profileId = route.params.profileId
        await keepsService.getKeepsByProfileId(profileId)
    }
    catch (error) {
        Pop.meow(error)
        logger.error("[Getting Keeps]", error.message)
    }


}
async function getVaults() {
    try {

        const profileId = route.params.profileId
        await vaultsService.getVaultsByProfileId(profileId)
    }
    catch (error) {
        Pop.meow(error);
        logger.log("[Getting Vaults]", error.message)
    }

}
async function getProfileById() {
    try {
        const profileId = route.params.profileId
        await profilesService.getProfileById(profileId)
    } catch (error) {
        Pop.meow(error)
        logger.error('[GETTING PROFILE BY ID]', error)
    }
}

</script>

<template>
    <div class="about text-center">
        <div v-if="account">
            <img class="cover-img" :src="profile?.coverImg" alt="" />
            <div>
                <img class="img-profile" :src="profile?.picture" alt="" />
                <h1>{{ profile?.name }}</h1>
                <p>{{ vaults?.length }} Vaults | {{ keeps?.length }} Keeps</p>
            </div>
        </div>
        <div v-else>
            <h1>Loading... <i class="mdi mdi-loading mdi-spin"></i></h1>
        </div>
    </div>
    <div class="container-fluid">
        <section class="row">
            <div class="col-md-12">
                <div class="row">
                    <h1>Vaults</h1>
                    <div v-for="vault in vaults" :key="vault.id" class="col-md-3 p-md-2">
                        <VaultCard :vault="vault" />
                    </div>
                </div>
            </div>
        </section>
        <section class="row">
            <div class="col-md-12">
                <div class="row">
                    <h1>Keeps</h1>
                    <div v-for="keep in keeps" :key="keep.id" class="col-md-3 p-md-2">
                        <KeepCard :keep="keep" />
                    </div>
                </div>
            </div>
        </section>
    </div>
</template>

<style scoped lang="scss">
.img-profile {
    max-width: 100px;
    border-radius: 50px;
}

.cover-img {
    width: 60%;
    height: 30dvh;
    object-fit: cover;
    object-position: center;
    border-radius: 40px;
}
</style>
