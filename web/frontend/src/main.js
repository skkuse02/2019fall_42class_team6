import Vue from 'vue'
import App from './App.vue'
import BootstrapVue from 'bootstrap-vue'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import router from './routes/router'
import store from './store/store'
import axios from 'axios'

Vue.use(BootstrapVue)
Vue.config.productionTip = false
Vue.prototype.$http = axios;
const userToken = localStorage.getItem('userToken')
const paymentToken = localStorage.getItem('paymentToken')
if (userToken) {
  Vue.prototype.$http.defaults.headers.common['Authorization'] = userToken
}
if (paymentToken) {
  Vue.prototype.$http.defaults.headers.common['PaymentMethod'] = paymentToken
}

new Vue({
  render: h => h(App),
  router,
  store
}).$mount('#app')
