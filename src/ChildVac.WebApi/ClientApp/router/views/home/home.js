export default {
    data() {
        return {
            slide: 0,
            iin: '',
            iinState: null,
            password: '',
            passwordState: null,
            show: true
        }
    },
    computed: {
        isValid() {
            var result = true;

            if (this.iin.length !== 12) {
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
        iin() {
            this.iinState = null;
        },
        password() {
            this.passwordState = null;
        }
    },
    methods: {
        onSlideStart(slide) {
            this.sliding = true;
        },
        onSlideEnd(slide) {
            this.sliding = false;
        },
        onSubmit(evt) {
            evt.preventDefault();

            if (this.isValid) {
                var formData = JSON.stringify({
                    iin: this.iin,
                    password: this.password
                });
                alert(formData);
            }
        },
        onReset(evt) {
            evt.preventDefault();

            this.form.email = '';
            this.form.name = '';
            this.form.food = null;
            this.form.checked = [];

            this.show = false;
            this.$nextTick(() => {
                this.show = true;
            });
        }
    }
}