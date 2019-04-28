export default {
    computed: {
        isAuthenticated() {
            return this.$store.getters.isAuthenticated;
        },
        activeRoute() {
            return this.$route.path;
        }
    },
    methods: {
        getButtonVariant(route) {
            if (this.activeRoute.toLowerCase() === "/" + route.toLowerCase()) {
                return "success";
            }

            return "primary";
        }
    }
}
