<template lang="html">
  <div class="form">
    <h2>Log In</h2>
    <br>
    <b-form v-on:submit="onSubmit">
      <b-form-group
        id="input-group-1"
        label="ID:"
        label-for="input-1"
        description="ID is 5-12 characters long."
      >
        <b-form-input
          id="input-1"
          v-model="form.id"
          required
          placeholder="Enter ID"
        ></b-form-input>
      </b-form-group>

      <b-form-group
        id="input-group-2"
        label="Password:"
        label-for="input-2"
        description="Password is 8-20 characters long."
      >
        <b-form-input
          id="input-2"
          v-model="form.pw"
          type="password"
          required
          placeholder="Enter Password"
        ></b-form-input>
      </b-form-group>

      <b-button type="submit" variant="warning">Log In</b-button>
      <b-button v-on:click="moveTo" variant="primary">Sign Up</b-button>
    </b-form>

    <b-card class="mt-3" header="Form Data Result">
      <pre class="m-0">{{ form }}</pre>
    </b-card>
  </div>
</template>

<script>
export default {
  data() {
    return {
      form: {
        id: '',
        pw: ''
      },
    }
  },
  methods: {
    onSubmit(evt) {
      evt.preventDefault()
      // 추후 DB로 보내는 식으로 수정
      console.log("로그인")
      let data = {
        headers: { 'Content-type': 'application/x-www-form-urlencoded' },
        function: 'CheckLogin',
        user_id: this.form.id,
        password: this.form.pw
      }
      this.$store.dispatch('login', data)
      .then(() => {this.$router.push("/"); this.$router.go();})
      .catch(err => console.log(err))
    },
    moveTo() {
      this.$router.push('/signup')
    }
  }
}
</script>

<style lang="css" scoped>
  .form{
    display: block;
    margin: 0 5rem;
    text-align: left;
  }
</style>
