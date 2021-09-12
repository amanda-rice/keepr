<template>
    <div class="card img-rounded">
      <div class="card-body p-0">
        <div class="img-total">
          <img class="w-100 img-rounded selectable" :src="keep.img" :alt="keep.name" :title="keep.name" data-toggle="modal" 
          :data-target="'#keep-modal-'+keep.id" @click="getKeep">
          <div class="img-text d-flex align-items-center w-100 pr-2">
            <h5 class="text-light w-100 text-center text-break text-wrap pl-2">{{keep.name}}</h5>
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
          await keepsService.getById(props.keep.id, route.params.id)
        } catch (error) {
          Pop.toast
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
 /* .bg-gradient {
    background-image:
    linear-gradient(to bottom, rgba(8, 9, 15, 0.178), rgba(17, 15, 17, 0.637));
    background-size: cover;
    color: white;
    border-radius: 2%;
}  */
</style>