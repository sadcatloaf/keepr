import { AppState } from "@/AppState.js"
import { Vault } from "@/models/Vault.js"
import { api } from "./AxiosService.js"
import { logger } from "@/utils/Logger.js"


class VaultsService {
    async deleteVault(vaultId) {
        const response = await api.delete(`api/vaults/${vaultId}`)
        logger.log('Deleted Keep', response.data)
        const vaultIndex = AppState.vaults.findIndex(vault => vault.id = vaultId)
        AppState.vaults.splice(vaultIndex, 1)
    }
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

    async getVaultsByProfileId(profileId) {
        const response = await api.get(`api/profiles/${profileId}/vaults`)
        logger.log('getting vaults for', profileId)
        const vaults = response.data.map(vaultPOJO => new Vault(vaultPOJO))
        AppState.vaults = vaults
    }

}
export const vaultsService = new VaultsService()

