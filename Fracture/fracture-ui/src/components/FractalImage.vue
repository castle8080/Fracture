<template>
  <img
    src="data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw=="
    class="fracture-image"
    ref="image"
    v-on:click="onImageClick"
  >
</template>
<script>
export default {
  name: 'FractalImage',
  created: function () {
    console.log(this.$store.image);
    this.loadImage();
  },

  methods: {

    onImageClick(e) {
      console.log(e);
      var x = e.offsetX;
      var y = e.offsetY;
    },

    loadImage() {
      var xhr = new XMLHttpRequest();
      xhr.open("GET", "api/image", true);
      xhr.responseType = "blob";
      xhr.onload = e => this.imageRequestCallback(e);
      xhr.send();
    },

    imageRequestCallback(e) {
      var xhr = e.target;
      if (xhr.readyState == 4) {
        if (xhr.status == 200) {
          var props = JSON.parse(xhr.getResponseHeader("X-GeneratedImageProperties"));
          var image = xhr.response;
          this.onImageRetrieved(image, props);
        }
        else {
          console.log("Failed to load image: ", xhr.status);
        }
      }
    },

    onImageRetrieved(image, props) {
      console.log("Image: ", props, image);
      this.$refs.image.src = URL.createObjectURL(image);
    }
  }
}
</script>