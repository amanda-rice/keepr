import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultsService {
  async getAllVaults() {
    const res = await api.get('/api/vaults')
    AppState.vaults = res.data
    logger.log(res.data)
  }

  /**
 *
 * @param {string} id
 */
  async getVaultsByProfile(id) {
    const res = await api.get(`/api/profiles/${id}/vaults`)
    AppState.profVaults = res.data
    logger.log(res.data)
  }

  /**
 *
 * @param {string} id
 * @param {number} keepId
 */
  async getVaultsByProfileNotKeep(id, keepId) {
    const res = await api.get(`/api/profiles/${id}/vaults`)
    const vaultsByKeep = await api.get(`/api/keeps/${keepId}/vaults`)
    const notInside = res.data.filter(v => !vaultsByKeep.data.find(vk => vk.id === v.id))
    AppState.selProfVaults = notInside
    logger.log(res.data)
  }

  /**
 *
 * @param {number} id
 */
  async getVaultById(id) {
    const res = await api.get(`/api/vaults/${id}`)
    AppState.activeVault = res.data
    logger.log(res.data)
  }

  /**
 *
 * @param {object} obj
 */
  async createVault(obj) {
    const res = await api.post('/api/vaults', obj)
    AppState.vaults.push(res.data)
    AppState.profVaults.push(res.data)
  }

  /**
 *
 * @param {number} id
 */
  async deleteVault(id) {
    const res = await api.delete(`/api/vaults/${id}`)
  }
}

export const vaultsService = new VaultsService()
