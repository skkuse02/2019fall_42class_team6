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
					console.log(resp)
					if (!resp.data){
						commit('auth_error')
						alert("존재하지 않는 회원정보입니다.")
						reject(resp)
					}else{
						//const token = resp.data.token
						//const user = resp.data.user
						const token = resp.data
						const user = resp.data.user_id
						console.log("login된 user id : ",user)

						localStorage.setItem('token', token)
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
		register({commit}, user){
			return new Promise((resolve, reject) => {
				commit('auth_request')
				axios({url: '/api/inteReal/user', data: user, method: 'POST' })
				.then(resp => {
					const token = resp.data.token
					const user = resp.data.user
					localStorage.setItem('token', token)
					// Add the following line:
					axios.defaults.headers.common['Authorization'] = token
					commit('auth_success', token, user)
					resolve(resp)
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
