<template lang="html">
  <div>
    <b-navbar toggleable="lg" type="light" variant="light" fixed="top">
      <img class="logo" src="../assets/logo2.png" @click="moveTo('/')">
      <b-navbar-brand class="ServiceName" @click="moveTo('/')">InteReal</b-navbar-brand>

      <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

      <b-collapse id="nav-collapse" is-nav>
        <!-- Right aligned nav items -->
        <b-navbar-nav class="ml-auto">
          <b-nav-item href="#">Download</b-nav-item>
          <b-nav-item-dropdown text="User" right v-if="propsstate[0]">
            <b-dropdown-item v-on:click="moveTo('/userinfo')">Account Information</b-dropdown-item>
            <b-dropdown-item v-on:click="moveTo('/paymentmethod')">Payment Methods</b-dropdown-item>
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
  props: [ 'propsstate' ],
  methods: {
    moveTo(path){
      this.$router.push(path);
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
