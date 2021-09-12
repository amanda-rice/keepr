<template>
 <div class="keep-modal container-fluid">
    <!-- Modal -->
    <div class="modal"
         :id="'keep-modal-'+keep.id"
         tabindex="-1"
         role="dialog"
         aria-labelledby="modelTitleId"
         aria-hidden="true"
    >
      <div class="modal-dialog modal-lg modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header d-flex flex-column align-items-center">
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <div class="row">
              
              <div class="col-6">
                <img class="w-100" :src="keep.img" :alt="keep.name" :title="keep.name">
              </div>
              <div class="col-6 d-flex flex-column flex-grow justify-content-between">
                <div class="d-flex justify-content-around">
                  <p>Views: {{keep.views}}</p>
                  <p>Keeps: {{keep.keeps}}</p>
                  <p>Shares: {{keep.shares}}</p>
                </div>
                <div>
                  <h3 class="text-wrap text-break">{{keep.name}}</h3>
                  <p class="text-left text-wrap text-break">{{keep.description}}</p>
                </div>
                <div class="row d-flex justify-content-around">
                  <div class="col-3">
                    <button>Add to vault</button>
                  </div>
                  <div class="col-2">
                    <i class="fa fa-trash" @click="deleteKeep"></i>
                  </div>
                  <div class="col-5 d-flex">
                    <img class="sm-prof-pic" :src="keep.creator.picture" :alt="keep.creator.name" :title="keep.creator.name">
                    <p class="pl-3 pr-1 text-break text-wrap">{{keep.creator.name}}</p>
                  </div>
                </div>
              </div>
            </div>
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
import { keepsService } from '../services/KeepsService'
import $ from 'jquery'

export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup(props){
    const route = useRoute()
    return {
      async deleteKeep(){
        try {
          if (await Pop.confirm()) {
            const id = props.keep.id
            $('#keep-modal-'+ id).modal('hide')
            await keepsService.deleteKeep(props.keep.id)
            Pop.toast('Deleted Keep Successfully', 'success')
          }
        } catch (error) {
          Pop.toast(error, 'error')
        }
      }}
  },
}
</script>


<style lang="scss" scoped>
  .sm-prof-pic{
    height: 30px;
    width: 30px;
  }
</style>