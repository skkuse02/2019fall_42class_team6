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
      var is_empty = true
    }
  },
  created(){
    this.paymentToken = this.$store.getters.paymentMethod
    this.is_empty = this.paymentToken.length == 0 ? true : false
  }
}
</script>

<style lang="css" scoped>
</style>
