export default {
    data() {
        return {
            slide: 0,
            iin: '',
            iinState: null,
            password: '',
            passwordState: null,
            show: true,
            alerts: [],
            submited: false
        }
    },
    computed: {
        isValid() {
            var result = true;

            if (this.iin.length !== 12) {
                result = false;
                this.iinState = false;
            }

            if (this.password.length === 0) {
                result = false;
                this.passwordState = false;
            }

            return result;
        }
    },
    watch: {
        iin() {
            this.iinState = null;
        },
        password() {
            this.passwordState = null;
        }
    },
    methods: {
        onSlideStart(slide) {
            this.sliding = true;
        },
        onSlideEnd(slide) {
            this.sliding = false;
        },
        onSubmit(evt) {
            evt.preventDefault();

            if (this.isValid) {
                this.submited = true;
                this.alerts = [];

                var formData = JSON.stringify({
                    iin: this.iin,
                    password: this.password,
                    role: "doctor"
                });

                this.$store.dispatch("AUTH_REQUEST", formData).then((response) => {
                    this.submited = false;
                    if (response.result) {
                        this.alerts = [];
                        this.$router.push("/calendar");
                    } else if (response.messages) {
                        response.messages.forEach(m => {
                            this.alerts.push({
                                title: m.title,
                                text: m.text,
                                variant: "danger"
                            });
                        });
                    } else if (response.errors) {

                        var errors = response.errors;
                        var keys = Object.keys(errors);
                        console.log(keys);

                        keys.forEach(key => {
                            errors[key].forEach(error => {
                                this.alerts.push({
                                    title: error,
                                    text: "",
                                    variant: "danger"
                                });
                            });
                        });
                    } else {
                        this.alerts.push({
                            title: "Что-то пошло не так...",
                            text: "Попробуйте повторить чуть позже.",
                            variant: "danger"
                        });
                    }
                }).catch(error => {
                    console.log(error);
                    this.submited = false;
                    this.alerts.push({
                        title: "Что-то пошло не так...",
                        text: "Попробуйте повторить чуть позже.",
                        variant: "danger"
                    });
                });
            }
        }
    }
}