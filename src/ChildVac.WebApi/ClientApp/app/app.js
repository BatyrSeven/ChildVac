import NavBar from '../components/navbar/navbar.vue';
import Nav from '../components/nav/nav.vue';

export default {
    components: {
        'navbar': NavBar,
        'nav-menu': Nav
    },
    data() {
        return {
            alerts: [
                { 
                    className: "alert-danger",
                    title: "Title",
                    text: "Text"
                },
                { 
                    className: "alert-success",
                    title: "Title",
                    text: "Text"
                }
            ]
        }
    },
    computed: {

    },
    methods: {

    },
    mounted() {
        if (this.$store.getters.isAuthenticated && !this.$store.getters.userName) {
            this.$store.dispatch("USER_REQUEST").catch(err => {
                this.$store.dispatch("AUTH_LOGOUT");
                this.$router.push("/");
            });
        }
    }
}
