<template>
  <div class="py-2 hover-here">
    <router-link :to="{ name: 'Vault', params: {id: vault.id}}">
      <img class="w-100 img-height gs img-rounded" :src="vault.img" :alt="vault.name" :title="vault.name">
    </router-link>
      <div class="hover-show">
        <h5 class="img-text sm-font-size text-break w-100 text-wrap pl-3 pr-4 text-light">{{vault.name}}</h5>
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
  text-shadow: 2px 2px 10px rgb(0, 0, 0);
}
.img-height{
  height: 200px;
  object-fit: cover;
}
h5{
  font-family: 'Lora', serif;
}
</style>