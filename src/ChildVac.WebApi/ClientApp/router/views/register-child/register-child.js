export default {
    data() {
        return {
            form: {
                firstName: '',
                lastName: '',
                patronim: '',
                iin: '',
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
                window.fetch('/api/parent/' + newValue, {
                    method: 'GET',
                    headers: {
                        'Accept': 'application/json, text/plain, */*',
                        'Content-Type': 'application/json'
                    }
                }).then(response => {
                    return response.text();
                }).then(result => {
                    var parent = JSON.parse(result);
                    this.parent = "<strong>" + parent.iin + "</strong> - " + parent.lastName + " " + parent.firstName + " " + parent.patronim;
                });
            }
        }
    },
    methods: {
        onSubmit(evt) {
            evt.preventDefault();
            var t = this;

            let data = JSON.stringify(this.form);
            console.log("data: " + data);

            window.fetch('/api/child', {
                method: 'POST',
                headers: {
                    'Accept': 'application/json, text/plain, */*',
                    'Content-Type': 'application/json'
                },
                body: data
            }).then(response => {
                if (response.status >= 200 && response.status < 300) {
                    t.alert.show = true;
                    t.alert.className = "alert-success";
                    t.alert.text = "Регистрация прошла успешно!";
                    this.reset();
                } else {
                    t.alert.show = true;
                    t.alert.className = "alert-danger";
                    t.alert.text = "Не удалось провести регистрацию. Проверьте правильность введеных данных.";
                }
            });
        },
        onReset(evt) {
            evt.preventDefault();

            this.reset();
        },
        reset() {
            this.form.firstName = '';
            this.form.lastName = '';
            this.form.patronim = '';
            this.form.iin = '';
            this.form.gender = 0;
            this.form.parentId = 0;

            this.resetSearchParentSuggestions();

            this.alert.show = false;
            this.alert.className = "";
            this.alert.text = "";

            // Trick to reset/clear native browser form validation state
            this.show = false;
            this.$nextTick(() => {
                this.show = true;
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
                return response.text();
            }).then(result => {
                var parents = [];
                var parentsJson = JSON.parse(result);

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