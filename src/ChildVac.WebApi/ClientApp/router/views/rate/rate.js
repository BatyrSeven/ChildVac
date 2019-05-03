export default {
    data() {
        return {
            form: {
                doctorName: '',
                hospital: '',
                text: '',
                rate: 0
            },
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

            var userId = this.$route.params.user_id;
            if (userId) {
                this.form.userId = userId;
            }

            let data = JSON.stringify(this.form);
            console.log("data: " + data);

            window.fetch('/api/feedback', {
                method: 'POST',
                headers: {
                    'Accept': 'application/json, text/plain, */*',
                    'Content-Type': 'application/json'
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