<template lang="html">
  <div class="newForm">
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
        { value: 'ShinHan', text: '신한' },
        { value: 'KB KookMin', text: 'KB국민' },
        { value: 'SamSung', text: '삼성' },
        { value: 'BC', text: '비씨' },
        { value: 'Lotte', text: '롯데' },
        { value: 'HyunDai', text: '현대' },
        { value: 'Hana', text: '하나' },
        { value: 'KEB_Hana', text: '하나(외환)' },
        { value: 'NH_ChaeUm', text: 'NH채움' },
        { value: 'City', text: '씨티' },
        { value: 'Uri', text: '우리' },
        { value: 'KakaoBank', text: '카카오뱅크' },
        { value: 'KBank', text: '케이뱅크' },
        { value: 'JeonBuk', text: '전북' },
        { value: 'GwangJu', text: '광주' },
        { value: 'Jeju', text: '제주' },
        { value: 'PostOffice', text: '우체국' },
        { value: 'SuHyup', text: '수협' },
        { value: 'ShinHyup', text: '신협' },
        { value: 'KFCC', text: '새마을금고' },
        { value: 'SB', text: '저축은행' },
        { value: 'KDB_Industry', text: 'KDB산업' }
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
      .then(() => { this.$router.push("/");
                    setTimeout(function(that){that.$router.push("/paymentmethod");}, 50, this);
                  })
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
  .newForm{
    width: 80%;
  }
</style>
