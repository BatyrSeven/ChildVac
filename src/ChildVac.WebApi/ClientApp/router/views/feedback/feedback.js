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
            alerts: [],
            submited: false,
            rateOptions: [1, 2, 3, 4, 5]

        }
    },
    methods: {
        onSubmit(evt) {
            evt.preventDefault();
            this.alerts = [];
            this.submited = true;

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
                this.submited = false;
                console.log(response);

                response.messages.forEach(m => {
                    this.alerts.push({
                        title: m.title,
                        text: m.text,
                        variant: response.result ? "success" : "danger"
                    });
                });
            });
        }
    }
}