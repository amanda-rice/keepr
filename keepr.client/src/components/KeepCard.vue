<template>
  <div>
    <div class="card img-rounded">
      <div class="card-body p-0">
        <div class="img-total">
          <img class="w-100 img-rounded selectable" :src="keep.img" :alt="keep.name" :title="keep.name" data-toggle="modal" 
          :data-target="'#keep-modal-'+keep.id" @click="getKeep">
          <div class="img-text d-flex justify-content-between w-100 pl-3 pr-4 pb-2">
            <h3 class="text-light text-break text-wrap">{{keep.name}}</h3>
            <div>
              <router-link :to="{ name: 'Profile', params: {id: keep.creatorId}}">
                <img class="sm-prof-img mt-1  mr-2" :src="keep.creator.picture" :alt="keep.creator.name">
              </router-link>
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
import { useRoute } from 'vue-router'
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
    return {
      async getKeep(){
        try {
          await keepsService.getByIdProf(props.keep.id)
        } catch (error) {
          Pop.toast(error, 'error')
        }
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
  text-shadow: 2px 2px rgba(0, 0, 0, 0.582);
  
}
.sm-prof-img{
  height: 30px;
  width: 30px;
  border-radius: 50%;
}
 .bg-gradient {
    background-image:
    linear-gradient(to bottom, rgba(8, 9, 15, 0.178), rgba(17, 15, 17, 0.637));
    background-size: cover;
    color: white;
    border-radius: 2%;
} 
</style>