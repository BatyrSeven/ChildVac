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
                parentId: null
            },
            searchParentIin: '',
            parents: [],
            parent: "",
            show: true,
            alerts: [],
            submited: false
        }
    },
    computed: {
        isValid() {
            var result = true;

            if (this.form.firstName.length === 0) {
                result = false;
                this.iinState = false;
            }

            if (this.form.lastName.length === 0) {
                result = false;
                this.passwordState = false;
            }

            return result;
        }
    },
    watch: {
        searchParentIin(newValue) {
            if (newValue.length > 3) {
                this.findParentByIin(newValue);
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
                    parents.push({ value: parent.id, text: parent.iin + " - " + parent.lastName + " " +parent.firstName + " " + parent.patronim });
                });

                this.parents = parents;

                if (parents.length) {
                    this.form.parentId = parents[0].value;
                }
            });
        },
        resetSearchParentSuggestions() {
            this.parents = [];
            this.form.parentId = 0;
            this.searchParentIin = "";
            this.parent = "";
        }
    }
}