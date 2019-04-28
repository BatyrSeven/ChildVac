export default {
    data() {
        return {
            modalDeleteShow: false,
            deleteTicketId: 0,
            events: [],
            alerts: []
        }
    },
    methods: {
        handleDayChange() {
            console.log("handleDayChange");
        },
        onDeleteTicket(id) {
            this.modalDeleteShow = true;
            this.deleteTicketId = id;
        },
        deleteTicket() {
            this.modalDeleteShow = false;

            var authHeader = 'Bearer ' + this.$store.state.token;
            window.fetch('/api/ticket/' + this.deleteTicketId,
                {
                    method: 'DELETE',
                    headers: {
                        'Accept': 'application/json, text/plain, */*',
                        'Content-Type': 'application/json',
                        'Authorization': authHeader
                    }
                }).then(response => {
                return response.json();
            }).then(response => {
                console.log(response);

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
                console.log(response);
                this.events = response.result;
            });
        }
    },
    mounted() {
        this.getTickets();
    }
}