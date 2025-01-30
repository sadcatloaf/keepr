import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Keep } from "@/models/Keep.js"
import { AppState } from "@/AppState.js"

class KeepsService {
    async getKeepsByVaultId(vaultId) {
        AppState.keeps = []
        const response = await api.get(`api/vaults/${vaultId}/keeps`)
        logger.log('[Got Keeps', response.data)
        AppState.keeps = response.data.map(pojo => new Keep(pojo))
    }
    async getKeepsByProfileId(profileId) {
        const response = await api.get(`api/profiles/${profileId}/keeps`)
        logger.log('Got my Keeps', response.data)
        const keeps = response.data.map(keepPOJO => new Keep(keepPOJO))
        AppState.keeps = keeps
    }

    async getKeeps() {
        const response = await api.get('api/keeps')
        logger.log('Got Keeps', response.data)
        const keeps = response.data.map(keepPOJO => new Keep(keepPOJO))
        AppState.keeps = keeps
    }
    setActiveKeep(keep) {
        AppState.activeKeeps = keep
    }

    async createKeep(keepData) {
        const response = await api.post('api/keeps', keepData)
        logger.log('Craeted Keep', response.data)
        const keep = new Keep(response.data)
        AppState.keeps.unshift(keep)
        return keep
    }
}
export const keepsService = new KeepsService()