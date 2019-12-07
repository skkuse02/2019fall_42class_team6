<template lang="html">
  <div>
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
      .then(() => this.$router.push("/paymentmethod"))
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
      .then(() => this.$router.push("/paymentmethod"))
      .catch(err => console.log(err))
    }
  }
}
</script>

<style lang="css" scoped>
</style>
