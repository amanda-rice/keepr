<template>
  <div class="py-2">
      <img class="w-100 img-height img-rounded" :src="vault.img" :alt="vault.name" :title="vault.name">
      <div class="">
        <h5 class="img-text text-break w-100 text-wrap pl-2 pr-3 text-light">{{vault.name}}</h5>
      </div>
  </div>
</template>


<script>
export default {
  props: {
    vault:{
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
  object-fit: cover;
}
</style>