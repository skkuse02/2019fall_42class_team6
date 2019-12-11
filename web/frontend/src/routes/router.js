import Vue from 'vue'
import VueRouter from 'vue-router'
import store from '../store/store.js'
import IRHome from '../components/IRHome.vue'
import IRLogin from '../components/Login/IRLogIn.vue'
import IRSignUp from '../components/Login/IRSignUp.vue'
import IRUserInfo from '../components/Login/IRUserInfo.vue'
import IRModifyUserInfo from '../components/Login/IRModifyUserInfo.vue'
import IRPaymentMethod from '../components/Payment/IRPaymentMethod.vue'
import Secure from '../components/Secure.vue'

Vue.use(VueRouter)

let router = new VueRouter({
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
    },
    {
      path: '/secure',
      name: 'secure',
      component: Secure,
      meta: {
        requiresAuth: true
      }
    }
  ]
})

router.beforeEach((to, from, next) => {
  if(to.matched.some(record => record.meta.requiresAuth)) {
    if (store.getters.isLoggedIn) {
      next()
      return
    }
    next('/login')
  } else {
    next()
  }
})

export default router
