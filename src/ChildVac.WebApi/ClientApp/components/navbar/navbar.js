export default {
    components: {

    },
    data() {
        return {
        }
    },
    computed: {
        isAuthenticated() {
            return this.$store.getters.isAuthenticated;
        },
        userName() {
            return this.$store.getters.userName;
        }
    },
    methods: {
        onSearchSubmit(event) {
            event.preventDefault();
            alert("Извините, поиск временно недоступен");
        },
        logout() {
            this.$store.dispatch("AUTH_LOGOUT");
            this.$router.push("/");
        }
    }
}
