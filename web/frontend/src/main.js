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
const token = localStorage.getItem('token')
if (token) {
  Vue.prototype.$http.defaults.headers.common['Authorization'] = token
}

new Vue({
  render: h => h(App),
  router,
  store
}).$mount('#app')
