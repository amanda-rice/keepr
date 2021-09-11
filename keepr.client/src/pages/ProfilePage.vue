<template>
  <div class="container-fluid p-4">
    <div class="row">
      <div class="col-12">
        <div class="row">
          <div class="col-6 col-md-4 col-lg-2" v-for="k in state.profKeeps" :key="k.id">
            <KeepProfCard :keep="k" />
          </div>
        </div>
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


<style lang="scss" scoped>

</style>