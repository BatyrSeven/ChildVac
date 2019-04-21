export default {
    data() {
        return {
            form: {
                firstName: '',
                lastName: '',
                patronim: '',
                iin: '',
                address: '',
                gender: 0
            },
            show: true,
            alert: {
                show: false,
                className: "",
                text: ""
            }
        }
    },
    methods: {
        onSubmit(evt) {
            evt.preventDefault();
            var t = this;

            var authHeader = 'Bearer ' + this.$store.state.token;
            let data = JSON.stringify(this.form);
            console.log("data: " + data);

            window.fetch('/api/parent', {
                method: 'POST',
                headers: {
                    'Accept': 'application/json, text/plain, */*',
                    'Content-Type': 'application/json',
                    'Authorization': authHeader
                },
                body: data
            }).then(response => {
                return response.json();
            }).then(response => {
                console.log(response);
                if (response.result) {
                    t.alert.show = true;
                    t.alert.className = "alert-success";
                    t.alert.text = "Регистрация прошла успешно!";
                    this.reset();
                } else {
                    this.alert.text = "<strong>" + response.messageTitle + "</strong><br />" + response.messageText;
                    this.alert.className = "alert-danger";
                    this.alert.show = true;
                }
            });
        },
        onReset(evt) {
            evt.preventDefault();

            this.reset();
            this.resetAlert();
        },
        reset() {
            // Reset our form values
            this.form.firstName = "";
            this.form.lastName = "";
            this.form.patronim = "";
            this.form.iin = "";
            this.form.address = "";
            this.form.gender = 0;

            // Trick to reset/clear native browser form validation state
            this.show = false;
            this.$nextTick(() => {
                this.show = true;
            });
        },
        resetAlert() {
            this.alert.show = false;
            this.alert.className = "";
            this.alert.text = "";
        }
    }
}