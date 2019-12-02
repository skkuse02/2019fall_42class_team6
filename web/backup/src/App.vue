<template>
  <div id="app">
    <IRHeader v-bind:propsstate="LoginState"></IRHeader>
    <router-view class="body"></router-view>
  </div>
</template>

<script>
import IRHeader from './components/IRHeader.vue'
//import IRBody from './components/IRBody.vue'

export default {
  data(){
    return{
      LoginState: [ true ],
      UserInfo: {}
    }
  },
  /*created() {
    if(SessionStorage.length > 0){
      for(var i=0; i < SessionStorage.length; i++){
        // 세션스토리지 데이터를 뷰 데이터에 저장
        this.UserInfo[SessionStorage.key(i)] = SessionStorage.value(i));
      }
    }
  },*/
  methods: {

  },
  components: {
    IRHeader: IRHeader,
  }
}
</script>

<style>
  body{
    margin-top: 100px;
    text-align: center;
    background-color: #F6F6F8;
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
