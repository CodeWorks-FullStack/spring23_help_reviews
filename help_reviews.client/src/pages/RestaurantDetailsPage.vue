<template>
  <div class="container-fluid">
    <div v-if="restaurant" class="row hero-row align-items-center">
      <div class="col-12 ps-4">
        <h1>{{ restaurant.name }}</h1>
      </div>
    </div>
  </div>
</template>


<script>
import { useRoute, useRouter } from 'vue-router';
import { restaurantsService } from '../services/RestaurantsService.js';
import Pop from '../utils/Pop.js';
import { computed, watchEffect } from 'vue';
import { AppState } from '../AppState.js';
import { logger } from '../utils/Logger.js';

export default {
  setup() {
    const route = useRoute()
    const router = useRouter()
    async function getRestaurantById() {
      try {
        const restaurantId = route.params.restaurantId
        await restaurantsService.getRestaurantById(restaurantId)
      } catch (error) {
        let errorMessage = error.response.data
        if (errorMessage == "SOMETHING WENT WRONG 😉") {
          logger.error(error)
          Pop.toast(errorMessage, 'error')
          router.push({ name: 'Home' })
        }
        else {
          Pop.error(error)
        }
      }
    }

    watchEffect(() => {
      route.params.restaurantId
      getRestaurantById()
    })

    return {
      restaurant: computed(() => AppState.activeRestaurant),
      restaurantImg: computed(() => `url(${AppState.activeRestaurant?.imgUrl})`)
    }
  }
}
</script>


<style lang="scss" scoped>
.hero-row {
  height: 40vh;
  background-image: v-bind(restaurantImg);
  background-size: cover;
}

h1 {
  color: whitesmoke;
  text-shadow: 1px 1px 2px black;
  background-color: rgba(80, 80, 80, 0.572);
  display: inline;
  padding: .3em;
  border-radius: 10px;
}
</style>