<template>
 <div class="keep-modal container-fluid">
    <!-- Modal -->
    <div class="modal"
         id="create-keep-modal"
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
            <form @submit.prevent="createKeep">
            <div class="form-group">
              <label class="pr-2" for="name">Keep Name</label>
              <input type="text"
                     id="name"
                     class="form-control"
                     placeholder="Keep Name..."
                     maxlength="25"
                     required
                     v-model="state.createKeep.name"
              >
            </div>
            <div class="form-group">
              <label class="pr-2" for="class">Description</label>
              <input type="text"
                     id="description"
                     class="form-control"
                     placeholder="Description..."
                     maxlength="500"
                     required
                     v-model="state.createKeep.description"
              >
            </div>
            <div class="form-group">
              <label class="pr-2" for="class">Image URL</label>
              <input type="text"
                     id="description"
                     class="form-control"
                     placeholder="Image URL..."
                     maxlength="200"
                     required
                     v-model="state.createKeep.img"
              >
            </div>
            <div>
              <button v-if="state.createKeep" type="submit" class="btn btn-success mr-3">
                Save
              </button>
              <button type="button" class="btn btn-primary closeModal" data-dismiss="modal" @click="closeModal">
                Close
              </button>
            </div>
          </form>
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
    const state = reactive({
      createKeep: {},
      account: computed(() => AppState.account)
    })
    return {
      state,
      async createKeep(){
        try {
          state.createKeep.creatorId = state.account.id
          await keepsService.createKeep(state.createKeep)
          state.createKeep = {}
          $("create-keep-modal").modal('hide')
          Pop.toast('Created Keep Successfully', 'success')
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      closeModal(){
        $("create-keep-modal").modal('hide')
      }
    }
  },
}
</script>


<style lang="scss" scoped>
  .sm-prof-pic{
    height: 30px;
    width: 30px;
  }
</style>