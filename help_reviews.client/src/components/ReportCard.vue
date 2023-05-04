<template>
  <div class="elevation-5 p-3">
    <div class="d-flex justify-content-between align-items-center">
      <div class="d-flex">
        <img :src="report.creator.picture" :alt="report.creator.name + ' picture'" class="">
        <div class="p-2 ms-1">
          <p>{{ report.creator.name }}</p>
          <p class="bg-success rounded-pill px-3">Rating: {{ report.rating }}</p>
        </div>
      </div>
      <div>{{ report.createdAt.toLocaleDateString() }}</div>
    </div>
    <h3 class="fs-4">"{{ report.title }}"</h3>
    <p>{{ report.body }}</p>
  </div>
</template>


<script>
import { Report } from '../models/Report.js';

export default {
  props: {
    report: { type: Report, required: true }
  },
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>
img {
  height: 10vh;
  width: 10vh;
  border-radius: 40%;
}
</style>