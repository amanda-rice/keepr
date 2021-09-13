<template>
  <div class="container-fluid home flex-grow-1 d-flex flex-column m-4">
    <div class="row align-items-start p-3">
      <div class="col-md-3">
        <img class=" rounded big-prof-pic w-100" :src="state.profile.picture" :alt="state.profile.name">
      </div>
      <div class="pl-4 col-md-9 d-flex flex-column align-items-start justify-content-end">
        <h1 class="text-break text-wrap">{{state.profile.name}}</h1>
        <h5>Vaults: {{state.profVaults.length}} </h5>
        <h5>Keeps: {{state.profKeeps.length}}</h5>
        <i v-if="route.params.id === account.id" class="fas fa-edit fa-2x hoverable" title="Edit Profile" data-toggle="modal" 
          data-target="#update-account-modal"></i>
      </div>
    </div>
    <div class="row">
      <div class="col-12">
        <div class="row">
          <div class="col-12 pb-4 d-flex">
            <h5 class="m-0 profile-titles text-left">
              Vaults
            </h5>
            <i v-if="route.params.id === account.id" class="fas fa-plus pt-2 fa-2x pl-3 hoverable" title="Create Vault" data-toggle="modal" 
          data-target="#create-vault-modal"></i>
          </div>
          <div class="col-md-4 col-lg-2" v-for="v in state.profVaults" :key="v.id">
            <VaultProfCard :vault="v" />
          </div>
        </div>
      </div>
      <div class="col-12 pb-4 pt-2 d-flex">
            <h5 class="text-left profile-titles m-0">
              Keeps
            </h5>
            <i v-if="route.params.id === account.id" class="fas fa-plus pt-2 fa-2x pl-3 hoverable" title="Create Keep" data-toggle="modal" 
          data-target="#create-keep-modal"></i>
          </div>
    </div>
      <div class="card-columns">
        <div v-for="k in state.profKeeps" :key="k.id">
          <KeepProfCard :keep="k" />
        </div>
    </div>
    <UpdateProfileModal/>
    <CreateVaultModal/>
    <CreateKeepModal/>
  </div>
</template>


<script>
import { computed, onMounted, reactive, watchEffect } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import Pop from '../utils/Notifier'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import { keepsService } from '../services/KeepsService'
import { profilesService } from '../services/ProfilesService'

export default {
  setup() {
    const route = useRoute()
    const state = reactive({
      profVaults: computed(() => AppState.profVaults),
      profKeeps: computed(()=> AppState.profKeeps),
      profile: computed(()=> AppState.activeProfile),
    })

    watchEffect(async() => {
      try {
        await vaultsService.getVaultsByProfile(route.params.id)
        await keepsService.getKeepsByProfile(route.params.id)
        await profilesService.getProfileById(route.params.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      state,
      route,
      account: computed(()=> AppState.account)
    }
  },
  components:{}
}
</script>


<style scoped lang="scss">
.home{
  text-align: center;
  user-select: none;
  > img{
    height: 200px;
    width: 200px;
  }
}
/* .big-prof-pic{
  height: 35vh;
  max-width: 30vh;
  background-size: cover;
  position: relative;
} */
.profile-titles{
  font-size: 40px;
  font-family: 'Lora', serif;
}
</style>