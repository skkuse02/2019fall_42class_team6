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
		user: localStorage.getItem('userToken') == null?
						'' : JSON.parse(localStorage.getItem('userToken')).user_id
	},
	getters: {
		isLoggedIn: state => !!state.userToken,
		existPaymentMethod: state => !!state.paymentToken,
		authStatus: state => state.status
	},
	mutations: {
		auth_request(state){
			state.status = 'loading'
		},
		auth_success(state, {userToken, user}){
			state.status = 'success'
			state.userToken = userToken
			state.user = user
		},
		auth_simple_success(state){
			state.status = 'success'
		},
		auth_error(state){
			state.status = 'error'
		},
		logout(state){
			state.status = ''
			state.userToken = ''
			state.paymentToken = ''
			state.user = ''
		},
		paycheck_request(state){
			state.status = 'loading'
		},
		paycheck_success(state, paymentToken){
			state.status = 'success'
			state.paymentToken = paymentToken
		},
		paycheck_simple_success(state){
			state.status = 'success'
		},
		paycheck_error(state){
			state.status = 'error'
		},
	},
	actions : {
		login({commit, dispatch}, user){
			return new Promise((resolve, reject) => {
				commit('auth_request')
				axios({url: '/user', data: user, method: 'POST' })
				.then(resp => {
					//console.log(resp.data[0])
					if (!resp.data){
						commit('auth_error')
						alert("존재하지 않는 회원정보입니다.")
						reject(resp)
					}else{
						var userToken = resp.data[0]
						var user = resp.data[0].user_id
						delete userToken.user_pw

						localStorage.setItem('userToken', JSON.stringify(userToken))

						// Add the following line:
						axios.defaults.headers.common['Authorization'] = userToken
						commit('auth_success', {userToken, user})
						dispatch('getPaymentList')
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
					//console.log(resp.data[0])
					if (!resp.data){
						commit('auth_error')
						alert("회원정보수정에 실패했습니다.")
						reject(resp)
					}else{
						// update userToken in localStorage
						delete user.password
						delete user.headers
						delete user.function
						let userToken = JSON.parse(localStorage.getItem('userToken'))
						for(var key in user) {
								userToken[key] = user[key] ;
						}
						localStorage.setItem('userToken', JSON.stringify(userToken))

						// Add the following line:
						axios.defaults.headers.common['Authorization'] = userToken
						commit('auth_success', {userToken, user})
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
		checkIdDupl({commit}, user){
			return new Promise((resolve, reject) => {
				commit('auth_request')
				axios({url: '/user', data: user, method: 'POST' })
				.then(resp => {
					if (resp.data){
						commit('auth_error')
						alert("이미 id가 존재합니다. 다른 id를 사용해주세요.")
						reject(resp)
					}else{
						commit('auth_simple_success')
						alert("사용가능한 id입니다!")
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
					//console.log(resp.data)
					if (!resp.data){
						commit('auth_error')
						alert("회원가입에 실패했습니다.\n필수항목은 다 채웠는지, ID 중복체크는 했는지 다시 한번 확인해주세요!")
						reject(resp)
					}else{
						commit('auth_simple_success')
						alert("회원가입에 성공했습니다. 가입한 계정으로 로그인해주세요.")
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
				localStorage.removeItem('paymentToken')
				delete axios.defaults.headers.common['PaymentMethod']
				resolve()
			})
		},
		getPaymentList({commit, state}){
			return new Promise((resolve, reject) => {
				commit('paycheck_request')
				var user = {
					headers: { 'Content-type': 'application/x-www-form-urlencoded' },
					function: 'GetPaymentList',
					user_id: state.user
				}
				axios({url: '/payment', data: user, method: 'POST' })
				.then(resp => {
					//console.log(resp.data)
					var paymentToken
					if(resp.data == false){
						paymentToken = ""
						localStorage.setItem('paymentToken', paymentToken)
					}else{
						// payment id로만 이루어진 array 생성
						paymentToken = new Object()
						for(var i=0; i < resp.data.length; i++){
							paymentToken[resp.data[i]['payment_id']] = resp.data[i]['card_number']
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
					//console.log(resp.data)
					if (!resp.data){
						commit('paycheck_error')
						alert("결제수단 등록에 실패했습니다.")
						reject(resp)
					}else{
						dispatch('getPaymentList')
						setTimeout(function(){resolve(resp);}, 50);
					}
				})
				.catch(err => {
					commit('paycheck_error', err)
					alert("통신에 실패했습니다.")
					reject(err)
				})
			})
		},
		setDefaultPayment({commit}, payment){
			return new Promise((resolve, reject) => {
				commit('paycheck_request')
				axios({url: '/user', data: payment, method: 'POST' })
				.then(resp => {
					//console.log(resp.data)
					if (!resp.data){
						commit('paycheck_error')
						alert("기본 결제수단 등록에 실패했습니다.")
						reject(resp)
					}else{
						let userToken = JSON.parse(localStorage.getItem('userToken'))
						userToken.payment_id = payment.payment_id;
						localStorage.setItem('userToken', JSON.stringify(userToken))
						commit('paycheck_simple_success')
						console.log("기본 결제수단으로 등록되었습니다.")
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
		removePayment({commit,dispatch}, payment){
			return new Promise((resolve, reject) => {
				commit('paycheck_request')
				axios({url: '/payment', data: payment, method: 'POST' })
				.then(resp => {
					//console.log(resp.data)
					if (!resp.data){
						commit('paycheck_error')
						alert("결제수단 삭제에 실패했습니다.")
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
