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
                ticketType: 0,
                vaccineId: null
            },
            searchChildIin: '',
            children: [],
            childId: 0,
            child: "",
            show: true,
            alerts: [],
            submited: false,
            vaccines: ["Загрузка..."]
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
                ticketType: this.form.ticketType,
                vaccineId: this.form.vaccineId
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
                    if (response.messages) {
                        response.messages.forEach(m => {
                            this.alerts.push({
                                title: m.title,
                                text: m.text,
                                variant: response.result ? "success" : "danger"
                            });
                        });
                    } else if (response.errors) {

                        var errors = response.errors;
                        var keys = Object.keys(errors);
                        console.log(keys);

                        keys.forEach(key => {
                            errors[key].forEach(error => {
                                this.alerts.push({
                                    title: error,
                                    text: "",
                                    variant: "danger"
                                });
                            });
                        });
                    } else {
                        this.alerts.push({
                            title: "Что-то пошло не так...",
                            text: "Попробуйте повторить чуть позже.",
                            variant: "danger"
                        });
                    }
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
                ticketType: this.form.ticketType,
                vaccineId: this.form.vaccineId
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
                    this.form.vaccineId = response.vaccineId;
                });
        },
        setChild(child) {
            this.child = child.lastName + " " + child.firstName + " " + child.patronim;
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
                    children.push({ value: child.id, text: child.iin + " - " + child.lastName + " " + child.firstName + " " + child.patronim });
                });

                this.children = children;
            });
        },
        getVaccines() {
            window.fetch('/api/vaccine', {
                method: 'GET',
                headers: {
                    'Accept': 'application/json, text/plain, */*',
                    'Content-Type': 'application/json'
                }
            }).then(response => {
                return response.json();
            }).then(response => {
                console.log(response);
                var vaccines = [];
                var vaccinesJson = response;
                vaccinesJson.forEach(vaccine => {
                    vaccines.push({ value: vaccine.id, text: vaccine.name + " - на " + vaccine.recieveMonth + " месяц" });
                });

                this.vaccines = vaccines;
            });
        },
        resetSearchChildSuggestions() {
            this.children = [];
            this.childId = 0;
            this.searchChildIin = "";
            this.child = "";
        },
        setCurrentDate() {
            var date = new Date().toISOString();
            var currentDate = date.slice(0, 10);
            this.form.date = currentDate;
        }
    },
    mounted() {
        if (this.ticket_id) {
            this.getTicket();
        }

        this.getVaccines();
        this.setCurrentDate();
    }
}