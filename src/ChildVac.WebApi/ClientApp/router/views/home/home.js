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

                window.fetch('/api/account',
                    {
                        method: 'POST',
                        headers: {
                            'Accept': 'application/json, text/plain, */*',
                            'Content-Type': 'application/json'
                        },
                        body: formData
                    }).then(response => {
                        try {
                            return response.json();
                        } catch (e) {
                            console.log("ERROR: " + e);
                        }
                    }).then(response => {
                        console.log(response);
                        if (response.result) {
                            this.$store.commit("SET_USER_INFO", response.result);
                            this.alert.show = false;
                        } else {
                            this.alert.text = "<strong>" + response.messageTitle + "</strong><br />" + response.messageText;
                            this.alert.className = "alert-danger";
                            this.alert.show = true;
                        }
                    });
            }
        }
    }
}