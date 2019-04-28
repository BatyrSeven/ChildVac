export default {
    data() {
        return {
            form: {
                firstName: '',
                lastName: '',
                patronim: '',
                iin: '',
                dateOfBirth: null,
                gender: 0,
                parentId: 0
            },
            searchParentIin: '',
            parents: [],
            parentId: 0,
            parent: "",
            show: true,
            alerts: [],
            submited: false
        }
    },
    computed: {
        isValid() {
            var result = true;

            if (this.firstName.length === 0) {
                result = false;
                this.iinState = false;
            }

            if (this.password.length === 0) {
                result = false;
                this.passwordState = false;
            }

            return result;
        }
    },
    watch: {
        searchParentIin(newValue) {
            this.parents = [];
            this.parentId = 0;
            if (newValue.length > 3) {
                this.findParentByIin(newValue);
            }
        },
        parentId(newValue) {
            this.form.parentId = newValue;
            this.parents = [];

            if (newValue !== 0) {
                window.fetch('/api/parent/' + newValue,
                    {
                        method: 'GET',
                        headers: {
                            'Accept': 'application/json, text/plain, */*',
                            'Content-Type': 'application/json'
                        }
                    }).then(response => {
                        return response.json();
                    }).then(response => {
                        var parent = response.result;
                        this.parent =
                            "<strong>" +
                            parent.iin +
                            "</strong> - " +
                            parent.lastName +
                            " " +
                            parent.firstName +
                            " " +
                            parent.patronim;
                    });
            }
        }
    },
    methods: {
        onSubmit(evt) {
            evt.preventDefault();

            if (!this.isValid) {
                return;
            }

            this.alerts = [];
            this.submited = true;

            var authHeader = 'Bearer ' + this.$store.state.token;
            let data = JSON.stringify(this.form);
            console.log("data: " + data);

            window.fetch('/api/child',
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
        findParentByIin(iin) {
            window.fetch('/api/parent/iin/' + iin, {
                method: 'GET',
                headers: {
                    'Accept': 'application/json, text/plain, */*',
                    'Content-Type': 'application/json'
                }
            }).then(response => {
                return response.json();
            }).then(response => {
                console.log(response);
                var parents = [];
                var parentsJson = response.result;
                parentsJson.forEach(parent => {
                    parents.push({ value: parent.id, text: parent.iin + " - " + parent.firstName + " " + parent.lastName + " " + parent.patronim });
                });

                this.parents = parents;
            });
        },
        resetSearchParentSuggestions() {
            this.parents = [];
            this.parentId = 0;
            this.searchParentIin = "";
            this.parent = "";
        }
    }
}