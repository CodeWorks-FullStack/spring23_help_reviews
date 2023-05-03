<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12 p-5">
        <img
          src="https://images.unsplash.com/photo-1555396273-367ea4eb4db5?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1374&q=80"
          alt="" class="img-fluid hero-img">
      </div>
      <div class="col-12 text-center mb-3">
        <h1>Restaurants</h1>
      </div>
    </div>
    <div class="row">
      <div v-for="r in restaurants" :key="r.id" class="col-12 col-md-10 m-auto mb-3">
        <RestaurantCard :restaurant="r" />
      </div>
    </div>
  </div>
</template>

<script>
import Pop from '../utils/Pop.js';
import { restaurantsService } from '../services/RestaurantsService.js'
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import RestaurantCard from '../components/RestaurantCard.vue';

export default {
  setup() {
    async function getAllRestaurants() {
      try {
        await restaurantsService.getAllRestaurants();
      }
      catch (error) {
        Pop.error(error);
      }
    }
    onMounted(() => {
      getAllRestaurants();
    });
    return {
      restaurants: computed(() => AppState.restaurants)
    };
  },
  components: { RestaurantCard }
}
</script>

<style scoped lang="scss">
.hero-img {
  max-height: 40vh;
  width: 100%;
  object-fit: cover;
  border: 3px solid black;
  box-shadow: 0 2px 5px black;
}
</style>
