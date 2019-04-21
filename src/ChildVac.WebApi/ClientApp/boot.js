import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import { library } from '@fortawesome/fontawesome-svg-core'
import { faCoffee, faUser } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

import 'promise-polyfill/src/polyfill';
import 'whatwg-fetch';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';

import router from './router';
import store from './store';

//import 'site.css'

Vue.use(BootstrapVue);

library.add(faCoffee, faUser);

Vue.component('font-awesome-icon', FontAwesomeIcon);

var app = new Vue({   
    el: '#app',
    router,
    store,
    render: h => h(require('./app/app.vue').default)
});
