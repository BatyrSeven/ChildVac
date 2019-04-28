export default {
    data() {
        return {
            form: {
                childId: 0,
                date: null,
                time: null
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
            this.alerts = [];
            this.submited = true;

            var authHeader = 'Bearer ' + this.$store.state.token;
            let data = JSON.stringify({
                startDateTime: this.form.date + " " + this.form.time,
                childId: this.form.childId
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
    }
}