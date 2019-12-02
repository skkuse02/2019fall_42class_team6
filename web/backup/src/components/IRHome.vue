<template lang="html">

  <div>
    <div class="backgroundDiv">
      <b-carousel class="limitedWidth"
        id="carousel-1"
        v-model="slide"
        :interval="3000"
        controls
        indicators
        background="#ababab"
        img-width="1024"
        img-height="550"
        style="text-shadow: 1px 1px 2px #333;"
        @sliding-start="onSlideStart"
        @sliding-end="onSlideEnd"
      >
        <!-- Text slides with image -->
        <b-carousel-slide
          caption="코비1"
          text="일단 임의로 넣어뒀습니다."
          img-src="src/assets/kovi1.png"
        ></b-carousel-slide>

        <b-carousel-slide
          caption="코비2"
          text="일단 임의로 넣어뒀습니다."
          img-src="src/assets/kovi2.png"
        ></b-carousel-slide>

        <!-- Slide with blank fluid image to maintain slide aspect ratio -->
        <b-carousel-slide caption="Blank Image" img-blank img-alt="Blank image">
          <p>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse eros felis, tincidunt
            a tincidunt eget, convallis vel est. Ut pellentesque ut lacus vel interdum.
          </p>
        </b-carousel-slide>
      </b-carousel>
    </div>
    <br>
    <p>적당히 쓸 내용을 생각해봅시다.</p>
  </div>
</template>

<script>
export default {
  data() {
    return {
      slide: 0,
      sliding: null
    }
  },
  methods: {
    onSlideStart() {
      this.sliding = true
    },
    onSlideEnd() {
      this.sliding = false
    }
  }
}
</script>

<style lang="css" scoped>
  .limitedWidth{
    min-width: 1024px;
    max-width: 60%;
    margin: auto;
  }
  .backgroundDiv{
    background-color: #ababab;
  }
</style>
