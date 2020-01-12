import Vue from 'vue';
import Vuex from 'vuex';
import { IProfileData } from './models/personalDataModel';

Vue.use(Vuex);

function initialState() {
  return {
    userId: 0,
    email: '',
    token: '',
    fullname: '',
    profileData: {} as IProfileData | null,
  }
}

export default new Vuex.Store({
  state: initialState(),
  mutations: {
    reset(state) {
      const init = initialState();
      Object.keys(init).forEach(key => {
        state[key] = init[key];
      })
    },
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
    setProfileData(state, profileData: IProfileData) {
      state.profileData = profileData;
    },
  },
  getters: {
    getUserId: state => state.userId,
    getEmail: state => state.email,
    getToken: state => state.token,
    getFullname: state => state.fullname,
    getProfileData: state => state.profileData,
  },
  actions: {

  }
});
