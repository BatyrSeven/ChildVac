export default {
    data() {
        return {
            form: {
                firstName: '',
                lastName: '',
                dateOfBirth: '',
                gender: '',
                iin: '',
                parentIin: '',
            },
            show: true
        }
    },
    methods: {
        onSubmit(evt) {
            evt.preventDefault();
            console.log(this.form);
            fetch('/api/child', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: this.form
            }).then(function (response) {
                if (response.status !== 200)
                    Promise.reject();
                return response.json();
            }).then(function (json) {
                console.log('parsed json', json);
            }).catch(function (ex) {
                console.log('parsing failed', ex);
            });
        },
        onReset(evt) {
            evt.preventDefault();

            // Reset our form values
            this.form.name = '';
            this.form.surname = '';
            this.form.dateOfBirth = '';
            this.form.gender = '';
            this.form.iin = '';
            this.form.parentIin = '';

            // Trick to reset/clear native browser form validation state
            this.show = false;
            this.$nextTick(() => {
                this.show = true;
            });
        }
    }
}