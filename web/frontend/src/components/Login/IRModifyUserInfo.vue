<template lang="html">
  <div class="form">
    <h2>Modify User Information</h2>
    <br>
    <b-form @submit="onSubmit" @reset="onReset" v-if="show">
      <b-form-group id="input-group-4" label="Password:" label-for="input-4">
        <b-form-input
          id="input-4"
          v-model="form.pw"
          type="password"
          required
        ></b-form-input>
      </b-form-group>

      <b-form-group id="input-group-5" label="Address:" label-for="input-5">
        <b-form-input
          id="input-5"
          v-model="form.address"
          required
        ></b-form-input>
      </b-form-group>

      <b-form-group id="input-group-6" label="Email Address:" label-for="input-6">
        <b-form-input
          id="input-6"
          type="email"
          v-model="form.email"
        ></b-form-input>
      </b-form-group>

      <b-form-group id="input-group-7">
        <b-form-checkbox-group v-model="form.checked" id="checkboxes-7">
          <b-form-checkbox value="seller">I'm a seller</b-form-checkbox>
        </b-form-checkbox-group>
      </b-form-group>

      <b-button type="submit" variant="primary">Modify</b-button>
      <b-button type="reset" variant="secondary">Reset</b-button>
    </b-form>
  </div>
</template>

<script>
export default {
  data() {
    return {
      form: {
        pw: '',
        address: '',
        email: '',
        checked: []
      },
      show: true
    }
  },
  methods: {
    onSubmit(evt) {
      evt.preventDefault()
      // 추후 DB로 보내는 식으로 수정
      console.log("정보수정")
      let data = {
        headers: { 'Content-type': 'application/x-www-form-urlencoded' },
        function: 'ModifyInfo',
        user_id: JSON.parse(localStorage.getItem('userToken')).user_id,
        password: this.form.pw,
        user_name: JSON.parse(localStorage.getItem('userToken')).user_name,
        address: this.form.address,
        email_address: this.form.email,
        role: this.form.checked.length > 0? 1 : 0
      }
      this.$store.dispatch('modifyUserInfo', data)
      .then(() => this.$router.push("/userinfo"))
      .catch(err => console.log(err))
    },
    onReset(evt) {
      evt.preventDefault()
      // Reset our form values
      this.form.pw = ''
      this.form.address = ''
      this.form.email = ''
      this.form.checked = []
      // Trick to reset/clear native browser form validation state
      this.show = false
      this.$nextTick(() => {
        this.show = true
      })
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
