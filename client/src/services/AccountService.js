import { Vault } from '@/models/Vault.js'
import { AppState } from '../AppState.js'
import { Account } from '../models/Account.js'
import { logger } from '../utils/Logger.js'
import { api } from './AxiosService.js'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getVaults() {
    const response = await api.get('/account/vaults')
    logger.log('Got Vaults', response.data)
    const vaults = response.data.map(vaultPOJO => new Vault(vaultPOJO))
    AppState.vaults = vaults
  }

  setActiveVault(vault) {
    AppState.activeVaults = vault
  }

  async updateAccount(accountData) {
    const response = await api.put('/account', accountData)
    logger.log('Updated Account', response.data)
    AppState.account = new Account(response.data)
  }
}

export const accountService = new AccountService()
