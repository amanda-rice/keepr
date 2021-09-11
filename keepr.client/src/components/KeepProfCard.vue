<template>
    <div class="card img-rounded">
      <div class="card-body p-0">
        <div class="img-total">
          <img class="w-100 img-rounded" :src="keep.img" :alt="keep.name" :title="keep.name" data-toggle="modal" 
          data-target="#keep-modal">
          <div class="img-text d-flex align-items-center w-100 pr-2">
            <h5 class="text-light w-100 text-center text-break text-wrap pl-2">{{keep.name}}</h5>
          </div>
        </div>
      </div>
      <KeepModal v-if="keep.id" :keep="keep" />
    </div>
</template>


<script>
export default {
  props: {
    keep:{
      type:Object,
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