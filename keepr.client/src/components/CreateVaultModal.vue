<template>
 <div class="vault-modal container-fluid">
    <!-- Modal -->
    <div class="modal"
         id="create-vault-modal"
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
            <form @submit.prevent="createVault">
            <div class="form-group">
              <label class="pr-2" for="name">Vault Name</label>
              <input type="text"
                     id="name"
                     class="form-control"
                     placeholder="Vault Name..."
                     maxlength="25"
                     required
                     v-model="state.createdVault.name"
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
                     v-model="state.createdVault.description"
              >
            </div>
            <div class="form-group">
              <label class="pr-2" for="class">Image URL</label>
              <input type="text"
                     id="img-url"
                     class="form-control"
                     placeholder="Image URL..."
                     maxlength="200"
                     required
                     v-model="state.createdVault.img"
              >
            </div>
            <div class="form-group">
                <label for="private" class="form-label">Private</label>
                <input class="form-checkbox" type="checkbox" v-model="state.createdVault.private">
              </div>
            <div>
              <button v-if="state.createdVault" type="submit" class="btn btn-primary mr-3">
                Save
              </button>
              <button type="button" class="btn btn-secondary closeModal" data-dismiss="modal" @click="closeModal">
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
import { vaultsService } from '../services/VaultsService'
import $ from 'jquery'

export default {
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup(props){
    const route = useRoute()
    const state = reactive({
      createdVault: {},
      account: computed(() => AppState.account)
    })
    return {
      state,
      async createVault(){
        try {
          state.createdVault.creatorId = state.account.id
          await vaultsService.createVault(state.createdVault)
          state.createdVault = {}
          $("create-vault-modal").modal('hide')
            Pop.toast('Created Vault Successfully', 'success')
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      closeModal(){
        $("create-vault-modal").modal('hide')
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