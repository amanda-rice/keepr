<template>
  <div class="container-fluid home flex-grow-1 d-flex flex-column align-items-center justify-content-center m-4">
      <div class="card-columns">
        <div v-for="k in keeps" :key="k.id">
          <KeepCard :keep="k" />
        </div>
      </div>
  </div>
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core'
import Pop from '../utils/Notifier'
import { logger } from '../utils/Logger'
import { keepsService } from '../services/KeepsService'
import { AppState } from '../AppState'

export default {
  setup() {
    onMounted(async()=>{
      try{
        await keepsService.getAllKeeps()
      }
      catch (error){
        Pop.toast(error, 'error')
      }
    })
    return {
      keeps: computed(() => AppState.keeps)
    }
  }
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
