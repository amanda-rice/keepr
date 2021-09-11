<template>
  <div class="container-fluid home flex-grow-1 d-flex flex-column align-items-center justify-content-center m-4">
    <div class="row">
      <div class="col-12">
        <div class="row">
          <div class="col-12 pb-4">
            <h1 class="m-0 text-left">
              Vaults
            </h1>
          </div>
          <div class="col-md-4 col-lg-2" v-for="v in state.profVaults" :key="v.id">
            <VaultProfCard :vault="v" />
          </div>
        </div>
      </div>
      <div class="col-12 pb-4 pt-2">
            <h1 class="text-left m-0">
              Vaults
            </h1>
          </div>
    </div>
      <div class="card-columns">
        <div v-for="k in state.profKeeps" :key="k.id">
          <KeepProfCard :keep="k" />
        </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted, reactive } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import Pop from '../utils/Notifier'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import { keepsService } from '../services/KeepsService'

export default {
  setup() {
    const route = useRoute()
    const state = reactive({
      profVaults: computed(() => AppState.profVaults),
      profKeeps: computed(()=> AppState.profKeeps),
    })

    onMounted(async() => {
      try {
        await vaultsService.getVaultsByProfile(route.params.id)
        await keepsService.getKeepsByProfile(route.params.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      state
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
</style>