import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Vault } from "@/models/Vault.js"
import { AppState } from "@/AppState.js"



class VaultKeepService {
    async createKeep(value) {
        const response = await api.post('api/vaultkeeps', value)
        logger.log("crated keep in vault", response.data)
        const vaultKeep = new Vault(response.data)
        return vaultKeep
    }
    async removeKeep(vaultKeepId) {
        // AppState.vaults = []
        const response = await api.delete(`api/vaultkeeps/${vaultKeepId}`)
        logger.log('Removed Keep', response.data)
        const vaultKeepIndex = AppState.keeps.findIndex(banana => banana.vaultKeepId == vaultKeepId)
        logger.log('removing from position', vaultKeepIndex)
        AppState.keeps.splice(vaultKeepIndex, 1)
    }

}

export const vaultKeepService = new VaultKeepService()