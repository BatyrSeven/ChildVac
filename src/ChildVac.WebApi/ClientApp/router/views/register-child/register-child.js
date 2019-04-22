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
            alert: {
                show: false,
                className: '',
                text: ''
            }
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
            var t = this;

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
                    console.log(response);

                    t.alert.show = true;
                    this.alert.text = "<strong>" + response.messageTitle + "</strong><br />" + response.messageText;

                    if (response.result) {
                        t.alert.className = "alert-success";
                        this.reset();
                    } else {
                        this.alert.className = "alert-danger";
                    }
                });
        },
        onReset(evt) {
            evt.preventDefault();

            this.reset();
            this.resetAlert();
        },
        reset() {
            this.form.firstName = '';
            this.form.lastName = '';
            this.form.patronim = '';
            this.form.iin = '';
            this.form.gender = 0;
            this.form.parentId = 0;

            this.resetSearchParentSuggestions();

            // Trick to reset/clear native browser form validation state
            this.show = false;
            this.$nextTick(() => {
                this.show = true;
            });
        },
        resetAlert() {
            this.alert.show = false;
            this.alert.className = "";
            this.alert.text = "";
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