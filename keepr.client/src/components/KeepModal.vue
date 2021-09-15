<template>
 <div class="keep-modal container-fluid def-pointer">
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
          <div class="modal-body body-scroll">
            <div class="row">
              
              <div class="col-md-6">
                <img class="w-100 max-image" :src="keep.img" :alt="keep.name" :title="keep.name">
              </div>
              <div class="col-md-6 d-flex pt-2 flex-column flex-grow justify-content-between">
                <div class="d-flex flex-column">
                <div class="d-flex justify-content-around">
                  <p><i class="far fa-eye pr-2" title="views"></i> {{keep.views}}</p>
                  <p><i class="far fa-bookmark pr-2" title="keeps"></i> {{keep.keeps}}</p>
                  <div v-if="account.id && account.id == keep.creatorId">
                    <p><i class="fas fa-share-alt pr-2 hoverable" title="shares" @click="shareKeep"></i> {{keep.shares}}</p>
                  </div>
                  <div v-else>
                    <p><i class="fas fa-share-alt pr-2" title="shares"></i> {{keep.shares}}</p>
                  </div>
                </div>
                <div>
                  <div class="d-flex justify-content-center align-items-center pb-3 pt-2">
                    <h3 class="text-wrap text-break m-0">{{keep.name}}</h3>
                  </div>
                  <p class="text-left text-wrap text-break">{{keep.description}}</p>
                </div>
                </div>
                <div class="row d-flex justify-content-around align-items-end">
                  <div v-if="account.id" class="col-lg-7 d-flex">
                    <select class="hoverable" v-model="state.vaultKeep.vaultId" @change="addVault">
                      <option class="hoverable" v-for="(value, key) in state.vaults" :key="key" :value="value.id">
                        {{ value.name }}
                      </option>
                    </select>
                    <i v-if="keep.creatorId === account.id" class="fa fa-trash text-warning pl-3 fa-mg hoverable" title="Delete Keep" @click="deleteKeep"></i>
                  </div>
                  <div class="col-lg-5 d-flex align-items-end justify-content-end p-2">
                    <router-link :to="{ name: 'Profile', params: {id: keep.creatorId}}" class="nav-link text-dark-grey">
                      <img class="rounded sm-prof-pic" :src="keep.creator.picture" :alt="keep.creator.name" :title="keep.creator.name" @click="closeModal">
                    </router-link>
                    <p class="pl-3 m-0 pr-1 text-ellipses text-truncate text-right" :title="keep.creator.name"><i>{{keep.creator.name}}</i></p>
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

import { computed, onMounted, reactive, watchEffect } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import { router } from '../router'
import Pop from '../utils/Notifier'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import { vaultKeepsService } from '../services/VaultKeepsService'
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
      vaultKeep: {},
      account: computed(() => AppState.account),
      vaults: computed(()=> AppState.selProfVaults)
    })
    return {
      state,
      route,
      /**
       * Users can add any keeps to their vaults that don't already contain that keep
       **/
      async addVault(){
        try {
          const routeName = route.name
          const id = props.keep.id
          $('#keep-modal-'+ id).modal('hide')
          state.vaultKeep.creatorId = state.account.id
          state.vaultKeep.keepId = props.keep.id
          const message = await vaultKeepsService.create(state.vaultKeep, routeName.toUpperCase(), route.params.id)
          state.vaultKeep = {}
          Pop.toast('Added to Vault', 'success')
        } catch (error) {
          Pop.toast(`Can't add Keep to this Vault` , 'error')
        }
      },
      /**
       * Deletes user's keep and closes the modal
       **/
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
      },
      /**
       * When share button is clicked, keep's shares count increases
       **/
      shareKeep(){
        keepsService.shareKeep(props.keep)
      },
      closeModal(){
        $('#keep-modal-'+ props.keep.id).modal('hide')
      },
      account: computed(()=>AppState.account)
    }
  },
}
</script>


<style lang="scss" scoped>
  .def-pointer{
    cursor: default;
  }
  .max-image{
    max-height: 50vh;
    object-fit: cover;
    border-radius: 2%;
  }
  @media only screen and (max-width: 768px) {
  .max-image {
    max-height: 30vh;
  }
}
</style>