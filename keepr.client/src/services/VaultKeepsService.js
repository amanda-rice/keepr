import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { keepsService } from './KeepsService'

class VaultKeepsService {
  async create(obj) {
    const res = await api.post('/api/vaultKeeps/', obj)
    if (res.data) {
      keepsService.getKeepsByVaultId(obj.vaultId)
      const indexK = AppState.keeps.findIndex(k => k.id === obj.keepId)
      if (AppState.keeps[indexK]) {
        AppState.keeps[indexK].keeps++
      }
    }
    return res
  }

  async remove(id) {
    const res = await api.delete('/api/vaultKeeps/' + id)
    AppState.vaultKeeps = AppState.vaultKeeps.filter(k => k.id !== id)
    return res
  }
}

export const vaultKeepsService = new VaultKeepsService()
