import Vue from 'vue'
import BootstrapVue from 'bootstrap-vue'

import 'promise-polyfill/src/polyfill'
import 'whatwg-fetch'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

import router from './router';

//import 'site.css'

Vue.use(BootstrapVue);

var app = new Vue({   
    el: '#app',
    router,
    render: h => h(require('./app/app.vue').default)
});
