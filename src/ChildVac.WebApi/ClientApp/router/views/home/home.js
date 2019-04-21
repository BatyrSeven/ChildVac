export default {
    data() {
        return {
            slide: 0,
            iin: '',
            iinState: null,
            password: '',
            passwordState: null,
            show: true,
            alert: {
                show: false,
                className: '',
                text: ''
            }
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
                var formData = JSON.stringify({
                    iin: this.iin,
                    password: this.password,
                    role: "doctor"
                });

                this.$store.dispatch("AUTH_REQUEST", formData).then((response) => {
                    if (response.result) {
                        this.alert.show = false;
                        this.$router.push("/calendar");
                    } else {
                        this.alert.text = "<strong>" + response.messageTitle + "</strong><br />" + response.messageText;
                        this.alert.className = "alert-danger";
                        this.alert.show = true;
                    }
                }).catch(error => {
                    this.alert.text = "<strong>Что-то пошло не так..</strong><br />Попробуйте повторить чуть позже.";
                    this.alert.className = "alert-danger";
                    this.alert.show = true;
                });
            }
        }
    }
}