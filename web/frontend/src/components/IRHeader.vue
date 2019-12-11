<template lang="html">
  <div>
    <b-navbar toggleable="lg" type="light" variant="light" fixed="top">
      <img class="logo" src="../assets/logo2.png" @click="moveTo('/')">
      <b-navbar-brand class="ServiceName" @click="moveTo('/')">InteReal</b-navbar-brand>

      <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

      <b-collapse id="nav-collapse" is-nav>
        <!-- Right aligned nav items -->
        <b-navbar-nav class="ml-auto">
          <b-nav-item v-on:click="downloadfile" v-if="isLoggedIn">Download</b-nav-item>
          <b-nav-item :href="specifyroomPath" v-if="isLoggedIn">Run SpecifyRoom</b-nav-item>
          <b-nav-item-dropdown text="User" right v-if="isLoggedIn">
            <b-dropdown-item v-on:click="moveTo('/userinfo')">Account Information</b-dropdown-item>
            <b-dropdown-item v-on:click="moveTo('/paymentmethod')">Payment Methods</b-dropdown-item>
            <b-dropdown-item v-on:click="logout">Log out</b-dropdown-item>
          </b-nav-item-dropdown>
          <b-nav-item-dropdown text="User" right v-else>
            <b-dropdown-item v-on:click="moveTo('/signup')">Sign UP</b-dropdown-item>
            <b-dropdown-item v-on:click="moveTo('/login')">Log In</b-dropdown-item>
          </b-nav-item-dropdown>
        </b-navbar-nav>
      </b-collapse>
    </b-navbar>
  </div>
</template>

<script>
export default {
  computed: {
    isLoggedIn() {
      return this.$store.getters.isLoggedIn
    },
    specifyroomPath(){
      return "specifyroom://" + JSON.parse(localStorage.getItem('userToken')).user_id
    }
  },
  methods: {
    logout() {
      this.$store.dispatch('logout')
      .then(() => this.$router.push('/login'))
    },
    moveTo(path){
      this.$router.push(path).catch(err => {});
    },
    downloadfile() {
      /*
      this.$http.get('/setup.zip')
      .then(resp => {console.log(resp);})
      .catch(err => {
        console.log(err)
      })*/
      this.$http.get('/setup.zip', {responseType: 'blob'})
      .then((response) => {
        var fileURL = window.URL.createObjectURL(new Blob([response.data]));
        var fileLink = document.createElement('a');

        fileLink.href = fileURL;
        fileLink.setAttribute('download', 'setup.zip');
        document.body.appendChild(fileLink);

        fileLink.click();
      }).catch(err => console.log(err))
    }
  }
}
</script>

<style lang="css" scoped>
  .logo{
    width: 80px;
    margin-right: 1rem;
  }
  .ServiceName{
    font-size: 2rem;
  }
</style>
