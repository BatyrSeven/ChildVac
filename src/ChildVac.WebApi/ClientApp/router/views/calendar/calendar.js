export default {
    data() {
        return {
            events: [
                {
                    date: '2019/04/25', // Required
                    time: '12:00',
                    title: 'Foo' // Required
                },
                {
                    date: '2019/04/25',
                    time: '13:00',
                    title: 'Bar',
                    desc: 'description',
                    customClass: 'disabled highlight'
                }
            ]
        }
    },
    methods: {
        handleDayChange() {
            console.log("handleDayChange");
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