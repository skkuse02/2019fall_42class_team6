import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex);

export default new Vuex.Store({
	// counter 라는 state 속성을 추가
	state: {
		status: '',
		userToken: localStorage.getItem('userToken') || '',
		paymentToken: localStorage.getItem('paymentToken') || '',
		user: {}
	},
	getters: {
		isLoggedIn: state => !!state.userToken,
		paymentMethod: state => !!state.paymentToken,
		authStatus: state => state.status
	},
	mutations: {
		auth_request(state){
			state.status = 'loading'
		},
		auth_success(state, userToken, user){
			state.status = 'success'
			state.userToken = userToken
			state.user = user
		},
		auth_error(state){
			state.status = 'error'
		},
		logout(state){
			state.status = ''
			state.userToken = ''
		},
		paycheck_request(state){
			state.status = 'loading'
		},
		paycheck_success(state, paymentToken){
			state.status = 'success'
			state.paymentToken = paymentToken
		},
		paycheck_error(state){
			state.status = 'error'
		},
	},
	actions : {
		login({commit}, user){
			return new Promise((resolve, reject) => {
				commit('auth_request')
				axios({url: '/user', data: user, method: 'POST' })
				.then(resp => {
					console.log(resp.data[0])
					if (!resp.data){
						commit('auth_error')
						alert("존재하지 않는 회원정보입니다.")
						reject(resp)
					}else{
						const userToken = resp.data[0]
						const user = resp.data[0].user_id
						delete userToken.user_pw

						localStorage.setItem('userToken', JSON.stringify(userToken))

						// Add the following line:
						axios.defaults.headers.common['Authorization'] = userToken
						commit('auth_success', userToken, user)
						resolve(resp)
					}
				})
				.catch(err => {
					commit('auth_error')
					localStorage.removeItem('userToken')
					reject(err)
				})
			})
		},
		modifyUserInfo({commit}, user){
			return new Promise((resolve, reject) => {
				commit('auth_request')
				axios({url: '/user', data: user, method: 'POST' })
				.then(resp => {
					console.log(resp.data[0])
					if (!resp.data){
						commit('auth_error')
						alert("회원정보수정에 실패했습니다.")
						reject(resp)
					}else{
						// update userToken in localStorage
						delete user.user_pw
						let userToken = JSON.parse(localStorage.getItem('userToken'))
						for(var key in user) {
								userToken[key] = user[key] ;
						}
						localStorage.setItem('userToken', JSON.stringify(userToken))

						// Add the following line:
						axios.defaults.headers.common['Authorization'] = userToken
						commit('auth_success', userToken, user)
						resolve(resp)
					}
				})
				.catch(err => {
					commit('auth_error')
					alert("통신에 실패했습니다.")
					reject(err)
				})
			})
		},
		register({commit}, user){
			return new Promise((resolve, reject) => {
				commit('auth_request')
				axios({url: '/user', data: user, method: 'POST' })
				.then(resp => {
					console.log(resp.data[0])
					if (!resp.data){
						commit('auth_error')
						alert("회원가입에 실패했습니다.")
						reject(resp)
					}else{
						let userToken = user
						delete userToken.user_pw

						// update userToken in localStorage
						localStorage.setItem('userToken', JSON.stringify(userToken))

						// Add the following line:
						axios.defaults.headers.common['Authorization'] = userToken
						commit('auth_success', userToken, userToken.user_id)
						resolve(resp)
					}
				})
				.catch(err => {
					commit('auth_error', err)
					alert("통신에 실패했습니다.")
					reject(err)
				})
			})
		},
		logout({commit}){
			return new Promise((resolve, reject) => {
				commit('logout')
				localStorage.removeItem('userToken')
				delete axios.defaults.headers.common['Authorization']
				resolve()
			})
		},
		getPaymentList({commit, state}){
			return new Promise((resolve, reject) => {
				commit('paycheck_request')
				axios({url: '/payment', data: state.user, method: 'POST' })
				.then(resp => {
					console.log(resp.data)
					if(resp.data == false){
						const paymentToken = ""
						localStorage.setItem('paymentToken', paymentToken)
					}else{
						// payment id로만 이루어진 array 생성
						var paymentToken = []
						for(var obj in resp.data){
							paymentToken.push(Object.values(obj)[0])
						}
						localStorage.setItem('paymentToken', JSON.stringify(paymentToken))
					}

					// Add the following line:
					axios.defaults.headers.common['PaymentMethod'] = paymentToken
					commit('paycheck_success', paymentToken)
					resolve(resp)
				})
				.catch(err => {
					commit('paycheck_error')
					localStorage.removeItem('paymentToken')
					reject(err)
				})
			})
		},
		registerPayment({commit, dispatch}, payment){
			return new Promise((resolve, reject) => {
				commit('paycheck_request')
				axios({url: '/payment', data: payment, method: 'POST' })
				.then(resp => {
					console.log(resp.data[0])
					if (!resp.data){
						commit('paycheck_error')
						alert("결제수단 등록에 실패했습니다.")
						reject(resp)
					}else{
						dispatch('getPaymentList')
						resolve(resp)
					}
				})
				.catch(err => {
					commit('paycheck_error', err)
					alert("통신에 실패했습니다.")
					reject(err)
				})
			})
		},



	}
});
