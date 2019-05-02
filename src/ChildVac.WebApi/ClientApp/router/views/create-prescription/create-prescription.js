export default {
    data() {
        return {
            ticket_id: this.$route.params.ticket_id,
            alerts: [],
            submited: false,
            child: {
                id: 0,
                name: "",
                iin: ""
            },
            form: {
                diagnosis: "",
                description: "",
                type: "",
                medication: ""
            }
        }
    },
    watch: {},
    methods: {
        onSubmit(evt) {
            evt.preventDefault();
            this.alerts = [];
            this.submited = true;

            var authHeader = 'Bearer ' + this.$store.state.token;
            let data = JSON.stringify({
                diagnosis: this.form.diagnosis,
                description: this.form.description,
                ticketId: this.ticket_id,
                medication: this.form.medication,
                type: this.form.type
            });
            console.log("data: " + data);

            window.fetch('/api/prescription',
                {
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
        },
        getTicket() {
            var authHeader = 'Bearer ' + this.$store.state.token;
            window.fetch('/api/ticket/' + this.ticket_id,
                {
                    method: 'GET',
                    headers: {
                        'Accept': 'application/json, text/plain, */*',
                        'Content-Type': 'application/json',
                        'Authorization': authHeader
                    }
                }).then(response => {
                return response.json();
            }).then(response => {
                this.submited = false;
                console.log(response);
                if (response.child) {
                    this.setChild(response.child);
                }

            });
        },
        setChild(child) {
            this.child = child;
        }
    },
    mounted() {
        this.getTicket();
    }
}