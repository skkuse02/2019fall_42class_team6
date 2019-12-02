<template>
  <div id="app">
    <IRHeader></IRHeader>
    <router-view class="body"></router-view>
  </div>
</template>

<script>
import IRHeader from './components/IRHeader.vue'
//import IRBody from './components/IRBody.vue'

export default {
  components: {
    IRHeader: IRHeader,
  }
}
</script>

<style>
  body{
    margin-top: 100px !important;
    text-align: center !important;
    background-color: #F6F6F8 !important;
  }
  input{
    border-style: groove;
    width: 200px;
  }
  button{
    border-style: groove;
  }
  .shadow{
    box-shadow: 5px 10px 10px rgba(0, 0, 0, 0.03);
  }
</style>
