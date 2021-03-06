<template>
    <div class="card img-rounded hoverable hover-here" >
      <div class="card-body p-0">
        <div class="img-total" data-toggle="modal" 
          :data-target="'#keep-modal-'+keep.id" @click="getKeep">
          <img class="w-100 gs img-rounded" :src="keep.img" :alt="keep.name" :title="keep.name">
          <div class="img-text d-flex justify-content-between align-items-end w-100 pl-3 pr-4 pb-2 " >
        </div>
        <div class="hover-show">
          <div v-if="route.name == 'Vault' && activeVault.creatorId == account.id">
            <i class="fas fa-times rmv-keep fa-lg text-light white-text-shadow hoverable" title="Remove Keep from Vault" @click.stop="removeKeep"></i>
          </div>
          <h5 class="text-light md-font-size w-100 text-center text-break text-wrap pl-2 pr-3 img-text">{{keep.name}}</h5>
        </div>
      </div>
    </div>
      <KeepModal v-if="keep.id" :keep="keep" />
    </div>
</template>


<script>
import { computed, onMounted, reactive, watchEffect } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import Pop from '../utils/Notifier'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import { keepsService } from '../services/KeepsService'
import { vaultKeepsService } from '../services/VaultKeepsService'

export default {
  props: {
    keep:{
      type:Object,
      required: true
    }
  },
  setup(props){
    const route = useRoute()
    return {
      route,
      /**
       * Before opening a keep card, if user is logged in, get a list of all of the vaults that 
       * the keep is not in to be used in the dropdown.
       **/
      async getKeep(){
        try {
          if(this.account.id){
            await vaultsService.getVaultsByProfileNotKeep(AppState.account.id, props.keep.id)
          }
          await keepsService.getById(props.keep.id, route.params.id)
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      /**
       * Allow users to remove keeps from their vaults
       **/
      async removeKeep(){
        try {
          if(await Pop.confirm()){
            await vaultKeepsService.remove(props.keep.vaultKeepId)
            await keepsService.getKeepsByVaultId(AppState.activeVault.id)
            Pop.toast('Removed Keep Successfully', 'success')
          }
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      /**
       * Users can delete their vaults. Must confirm the delete before method calls service
       * to perform delete.
       **/      
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
      activeVault: computed(()=> AppState.activeVault),
      account: computed(()=> AppState.account),
    }
  },
  components:{}
}
</script>


<style lang="scss" scoped>
.img-total{
  position: relative;
}
.img-text{
  position: absolute;
  bottom: 5px;
  left: 10px;
  text-shadow: 2px 2px 10px rgb(0, 0, 0);
  
}
.rmv-keep{
  position: absolute;
  top: 30px;
  right: 30px;
  text-shadow: 2px 2px 10px rgba(0, 0, 0, 0.829);
  
}
 .bg-gradient {
    background-image:
    linear-gradient(to bottom, rgba(8, 9, 15, 0.007), rgba(17, 15, 17, 0.616));
    background-size: cover;
    color: white;
    border-radius: 10%;
    position: absolute;
    bottom:-2px;
    left: -2px;
} 
h5{
    font-family: 'Lora', serif;
  }
.clip-text {
  white-space: nowrap;
  text-overflow: ellipsis;
  overflow-x: hidden;
  overflow-y: hidden;
  max-width: 95%;
  font-family: 'Lora', serif;
}

</style>