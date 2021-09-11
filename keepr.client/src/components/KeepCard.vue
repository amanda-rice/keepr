<template>
  <div>
    <div class="card img-rounded">
      <div class="card-body p-0">
        <div class="img-total">
          <img class="w-100 img-rounded" :src="keep.img" alt="keep.name" title="keep.name">
          <div class="img-text d-flex justify-content-between w-100 pl-2 pr-4">
            <h2 class="text-light text-break text-wrap">{{keep.name}}</h2>
            <div>
              <router-link :to="{ name: 'Profile', params: {id: keep.creatorId}}">
                <img class="sm-prof-img mt-2" :src="keep.creator.picture" :alt="keep.creator.name">
              </router-link>
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
.sm-prof-img{
  height: 30px;
  width: 30px;
}
 .bg-gradient {
    background-image:
    linear-gradient(to bottom, rgba(8, 9, 15, 0.178), rgba(17, 15, 17, 0.637));
    background-size: cover;
    color: white;
    border-radius: 2%;
} 
</style>