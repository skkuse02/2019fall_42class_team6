<template lang="html">
  <div class="form">
    <h2>Sign Up</h2>
    <br>
    <b-form @submit="onSubmit" @reset="onReset" v-if="show">
      <b-form-group
        id="input-group-1"
        label="ID:"
        label-for="input-1"
      >
        <b-form-input
          id="input-1"
          v-model="form.id"
          required
          placeholder="Enter ID"
          v-bind:state="validation_id"
        ></b-form-input>
        <b-form-invalid-feedback v-bind:state="validation_id">
          Your user ID must be 5-12 characters long.
        </b-form-invalid-feedback>
        <b-form-valid-feedback v-bind:state="validation_id">
          Looks Good.
        </b-form-valid-feedback>
      </b-form-group>

      <b-form-group
        id="input-group-2"
        label="Password:"
        label-for="input-2"
      >
        <b-form-input
          id="input-2"
          v-model="form.pw"
          type="password"
          required
          placeholder="Enter Password"
          v-bind:state="validation_pw"
        ></b-form-input>
        <b-form-invalid-feedback v-bind:state="validation_pw">
          Your password must be 8-20 characters long.
        </b-form-invalid-feedback>
        <b-form-valid-feedback v-bind:state="validation_pw">
          Looks Good.
        </b-form-valid-feedback>
      </b-form-group>

      <b-form-group id="input-group-3" label="Name:" label-for="input-3">
        <b-form-input
          id="input-3"
          v-model="form.name"
          required
          placeholder="Enter name"
        ></b-form-input>
      </b-form-group>

      <b-form-group id="input-group-4" label="Address:" label-for="input-4">
        <b-form-input
          id="input-4"
          v-model="form.address"
          required
        ></b-form-input>
      </b-form-group>

      <b-form-group id="input-group-5" label="Email Address:" label-for="input-5">
        <b-form-input
          id="input-5"
          type="email"
          v-model="form.email"
        ></b-form-input>
      </b-form-group>

      <b-form-group id="input-group-6">
        <b-form-checkbox-group v-model="form.checked" id="checkboxes-6">
          <b-form-checkbox value="seller">I'm a seller</b-form-checkbox>
        </b-form-checkbox-group>
      </b-form-group>

      <b-button type="submit" variant="primary">Register</b-button>
      <b-button type="reset" variant="secondary">Reset</b-button>
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
        pw: '',
        name: '',
        address: '',
        email: '',
        checked: []
      },
      show: true
    }
  },
  computed: {
    validation_id() {
      var id_len = this.form.id.length;
      return id_len > 4 && id_len < 13;
    },
    validation_pw() {
      var pw_len = this.form.pw.length;
      return pw_len > 8 && pw_len < 20;
    }
  },
  methods: {
    onSubmit(evt) {
      evt.preventDefault()
      // 추후 DB로 보내는 식으로 수정
      console.log("회원가입")
      let data = {
        headers: { 'Content-type': 'application/x-www-form-urlencoded' },
        function: 'RegisterID',
        user_id: this.form.id,
        password: this.form.pw,
        user_name: this.form.user_name,
        address : this.form.Address,
        email_address: this.form.email_address,
        role: this.form.checked.length > 0 ? 1 : 0
      }
      this.$store.dispatch('register', data)
      .then(() => this.$router.push("/"))
      .catch(err => console.log(err))
    },
    onReset(evt) {
      evt.preventDefault()
      // Reset our form values
      this.form.id = ''
      this.form.pw = ''
      this.form.name = ''
      this.form.address = ''
      this.form.phonenumber = ''
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
