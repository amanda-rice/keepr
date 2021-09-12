<template>
  <div class="component container-fluid">
    <div class="row pb-5">
      <div class="col-12">
        <div class="d-flex align-items-start">
          <h1 class="m-0">{{state.activeVault.name}}</h1>
          <i v-if="account.id === state.activeVault.creatorId" class="fa fa-trash pl-3 pt-3 fa-lg selectable" @click="deleteVault" title="Delete Vault"></i>
        </div>
        <h3>Keeps: {{state.vaultKeeps.length}}</h3>
      </div>
    </div>
    <div class="card-columns">
        <div v-for="k in state.vaultKeeps" :key="k.id">
          <KeepProfCard :keep="k" />
        </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted, reactive, watchEffect} from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import Pop from '../utils/Notifier'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import { keepsService } from '../services/KeepsService'
import { router } from '../router'

export default {
  setup() {
    const route = useRoute()
    const state = reactive({
      activeVault: computed(() => AppState.activeVault),
      vaultKeeps: computed(()=> AppState.vaultKeeps),
    })

    watchEffect(async() => {
      try {
        await vaultsService.getVaultById(route.params.id)
        await keepsService.getKeepsByVaultId(route.params.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      state,
      route,
      async deleteVault(){
        try {
          if (await Pop.confirm()) {
            await vaultsService.deleteVault(state.activeVault.id)
            Pop.toast('Deleted Vault Successfully', 'success')
            router.push({name: 'Home'})
          }
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      account: computed(()=>AppState.account)
    }
  },
  components:{}
}
</script>


<style lang="scss" scoped>

</style>