<template>
 <div class="keep-modal container-fluid">
    <!-- Modal -->
    <div class="modal"
         id="keep-modal"
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
              <div class="col-6">
                <h3 class="text-wrap text-break">{{keep.name}}</h3>
                <p class="text-left text-wrap text-break">{{keep.description}}</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup(){
    return {}
  },
  components:{}
}
</script>


<style lang="scss" scoped>

</style>