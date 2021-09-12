import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultKeepsService {
  async create(obj) {
    const res = await api.post('/api/vaultKeeps/', obj)
    if (res.data) {
      const indexPK = AppState.profKeeps.findIndex(k => k.id === obj.keepId)
      if (AppState.profKeeps[indexPK]) {
        AppState.profKeeps[indexPK].keeps++
      }
      const indexVK = AppState.vaultKeeps.findIndex(k => k.id === obj.keepId)
      if (AppState.vaultKeeps[indexVK]) {
        AppState.vaultKeeps[indexVK].keeps++
      }
      const indexK = AppState.keeps.findIndex(k => k.id === obj.keepId)
      if (AppState.keeps[indexK]) {
        AppState.keeps[indexK].keeps++
      }
    }
    return res
  }
}

export const vaultKeepsService = new VaultKeepsService()
