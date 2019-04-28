export default {
    data() {
        return {
            ticket_id: this.$route.params.id,
            form: {
                childId: 0,
                doctorId: 0,
                date: null,
                time: null,
                title: "",
                text: "",
                room: "",
                ticketType: 0
            },
            searchChildIin: '',
            children: [],
            childId: 0,
            child: "",
            show: true,
            alerts: [],
            submited: false
        }
    },
    watch: {
        searchChildIin(newValue) {
            this.children = [];
            this.childId = 0;
            if (newValue.length > 3) {
                this.findChildByIin(newValue);
            }
        },
        childId(newValue) {
            this.form.childId = newValue;
            this.children = [];

            if (newValue !== 0) {
                window.fetch('/api/child/' + newValue,
                    {
                        method: 'GET',
                        headers: {
                            'Accept': 'application/json, text/plain, */*',
                            'Content-Type': 'application/json'
                        }
                    }).then(response => {
                        return response.json();
                    }).then(response => {
                        var child = response.result;
                        this.child =
                            "<strong>" +
                            child.iin +
                            "</strong> - " +
                            child.lastName +
                            " " +
                            child.firstName +
                            " " +
                            child.patronim;
                    });
            }
        }
    },
    methods: {
        onSubmit(evt) {
            evt.preventDefault();
            if (this.ticket_id) {
                this.updateTicket();
            } else {
                this.addTicket();
            }
        },
        addTicket() {
            this.alerts = [];
            this.submited = true;

            var authHeader = 'Bearer ' + this.$store.state.token;
            let data = JSON.stringify({
                startDateTime: this.form.date + " " + this.form.time,
                childId: this.form.childId,
                room: this.form.room,
                title: this.form.title,
                text: this.form.text,
                ticketType: this.form.ticketType
            });
            console.log("data: " + data);

            window.fetch('/api/ticket',
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
        updateTicket() {
            var authHeader = 'Bearer ' + this.$store.state.token;
            let data = JSON.stringify({
                id: this.ticket_id,
                startDateTime: this.form.date + " " + this.form.time,
                childId: this.form.childId,
                doctorId: this.form.doctorId,
                room: this.form.room,
                title: this.form.title,
                text: this.form.text,
                ticketType: this.form.ticketType
            });

            window.fetch('/api/ticket/' + this.ticket_id,
                {
                    method: 'PUT',
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
                console.log(response);
                this.submited = false;

                if (response.child) {
                    this.setChild(response.child);
                }

                if (response.startDateTime) {
                    this.form.date = response.startDateTime.substr(0, 10);
                    this.form.time = response.startDateTime.substr(11, 5);
                }

                if (response.doctorId) {
                    this.form.doctorId = response.doctorId;
                }

                this.form.room = response.room;
                this.form.title = response.title;
                this.form.text = response.text;
                this.form.ticketType = response.ticketType;
            });
        },
        setChild(child) {
            this.child = child.lastName + " " + child.firstName + " "  + child.patronim;
            this.form.childId = child.id;
        },
        findChildByIin(iin) {
            window.fetch('/api/child/iin/' + iin, {
                method: 'GET',
                headers: {
                    'Accept': 'application/json, text/plain, */*',
                    'Content-Type': 'application/json'
                }
            }).then(response => {
                return response.json();
            }).then(response => {
                console.log(response);
                var children = [];
                var childrenJson = response.result;
                childrenJson.forEach(child => {
                    children.push({ value: child.id, text: child.iin + " - " + child.firstName + " " + child.lastName + " " + child.patronim });
                });

                this.children = children;
            });
        },
        resetSearchChildSuggestions() {
            this.children = [];
            this.childId = 0;
            this.searchChildIin = "";
            this.child = "";
        }
    },
    mounted() {
        if (this.ticket_id) {
            this.getTicket();
        }
    }
}