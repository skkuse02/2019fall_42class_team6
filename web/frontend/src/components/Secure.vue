<template>
  <div class="securePage">
    <h1>This page is protected by auth</h1>
    <br>
    <b-button class="homeBtn" v-on:click="moveTo" variant="secondary">Go to Home</b-button>
  </div>
</template>

<script>
export default {
  methods: {
    moveTo(){
      this.$router.push('/').catch(err => {});
    }
  }
}
</script>

<style media="screen">
  .securePage{
    display: inline-block;
    text-align: center;
  }
</style>
