<template>
  <div class="component container-fluid">
    <div class="row pb-5">
      <div class="col-12">
        <div class="d-flex align-items-start py-1">
          <h1 class="m-0">{{state.activeVault.name}}</h1>
          <i v-if="account.id === state.activeVault.creatorId" class="fa fa-trash pl-3 pt-3 text-warning fa-lg hoverable" @click="deleteVault" title="Delete Vault"></i>
        </div>
        <h3 class="py-1">Keeps: {{vaultKeeps.length}}</h3>
        <h3 class="text-dark-grey"><i>{{state.activeVault.isPrivate?'Private':'Public'}}</i></h3>
      </div>
    </div>
    <div class="card-columns">
        <div v-for="k in vaultKeeps" :key="k.id">
          <KeepProfCard :keep="k" />
        </div>
    </div>
  </div>
  <!-- <p v-else>You don't have access to this page</p> -->
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
      
    })

    watchEffect(async() => {
      try {
        if(route.params.id && !isNaN(route.params.id)){
        await vaultsService.getVaultById(route.params.id)
        }
      } catch (error) {
        router.push({name: 'Home'})
        Pop.toast(`You don't have access to this page`, 'error')
      }
      try {
        if(route.params.id && !isNaN(route.params.id)){
        await keepsService.getKeepsByVaultId(route.params.id)
        }
      } catch (error) {
        Pop.toast(`You don't have access to this page`, 'error')
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
      account: computed(()=>AppState.account),
      vaultKeeps: computed(()=> AppState.vaultKeeps),
    }
  },
  components:{}
}
</script>


<style lang="scss" scoped>

</style>