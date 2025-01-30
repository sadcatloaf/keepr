import { AppState } from "@/AppState.js"
import { Vault } from "@/models/Vault.js"
import { api } from "./AxiosService.js"
import { logger } from "@/utils/Logger.js"


class VaultsService {
    async getVaultById(vaultId) {
        AppState.activeVaults = null
        const response = await api.get(`api/vaults/${vaultId}`)
        logger.log('Got Vault', response.data)
        const vault = new Vault(response.data)
        AppState.activeVaults = vault
    }
    async createVault(vaultData) {
        const response = await api.post('api/vaults', vaultData)
        logger.log('Created Vault', response.data)
        const vault = new Vault(response.data)
        AppState.vaults.unshift(vault)
        return vault
    }

}
export const vaultsService = new VaultsService()

