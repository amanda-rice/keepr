<template>
  <div class="component container-fluid">
    <div class="row pb-5">
      <div class="col-12">
        <h1>{{state.activeVault.name}}</h1>
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
      activeVault: computed(() => AppState.activeVault),
      vaultKeeps: computed(()=> AppState.vaultKeeps),
    })

    onMounted(async() => {
      try {
        await vaultsService.getVaultById(route.params.id)
        await keepsService.getKeepsByVaultId(route.params.id)
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


<style lang="scss" scoped>

</style>