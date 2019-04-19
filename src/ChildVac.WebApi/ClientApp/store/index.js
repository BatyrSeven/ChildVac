﻿import Vue from 'vue';
import Vuex from 'vuex';
import mutations from './mutations';
import actions from './actions';

Vue.use(Vuex);

const store = new Vuex.Store({
    state: {
        user: {
            iin: null,
            token: null,
            role: null
        }
    },
    mutations,
    actions
});

export default store;