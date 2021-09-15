<template>
 <div class="account-modal container-fluid">
    <!-- Modal -->
    <div class="modal"
         id="update-account-modal"
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
            <h3 class="m-0">Edit Account</h3>
          </div>
          <div class="modal-body">
            <form @submit.prevent="updateAccount">
            <div class="form-group">
              <label class="pr-2" for="account-name">Name</label>
              <input type="text"
                     id="account-name"
                     class="form-control"
                     placeholder="Name..."
                     maxlength="25"
                     required
                     v-model="state.updatedAccount.name"
              >
            </div>
            <div class="form-group">
              <label class="pr-2" for="account-img-url">Image URL</label>
              <input type="text"
                     id="account-img-url"
                     class="form-control"
                     placeholder="Image URL..."
                     maxlength="200"
                     required
                     v-model="state.updatedAccount.picture"
              >
            </div>
            <div>
              <button v-if="state.updatedAccount" type="submit" class="btn btn-success mr-3">
                <b>Save</b>
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
import { accountService } from '../services/AccountService'
import $ from 'jquery'

export default {
  setup(){
    const route = useRoute()
    const state = reactive({
      updatedAccount: {},
      account: computed(() => AppState.account)
    })
    return {
      state,
      /**
       * Set the updatedAccount object's id to the user's account id
       * Send object to the service to update the account
       **/
      async updateAccount(){
        try {
          state.updatedAccount.id = state.account.id
          await accountService.updateAccount(state.updatedAccount)
          state.updatedAccount = {}
          $("#update-account-modal").modal('hide')
          Pop.toast('Updated Account Successfully', 'success')
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      closeModal(){
        $("#update-account-modal").modal('hide')
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