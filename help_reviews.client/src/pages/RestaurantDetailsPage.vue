<template>
  <div v-if="restaurant" class="container-fluid">
    <div class="row hero-row align-items-center">
      <div class="col-12 ps-4">
        <h1>{{ restaurant.name }}</h1>
      </div>
    </div>
    <div class="row">
      <div class="col-12 ps-5 mb-4">
        <div class="d-flex justify-content-between align-items-center">
          <h2 class="mb-3">Reports:</h2>
          <!-- NOTE the forbidden marquee -->
          <!-- <marquee v-for="n in restaurant.reportCount * 10" class="biz" behavior="alternate" direction="up"
            :scrollamount="Math.random() * 50">
            <marquee direction="right" :scrollamount="Math.random() * 20" behavior="alternate" class="fs-2">
              <i class="mdi mdi-rodent"></i>
            </marquee>
          </marquee> -->
          <div class="fs-2">
            <i v-for="n in restaurant.reportCount" class="mdi mdi-rodent"></i>
          </div>
        </div>
        <button class="btn btn-success">Write a report</button>
      </div>
    </div>
    <div class="row">
      <div v-for="r in reports" :key="r.id" class="col-12 col-md-6 p-5">
        <ReportCard :report="r" />
      </div>
    </div>
  </div>
</template>


<script>
import { useRoute, useRouter } from 'vue-router';
import { restaurantsService } from '../services/RestaurantsService.js';
import { reportsService } from '../services/ReportsService.js';
import Pop from '../utils/Pop.js';
import { computed, watchEffect } from 'vue';
import { AppState } from '../AppState.js';
import { logger } from '../utils/Logger.js';
import ReportCard from '../components/ReportCard.vue';

export default {
  setup() {
    const route = useRoute();
    const router = useRouter();
    async function getRestaurantById() {
      try {
        const restaurantId = route.params.restaurantId;
        await restaurantsService.getRestaurantById(restaurantId);
      }
      catch (error) {
        let errorMessage = error.response.data;
        if (errorMessage == "SOMETHING WENT WRONG 😉") {
          logger.error(error);
          Pop.toast(errorMessage, "error");
          router.push({ name: "Home" });
        }
        else {
          Pop.error(error);
        }
      }
    }
    async function getReportsForRestaurant() {
      try {
        const restaurantId = route.params.restaurantId;
        await reportsService.getReportsForRestaurant(restaurantId);
      }
      catch (error) {
        Pop.error(error);
      }
    }
    watchEffect(() => {
      route.params.restaurantId;
      getRestaurantById();
      getReportsForRestaurant();
    });
    return {
      restaurant: computed(() => AppState.activeRestaurant),
      restaurantImg: computed(() => `url(${AppState.activeRestaurant?.imgUrl})`),
      reports: computed(() => AppState.reports)

    };
  },
  components: { ReportCard }
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

.biz {
  height: 100vh;
  width: 100vw;
  position: fixed;
}
</style>