import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  /**@type {import('@bcwdev/auth0provider-client').Identity} */
  identity: null,
  /** @type {import('./models/Account.js').Account} user info from the database*/
  account: null,
  /** @type {import('./models/Keep.js').Keep[]} user info from the database*/
  keeps: [],
  /** @type {import('./models/Vault.js').Vault[]} Vaults to be displayed on the Profile page*/
  vaults: [],
  /** @type {import('./models/Vault.js').Vault[]} Vaults to be displayed in the drop down menu, for the logged in user*/
  myVaults: [],
  /** @type {import('./models/Keep.js').Keep} user info from the database*/
  activeKeeps: null,
  /** @type {import('./models/Vault.js').Vault} user info from the database*/
  activeVaults: null,
  /** @type {import('./models/Profile.js').Profile} user info from the database*/
  activeProfiles: null

})

