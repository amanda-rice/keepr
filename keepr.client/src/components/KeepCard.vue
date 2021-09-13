<template>
  <div>
    <div class="card img-rounded hoverable hover-here" >
      <div class="card-body p-0">
        <div class="img-total">
          <img class="w-100 gs img-rounded hoverable" :src="keep.img" :alt="keep.name" :title="keep.name" data-toggle="modal" 
          :data-target="'#keep-modal-'+keep.id" @click="getKeep">
          <div class="hover-show ">
          <div class="img-text d-flex justify-content-between align-items-end w-100 pl-3 pr-4 pb-2">
            <h3 class="pl-3 pr-2 text-light white-text-shadow clip-text md-font-size">{{keep.name}}</h3>
            <div>
                <img class="sm-prof-img mt-1 mr-3 mb-2" :src="keep.creator.picture" :alt="keep.creator.name" :title="keep.creator.name" @click.stop="goToProfile">
            </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <KeepModal v-if="keep.id" :keep="keep" />
  </div>
</template>


<script>
import { computed, onMounted, reactive } from '@vue/runtime-core'
import { useRoute} from 'vue-router'
import { router } from '../router'
import Pop from '../utils/Notifier'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import { keepsService } from '../services/KeepsService'

export default {
  props: {
    keep:{
      type:Object,
      required: true
    }
  },
  setup(props){
    const route = useRoute()
    const state = reactive({
      account: computed(()=> AppState.account)
    })
    return {
      async getKeep(){
        try {
          if(state.account.id){
            await keepsService.getByIdProf(props.keep.id)
            await vaultsService.getVaultsByProfileNotKeep(AppState.account.id, props.keep.id)
          }
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      goToProfile(){
        router.push({ name: 'Profile', params: {id: props.keep.creatorId}})
      }
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
.sm-prof-img{
  height: 30px;
  width: 30px;
  border-radius: 50%;
}
 /* .bg-gradient {
    background-image:
    linear-gradient(to bottom, rgba(8, 9, 15, 0.144), rgba(17, 15, 17, 0.788));
    background-size: cover;
    color: white;
    border-radius: 10%;
    position: absolute;
    bottom:-2px;
    left: -2px;
}  */
h3{
    font-family: 'Lora', serif;
  }
@media only screen and (max-width: 768px) {
  .md-font-size {
    font-size: 22px;
  }
  
}
.clip-text {
  white-space: nowrap;
  text-overflow: ellipsis;
  overflow-x: hidden;
  overflow-y: hidden;
  max-width: 95%;
}
</style>