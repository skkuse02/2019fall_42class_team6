<template lang="html">
  <div class="form">
    <h2>Payment Methods</h2>
    <br>
    <b-form @submit="onSubmit" @reset="onReset" v-if="show">
      <b-form-group
        id="input-group-1"
        label="CardNumber:"
        label-for="input-1"
      >
        <b-form-input
          id="input-1"
          type="number"
          v-model="form.cardnumber"
          required
          placeholder="Enter Card Number"
          v-bind:state="validation_cn"
        ></b-form-input>
        <b-form-invalid-feedback v-bind:state="validation_cn">
          Card Number must be 15 or 16 characters long.
        </b-form-invalid-feedback>
      </b-form-group>

      <label>Expiration Date:</label>
      <b-form inline>
        <b-form-group
          id="input-group-2"
          label="Month:"
          label-for="input-2"
        >
          <b-form-select
            id="input-2"
            v-model="form.month"
            :options="options_month"
            required
            class="ml-sm-2 mr-sm-2"
          ></b-form-select>
        </b-form-group>

        <b-form-group
          id="input-group-3"
          label="Year:"
          label-for="input-3"
        >
          <b-form-select
            id="input-3"
            v-model="form.year"
            :options="options_year"
            required
            class="ml-sm-2 mr-sm-2"
          ></b-form-select>
        </b-form-group>
      </b-form>

      <b-form-group
        id="input-group-4"
        label="CVC:"
        label-for="input-4"
      >
        <b-form-input
          id="input-4"
          type="number"
          v-model="form.cvc"
          required
          placeholder="Enter CVC Number"
          v-bind:state="validation_cvc"
        ></b-form-input>
        <b-form-invalid-feedback v-bind:state="validation_cvc">
          CVC Number must be 3 or 4 numbers long.
        </b-form-invalid-feedback>
      </b-form-group>

      <b-form-group
        id="input-group-5"
        label="Password:"
        label-for="input-5"
      >
        <b-form-input
          id="input-5"
          type="number"
          v-model="form.pw"
          required
          placeholder="Enter Password"
          v-bind:state="validation_pw"
        ></b-form-input>
        <b-form-invalid-feedback v-bind:state="validation_pw">
          Password must be 4 numbers long.
        </b-form-invalid-feedback>
      </b-form-group>

      <b-button type="submit" variant="primary">Register</b-button>
      <b-button type="reset" variant="secondary">Reset</b-button>
    </b-form>
  </div>
</template>

<script>
export default {
  data(){
    return{
      form: {
        cardnumber: '',
        month: null,
        year: null,
        cvc: '',
        pw: ''
      },
      show: true,
      options_month: [
        { value: null, text: 'Select the month' },
        { value: 1, text: '1' },
        { value: 2, text: '2' },
        { value: 3, text: '3' },
        { value: 4, text: '4' },
        { value: 5, text: '5' },
        { value: 6, text: '6' },
        { value: 7, text: '7' },
        { value: 8, text: '8' },
        { value: 9, text: '9' },
        { value: 10, text: '10' },
        { value: 11, text: '11' },
        { value: 12, text: '12' }
      ],
      options_year: [
        { value: null, text: 'Select the year' },
        { value: 2019, text: '2019' },
        { value: 2020, text: '2020' },
        { value: 2021, text: '2021' },
        { value: 2022, text: '2022' },
        { value: 2023, text: '2023' },
        { value: 2024, text: '2024' },
        { value: 2025, text: '2025' }
      ]
    }
  },
  computed: {
    validation_cn() {
      var len = this.form.cardnumber.length;
      return len > 14 && len < 17;
    },
    validation_cvc() {
      var len = this.form.cvc.length;
      return len > 2 && len < 5;
    },
    validation_pw() {
      var len = this.form.pw.length;
      return len == 4;
    }
  },
  methods: {
    onSubmit(evt) {
      evt.preventDefault()
      // 추후 DB로 보내는 식으로 수정
      alert(JSON.stringify(this.form))
    },
    onReset(evt) {
      evt.preventDefault()
      // Reset our form values
      this.form.cardnumber = ''
      this.form.month = null
      this.form.year = null
      this.form.cvd = ''
      this.form.pw = ''
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
    display: inline-block;
    width: 90%;
    text-align: left;
  }
</style>
