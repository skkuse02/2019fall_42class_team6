<template lang="html">
  <div class="card">
    <b-card bg-variant="light" text-variant="black" title="Payment Method">
      <b-card-text>
        Card Number : {{ getCardNumber }}
      </b-card-text>
      <b-button v-on:click="setDefault" variant="primary">Set it as a default</b-button>
      <b-button v-on:click="remove" variant="secondary">Remove</b-button>
    </b-card>
  </div>
</template>

<script>
export default {
  props: [ 'PID' ],
  computed: {
    getCardNumber(){
      return JSON.parse(localStorage.getItem('paymentToken'))[this.PID]
    }
  },
  methods: {
    setDefault() {
      console.log("기본 결제수단 설정")
      let data = {
        headers: { 'Content-type': 'application/x-www-form-urlencoded' },
        function: 'SetDefaultPayment',
        user_id: JSON.parse(localStorage.getItem('userToken')).user_id,
        payment_id: this.PID
      }
      this.$store.dispatch('setDefaultPayment', data)
      .then(() => alert("Card Number: " + this.getCardNumber + "이 기본 결제수단으로 설정되었습니다."))
      .catch(err => console.log(err))
    },
    remove() {
      console.log("결제수단 삭제")
      let data = {
        headers: { 'Content-type': 'application/x-www-form-urlencoded' },
        function: 'RemovePayment',
        payment_id: this.PID
      }
      this.$store.dispatch('removePayment', data)
      .then(() => { this.$router.push("/");
                    setTimeout(function(that){that.$router.push("/paymentmethod");}, 50, this); })
      .catch(err => console.log(err))
    }
  }
}
</script>

<style lang="css" scoped>
  .card{
    text-align: left;
  }
</style>
