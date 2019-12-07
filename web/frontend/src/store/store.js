import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex);

export default new Vuex.Store({
	// counter 라는 state 속성을 추가
	state: {
		status: '',
		token: localStorage.getItem('token') || '',
		user: {}
	},
	getters: {
		isLoggedIn: state => !!state.token,
		authStatus: state => state.status
	},
	mutations: {
		auth_request(state){
			state.status = 'loading'
		},
		auth_success(state, token, user){
			state.status = 'success'
			state.token = token
			state.user = user
		},
		auth_error(state){
			state.status = 'error'
		},
		logout(state){
			state.status = ''
			state.token = ''
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
						const token = resp.data[0]
						const user = resp.data[0].user_id
						delete token.user_pw

						localStorage.setItem('token', JSON.stringify(token))

						// Add the following line:
						axios.defaults.headers.common['Authorization'] = token
						commit('auth_success', token, user)
						resolve(resp)
					}
				})
				.catch(err => {
					commit('auth_error')
					localStorage.removeItem('token')
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
						// update token in localStorage
						localStorage.setItem('token', JSON.stringify(token))

						// Add the following line:
						axios.defaults.headers.common['Authorization'] = token
						commit('auth_success', token, user)
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
						let token = user
						delete token.user_pw

						// update token in localStorage
						localStorage.setItem('token', JSON.stringify(token))

						// Add the following line:
						axios.defaults.headers.common['Authorization'] = token
						commit('auth_success', token, token.user_id)
						resolve(resp)
					}
				})
				.catch(err => {
					commit('auth_error', err)
					localStorage.removeItem('token')
					reject(err)
				})
			})
		},
		logout({commit}){
			return new Promise((resolve, reject) => {
				commit('logout')
				localStorage.removeItem('token')
				delete axios.defaults.headers.common['Authorization']
				resolve()
			})
		}
	}
});
