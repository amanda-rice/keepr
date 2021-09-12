import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultsService {
  async getAllVaults() {
    const res = await api.get('/api/vaults')
    AppState.vaults = res.data
    logger.log(res.data)
  }

  async getVaultsByProfile(id) {
    const res = await api.get(`/api/profiles/${id}/vaults`)
    AppState.profVaults = res.data
    logger.log(res.data)
  }

  async getVaultsByProfileNotKeep(id, keepId) {
    const res = await api.get(`/api/profiles/${id}/vaults`)
    AppState.selProfVaults = res.data.filter(v => (v.id !== keepId))
    logger.log(res.data)
  }

  async getVaultById(id) {
    const res = await api.get(`/api/vaults/${id}`)
    AppState.activeVault = res.data
    logger.log(res.data)
  }

  async createVault(obj) {
    const res = await api.post('/api/vaults', obj)
    console.log(res.data, 'create vault')
    AppState.vaults.push(res.data)
    AppState.profVaults.push(res.data)
  }
}

export const vaultsService = new VaultsService()
