<template lang="html">
  <div class="infoArea">
    <h2>User Information</h2>
    <b-table stacked :items="infoItems"></b-table>
    <br>
    <b-button class="modifyBtn" v-on:click="moveTo" variant="primary">Modify Information</b-button>
  </div>
</template>

<script>
export default {
  data(){
    return {
      infoItems: [{
        'ID': 'shb0107',
        'Name': '서해빈',
        'Address': '수원시 장안구 화산로 203번길 9-9',
        'Phone Number': '01073073446',
        'Email': 'shb0107@gmail.com',
        'Role': 'seller'
      }]
    }
  },
  methods: {
    moveTo(){
      this.$router.push('/modifyuserinfo');
    }
  }
}
</script>

<style lang="css" scoped>
  .infoArea{
    display: inline-block;
    width: 90%;
    text-align: left;
  }
  .modifyBtn{
    position: absolute;
    right: 10px;
  }
</style>
