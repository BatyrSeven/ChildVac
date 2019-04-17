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

            let data = JSON.stringify(this.form);
            console.log("data: " + data);

            window.fetch('/api/parent', {
                method: 'POST',
                headers: {
                    'Accept': 'application/json, text/plain, */*',
                    'Content-Type': 'application/json'
                },
                body: data
            }).then(response => {
                if (response.status >= 200 && response.status < 300) {
                    t.alert.show = true;
                    t.alert.className = "alert-success";
                    t.alert.text = "Регистрация прошла успешно!";
                } else {
                    t.alert.show = true;
                    t.alert.className = "alert-danger";
                    t.alert.text = "Не удалось провести регистрацию. Проверьте правильность введеных данных.";
                }
            });
        },
        onReset(evt) {
            evt.preventDefault();

            // Reset our form values
            this.form.login= "";
            this.form.firstName= "";
            this.form.lastName= "";
            this.form.dateOfBirth= "";
            this.form.iin = "";

            this.alert.show = false;
            this.alert.className = "";
            this.alert.text = "";

            // Trick to reset/clear native browser form validation state
            this.show = false;
            this.$nextTick(() => {
                this.show = true;
            });
        }
    }
}