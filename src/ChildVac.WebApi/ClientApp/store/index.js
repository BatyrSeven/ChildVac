import Vue from 'vue';
import Vuex from 'vuex';
import mutations from './mutations';
import actions from './actions';
import getters from './getters';

Vue.use(Vuex);

const store = new Vuex.Store({
    state: {
        token: localStorage.getItem('user-token') || '',
        userId: localStorage.getItem('user-id') || '',
        user: null,
        status: null
    },
    mutations,
    actions,
    getters
});

export default store;