import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  updateAccount(obj) {
    const res = api.put('/account', obj)
    console.log(res, 'account res')
    AppState.account.name = obj.name
    AppState.account.picture = obj.picture
    if (AppState.activeProfile.id === obj.id) {
      AppState.activeProfile.name = obj.name
      AppState.activeProfile.picture = obj.picture
      console.log(AppState.activeProfile, 'active prof')
    }
  }
}

export const accountService = new AccountService()
