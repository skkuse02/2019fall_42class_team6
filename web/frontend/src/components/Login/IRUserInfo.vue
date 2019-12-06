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
        'ID': '',
        'Name': '',
        'Address': '',
        'Email': '',
        'Role': ''
      }]
    }
  },
  created() {
    let info =  JSON.parse(localStorage.getItem('token'))
    this.infoItems[0].ID = info.user_id
    this.infoItems[0].Name = info.user_name
    this.infoItems[0].Address = info.address
    this.infoItems[0].Email = info.email_address
    this.infoItems[0].Role = info.role == 0 ? 'customer' : 'seller'
  },
  /*computed: {
    getUserStatus() {
      let info =  JSON.parse(localStorage.getItem('token'))
      console.log(info)
      return [info]
      //this.infoItems['ID'] = info.user_id
      //this.infoItems['Name'] = info.user_name
      //this.infoItems['Address'] = info.address
      //this.infoItems['Email'] = info.email_address
      //this.infoItems['Role'] = info.role == 0 ? 'customer' : 'seller'
      return this.infoItems
    }
  },*/
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
