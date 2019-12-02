import Vue from 'vue'
import VueRouter from 'vue-router'
import IRHome from '../components/IRHome.vue'
import IRLogin from '../components/Login/IRLogin.vue'
import IRSignUp from '../components/Login/IRSignUp.vue'
import IRUserInfo from '../components/Login/IRUserInfo.vue'
import IRModifyUserInfo from '../components/Login/IRModifyUserInfo.vue'
import IRPaymentMethod from '../components/Payment/IRPaymentMethod.vue'
import IRDownload from '../components/Download/IRDownload.vue'

Vue.use(VueRouter)

export const router = new VueRouter({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'home',
      component: IRHome
    },
    {
      path: '/signup',
      name: 'signup',
      component: IRSignUp
    },
    {
      path: '/login',
      name: 'login',
      component: IRLogin
    },
    {
      path: '/download',
      name: 'download',
      component: IRDownload
    },
    {
      path: '/userinfo',
      name: 'userinfo',
      component: IRUserInfo
    },
    {
      path: '/modifyuserinfo',
      name: 'modifyuserinfo',
      component: IRModifyUserInfo
    },
    {
      path: '/paymentmethod',
      name: 'paymentmethod',
      component: IRPaymentMethod
    }
  ]
})
