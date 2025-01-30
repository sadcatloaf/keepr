import { AppState } from "@/AppState.js"
import { api } from "./AxiosService.js"
import { logger } from "@/utils/Logger.js"
import { Account } from "@/models/Account.js"

class ProfilesService {
    async getProfileById(profileId) {
        AppState.activeProfiles = null
        const response = await api.get(`api/profiles/${profileId}`)
        logger.log('GOT PROFILE 💂‍♀️', response.data)
        const profile = new Account(response.data)
        AppState.activeProfiles = profile
    }
}
export const profilesService = new ProfilesService