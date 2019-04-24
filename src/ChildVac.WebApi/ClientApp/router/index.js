import Vue from 'vue';
import VueRouter from 'vue-router';
import routes from './routes';

Vue.use(VueRouter);

const router = new VueRouter({
    routes
});

router.beforeEach((to, from, next) => {
    var token = localStorage.getItem('user-token') || '';
    var isAuthenticated = !!token;
    if (to.matched.some(record => record.meta.requiresAuth)) {
        if (!isAuthenticated) {
            next({
                path: '/'
            });
        } else {
            next();
        }
    } else {
        if (isAuthenticated) {
            next({
                path: '/calendar'
            });
        } else {
            next();
        }
    }
});

export default router;