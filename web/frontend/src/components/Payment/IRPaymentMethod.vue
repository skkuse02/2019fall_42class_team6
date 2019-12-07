<template lang="html">
  <div class="form">
    <h2>Payment Methods</h2>
    <br>
    <component v-if="is_empty" v-bind:is="IRPaymentMethodBlank"></component>
    <div v-else v-for="_ in paymentToken">
      <component v-bind:is="IRPaymentMethodCard"></component>
    </div>
    <component v-bind:is="IRPaymentMethodForm"></component>
  </div>
</template>

<script>
import IRPaymentMethodBlank from './IRPaymentMethodBlank.vue'
import IRPaymentMethodCard from './IRPaymentMethodCard.vue'
import IRPaymentMethodForm from './IRPaymentMethodForm.vue'

export default {
  data(){
    return {
      var paymentToken = []
    }
  },
  created() {
    this.paymentToken = JSON.parse(localStorage.getItem('paymentToken'))
  },
  computed: {
    is_empty() {
      return !this.$store.getters.existPaymentMethod
    }
  }
}
</script>

<style lang="css" scoped>
</style>
