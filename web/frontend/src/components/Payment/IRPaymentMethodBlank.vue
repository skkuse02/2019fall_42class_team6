<template lang="html">
  <div>
    <b-card bg-variant="light" text-variant="black" title="Payment Method">
      <b-card-text>
        There is no payment method.
      </b-card-text>
      <b-button v-on:click="setDefault" variant="primary">Set it as a default</b-button>
      <b-button v-on:click="remove" variant="secondary">Remove</b-button>
    </b-card>
  </div>
</template>

<script>
export default {
  data(){
    return{
      p_id: 0;
      cardnumber: '**** **** **** 1234';
  },
  methods: {
    setDefault() {
      // 추후 DB로 보내는 식으로 수정
      alert(this.cardnumber + "is set as a defalut paymentmethod")
    },
    remove() {
      this.$emit('clicked', 'removeMethod', this.p_id)
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
