<template>
 <div class="vault-modal container-fluid">
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
            <h3 class="m-0">Create Vault</h3>
          </div>
          <div class="modal-body">
            <form @submit.prevent="createVault">
            <div class="form-group">
              <label class="pr-2" for="vault-name">Vault Name</label>
              <input type="text"
                     id="vault-name"
                     class="form-control"
                     placeholder="Vault Name..."
                     maxlength="25"
                     required
                     title = "Vault Name"
                     v-model="state.createdVault.name"
              >
            </div>
            <div class="form-group">
              <label class="pr-2" for="vault-description">Description</label>
              <input type="text"
                     id="vault-description"
                     class="form-control"
                     placeholder="Description..."
                     maxlength="500"
                     required
                     title ="Vault Description"
                     v-model="state.createdVault.description"
              >
            </div>
            <div class="form-group">
              <label class="pr-2" for="vault-img">Image URL</label>
              <input type="text"
                     id="vault-img"
                     class="form-control"
                     placeholder="Image URL..."
                     maxlength="200"
                     required
                     title="Vault Image URL"
                     v-model="state.createdVault.img"
              >
            </div>
            <div class="form-group d-flex align-items-center justify-content-start">
                <label for="private" class="form-label pr-2 m-0">Private?</label>
                <input class="form-checkbox m-0" type="checkbox" v-model="state.createdVault.private" title="Set vault to private">
              </div>
            <div>
              <button v-if="state.createdVault" type="submit" class="btn btn-success mr-3" title="Create New Vault">
                <b>Create</b>
              </button>
              <button type="button" class="btn btn-primary closeModal" title="Close Modal" data-dismiss="modal" @click="closeModal">
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
      /**
       * Users can create new vaults
       **/
      async createVault(){
        try {
          state.createdVault.creatorId = state.account.id
          await vaultsService.createVault(state.createdVault)
          state.createdVault = {}
          $("#create-vault-modal").modal('hide')
          Pop.toast('Created Vault Successfully', 'success')
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      closeModal(){
        $("#create-vault-modal").modal('hide')
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
  h3{
    font-family: 'Lora', serif;
  }  
</style>