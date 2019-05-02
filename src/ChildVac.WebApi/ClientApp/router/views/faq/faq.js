export default {
    data() {
        return {
            advices: []
        }
    },
    methods: {
        getAdvices() {
            window.fetch('/api/advice', {
                method: 'GET',
                headers: {
                    'Accept': 'application/json, text/plain, */*',
                    'Content-Type': 'application/json'
                }
            }).then(response => {
                return response.json();
            }).then(response => {
                console.log(response);

                this.advices = response;
            });
        }
    },
    mounted() {
        this.getAdvices();
    }
}