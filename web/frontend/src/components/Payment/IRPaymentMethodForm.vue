<template lang="html">
  <div class="form">
    <h3>New Payment Methods</h3>
    <br>
    <b-form @submit="onSubmit" @reset="onReset" v-if="show">
      <b-form-group
        id="input-group-1"
        label="Card Company:"
        label-for="input-1"
      >
        <b-form-select
          id="input-1"
          v-model="form.company"
          :options="options_company"
          required
          class="ml-sm-2 mr-sm-2"
        ></b-form-select>
      </b-form-group>

      <b-form-group
        id="input-group-2"
        label="CardNumber:"
        label-for="input-2"
      >
        <b-form-input
          id="input-2"
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
          id="input-group-3"
          label="Month:"
          label-for="input-3"
        >
          <b-form-select
            id="input-3"
            v-model="form.month"
            :options="options_month"
            required
            class="ml-sm-2 mr-sm-2"
          ></b-form-select>
        </b-form-group>

        <b-form-group
          id="input-group-4"
          label="Year:"
          label-for="input-4"
        >
          <b-form-select
            id="input-4"
            v-model="form.year"
            :options="options_year"
            required
            class="ml-sm-2 mr-sm-2"
          ></b-form-select>
        </b-form-group>
      </b-form>

      <b-form-group
        id="input-group-5"
        label="CVC:"
        label-for="input-5"
      >
        <b-form-input
          id="input-5"
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
        id="input-group-6"
        label="Password:"
        label-for="input-6"
      >
        <b-form-input
          id="input-6"
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
        company: '',
        cardnumber: '',
        month: null,
        year: null,
        cvc: '',
        pw: ''
      },
      show: true,
      options_company: [
        { value: null, text: 'Select the card company' },
        { value: '신한', text: '신한' },
        { value: 'KB국민', text: 'KB국민' },
        { value: '삼성', text: '삼성' },
        { value: '비씨', text: '비씨' },
        { value: '롯데', text: '롯데' },
        { value: '현대', text: '현대' },
        { value: '하나', text: '하나' },
        { value: '하나(외환)', text: '하나(외환)' },
        { value: 'NH채움', text: 'NH채움' },
        { value: '씨티', text: '씨티' },
        { value: '우리', text: '우리' },
        { value: '카카오뱅크', text: '카카오뱅크' },
        { value: '케이뱅크', text: '케이뱅크' },
        { value: '전북', text: '전북' },
        { value: '광주', text: '광주' },
        { value: '제주', text: '제주' },
        { value: '우체국', text: '우체국' },
        { value: '수협', text: '수협' },
        { value: '신협', text: '신협' },
        { value: '새마을금고', text: '새마을금고' },
        { value: '저축은행', text: '저축은행' },
        { value: 'KDB산업', text: 'KDB산업' }
      ],
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
      console.log("결제수단 등록")
      let data = {
        headers: { 'Content-type': 'application/x-www-form-urlencoded' },
        function: 'AddPayment',
        user_id: JSON.parse(localStorage.getItem('userToken')).user_id,
        card_company: this.form.company,
        card_number: this.form.cardnumber,
        valid_month: this.form.month,
        valid_year: this.form.year,
        CVC: this.form.cvc,
        payment_pw: this.form.pw
      }
      this.$store.dispatch('registerPayment', data)
      .then(() => this.$router.push("/paymentmethod"))
      .catch(err => console.log(err))
    },
    onReset(evt) {
      evt.preventDefault()
      // Reset our form values
      this.form.company = ''
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
/*  .form{
    display: inline-block;
    width: 90%;
    text-align: left;
  }*/
</style>
