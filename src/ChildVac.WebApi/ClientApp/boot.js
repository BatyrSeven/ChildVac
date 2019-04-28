import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import vueEventCalendar from 'vue-event-calendar'
import { library } from '@fortawesome/fontawesome-svg-core'
import { faCoffee, faUser, faCalendarWeek, faPen, faUserMd, faBaby, faUserPlus } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

import 'promise-polyfill/src/polyfill';
import 'whatwg-fetch';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';
import 'vue-event-calendar/dist/style.css'

import router from './router';
import store from './store';

//import 'site.css'

Vue.use(BootstrapVue);
Vue.use(vueEventCalendar,
    {
        locale: 'ru',
        color: '#0669c6',
        weekStartOn: 1
    });

library.add(faCoffee, faUser, faCalendarWeek, faPen, faUserMd, faBaby, faUserPlus);

Vue.component('font-awesome-icon', FontAwesomeIcon);

var app = new Vue({   
    el: '#app',
    router,
    store,
    render: h => h(require('./app/app.vue').default)
});
