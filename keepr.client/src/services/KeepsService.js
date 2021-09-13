import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class KeepsService {
  async getAllKeeps() {
    const res = await api.get('/api/keeps')
    AppState.keeps = res.data
    logger.log(res.data)
  }

  async getById(id) {
    const res = await api.get(`/api/keeps/${id}`)
    const index = AppState.profKeeps.findIndex(k => k.id === id)
    if (AppState.profKeeps[index]) {
      AppState.profKeeps[index].views++
    }
    const index2 = AppState.vaultKeeps.findIndex(k => k.id === id)
    if (AppState.vaultKeeps[index2]) {
      AppState.vaultKeeps[index2].views++
    }
    logger.log(res.data)
  }

  async getByIdProf(id) {
    const res = await api.get(`/api/keeps/${id}`)
    const index = AppState.keeps.findIndex(k => k.id === id)
    AppState.keeps[index] = res.data
    logger.log()
  }

  async getKeepsByProfile(id) {
    const res = await api.get(`/api/profiles/${id}/keeps`)
    AppState.profKeeps = res.data
    logger.log(res.data)
  }

  async getKeepsByVaultId(id) {
    const res = await api.get(`/api/vaults/${id}/keeps`)
    AppState.vaultKeeps = res.data
    logger.log(res.data, 'vaultKeeps')
  }

  async createKeep(obj) {
    const res = await api.post('/api/keeps', obj)
    AppState.keeps.push(res.data)
    AppState.profKeeps.push(res.data)
  }

  async deleteKeep(id) {
    const res = await api.delete(`/api/keeps/${id}`)

    AppState.profKeeps = AppState.profKeeps.filter(k => k.id !== id)

    AppState.vaultKeeps = AppState.vaultKeeps.filter(k => k.id !== id)
    AppState.keeps = AppState.keeps.filter(k => k.id !== id)
    console.log(res)
  }
}

export const keepsService = new KeepsService()
