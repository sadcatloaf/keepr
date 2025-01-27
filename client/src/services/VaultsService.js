import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Vault } from "@/models/Vault.js"

class VaultsService {
    createKeep() {
        throw new Error('Method not implemented.')
    }

    async getVaults() {
        const response = await api.get('api/vaults')
        logger.log('Got Keeps', response.data)
        const vaults = response.data.map(vaultPOJO => new Vault(vaultPOJO))
        AppState.vaults = vaults
    }
}
export const vaultsService = new VaultsService()