<template>
  <img
    src="data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw=="
    class="fracture-image"
    ref="image"
    v-on:click="onImageClick"
  >
</template>
<script>
import _ from 'lodash'
import CoordinateTransformer from '../util/CoordinateTransformer'

export default {

  name: 'FractalImage',
  
  data: function () {
    return {
      imageProperties: {
        PixelWidth: 400,
        PixelHeight: 400,
        OriginX: -0.75,
        OriginY: 0,
        LogicalWidth: 3
      }
    };
  },

  created: function () {
    console.log(this.$store);
    this.loadImage();
  },

  methods: {

    getCoordinateTransformer() {
        return CoordinateTransformer.createPixelTransformer(
          this.imageProperties.PixelWidth, this.imageProperties.PixelHeight,
          this.imageProperties.OriginX, this.imageProperties.OriginY,
          this.impageProperties.LogicalWidth);
    },

    onImageClick(e) {
      console.log(e);
      var args = {};
      if (this.imageProperties != null)
      {
        // TODO: Add shift of origin here as well.
        args = _.assign({}, this.imageProperties);

        var ct = this.getCoordinateTransformer();


        args.LogicalWidth = args.LogicalWidth * 0.75;
      }
      this.loadImage(args);
      console.log(args);
    },

    loadImage(args) {
      if (!args) {
        args = {};
      }
      var uri = "api/image?" +
        _.map(args, (v,k) => encodeURIComponent(k) + "=" + encodeURIComponent(v))
        .join("&");

      console.log("About to load: ", uri);

      var xhr = new XMLHttpRequest();
      xhr.open("GET", uri, true);
      xhr.responseType = "blob";
      xhr.onload = e => this.imageRequestCallback(e);
      xhr.send();
    },

    imageRequestCallback(e) {
      // I couldn't read HTTP response headers using the fetch API.
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
      this.imageProperties = props;
      this.$refs.image.src = URL.createObjectURL(image);
    }
  }
}
</script>