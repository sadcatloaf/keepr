import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Vault } from "@/models/Vault.js"

class VaultsService {

    async getVaults() {
        const response = await api.get('api/vaults')
        logger.log('Got Keeps', response.data)
        const vaults = response.data.map(vaultPOJO => new Vault(vaultPOJO))
        AppState.vaults = vaults
    }

    setActiveVault(vault) {
        AppState.activeVaults = vault
    }
}
export const vaultsService = new VaultsService()