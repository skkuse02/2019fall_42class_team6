import Vue from 'vue'
import App from './App.vue'
import BootstrapVue from 'Bootstrap-vue'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import { router } from './routes/router.js'
import axios from 'axios'

Vue.use(BootstrapVue)
Vue.config.productionTip = false
Vue.prototype.$http = axios;

new Vue({
  render: h => h(App),
  router
}).$mount('#app')
