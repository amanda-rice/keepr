import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class KeepService {
  async getAllKeeps() {
    const res = await api.get('/api/keeps')
    AppState.keeps = res.data
    logger.log(res.data)
  }

  async getKeepsByProfile(id) {
    const res = await api.get(`/api/profiles/${id}/keeps`)
    AppState.profKeeps = res.data
    logger.log(res.data)
  }
}

export const keepsService = new KeepService()
