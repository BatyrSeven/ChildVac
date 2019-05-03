export default {
    data() {
        return {
            modalDeleteShow: false,
            cancelTicketId: 0,
            events: [],
            alerts: []
        }
    },
    methods: {
        handleDayChange() {
            console.log("handleDayChange");
        },
        onCancelTicket(id) {
            this.modalDeleteShow = true;
            this.cancelTicketId = id;
        },
        cancelTicket() {
            this.modalDeleteShow = false;
            this.alerts = [];

            var authHeader = 'Bearer ' + this.$store.state.token;
            window.fetch('/api/ticket/' + this.cancelTicketId + '/status',
                {
                    method: 'PATCH',
                    headers: {
                        'Accept': 'application/json, text/plain, */*',
                        'Content-Type': 'application/json',
                        'Authorization': authHeader
                    },
                    body: JSON.stringify({status: 3})
                }).then(response => {
                return response.json();
            }).then(response => {
                console.log(response);

                if (response.messages) {
                    response.messages.forEach(m => {
                        this.alerts.push({
                            title: m.title,
                            text: m.text,
                            variant: response.result ? "success" : "danger"
                        });
                    });
                }

                this.getTickets();
            });
        },
        getTickets() {
            var authHeader = 'Bearer ' + this.$store.state.token;
            var userId = this.$store.state.userId;
            window.fetch('/api/ticket/doctor/' + userId,
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
                var events = [];

                if (response.result) {
                    response.result.forEach(ticket => {
                        ticket.date = ticket.startDateTime.substr(0, 10).replace(/-/g, "/");
                        ticket.time = ticket.startDateTime.substr(11, 5);
                        ticket.title = ticket.title || "";
                        events.push(ticket);
                    })
                }
                console.log(events);
                this.events = events;
            });
        }
    },
    mounted() {
        this.getTickets();
    }
}