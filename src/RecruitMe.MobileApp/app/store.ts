import Vue from 'vue';
import Vuex from 'vuex';
import { IPersonalData } from './models/personalDataModel';

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    userId: 0,
    email: '',
    token: '',
    fullname: '',
    personalData: {} as IPersonalData | null,
  },
  mutations: {
    setUserId(state, userId) {
      state.userId = userId ? userId : 0;
    },
    setEmail(state, email) {
      state.email = email ? email : '';
    },
    setToken(state, token) {
      state.token = token ? token : '';
    },
    setFullname(state, fullname) {
      state.fullname = fullname ? fullname : '';
    },
    setPersonalData(state, personalData: IPersonalData) {
      state.personalData = personalData;
    },
  },
  getters: {
    getUserId: state => state.userId,
    getEmail: state => state.email,
    getToken: state => state.token,
    getFullname: state => state.fullname,
    getPersonalData: state => state.personalData,
  },
  actions: {

  }
});
