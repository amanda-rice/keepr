import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class ProfilesService {
/**
 *
 * @param {string} id
 */
  async getProfileById(id) {
    const res = await api.get('/api/profiles/' + id)
    AppState.activeProfile = res.data
    logger.log(res.data)
  }
}

export const profilesService = new ProfilesService()
