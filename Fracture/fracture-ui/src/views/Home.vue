<template>
  <div class="fracture-app">
    The app!
    <FractalImage/>
  </div>
</template>

<script>
// @ is an alias to /src
import FractalImage from '@/components/FractalImage.vue'

export default {
  name: 'home',
  components: {
    FractalImage
  }
}
</script>
