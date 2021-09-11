<template>
  <div class="">
      <img class="w-100 img-height" :src="keep.img" alt="keep.name" title="keep.name">
      <div class="">
        <h5 class="">{{keep.name}}</h5>
      </div>
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
.img-height{
  height: 200px;
  width:200px;
  background-size: cover;
}
</style>