<template>
  <router-link :to="{ name: 'RestaurantDetails', params: { restaurantId: restaurant.id } }">
    <div class="row selectable elevation-5 p-3">
      <div class="col-4">
        <img :src="restaurant.imgUrl" class="img-fluid" alt="">
      </div>
      <div class="col-8 text-dark">
        <div class="d-flex justify-content-between mb-5 align-items-center">
          <h2>{{ restaurant.name }}</h2>
          <span class="fs-1">
            {{ restaurant.reportCount }} <i class="mdi mdi-rodent"></i>
          </span>
        </div>
        <p>
          {{ restaurant.description }}
        </p>
      </div>
    </div>
  </router-link>
</template>


<script>
import { Restaurant } from '../models/Restaurant.js';

export default {
  props: {
    restaurant: { type: Restaurant, required: true }
  },
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>
img {
  height: 30vh;
  width: 100%;
  object-fit: cover;
  object-position: center;
}
</style>