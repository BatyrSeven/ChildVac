export default {
    AUTH_REQUEST: state => {
        state.status = 'loading';
    },
    AUTH_SUCCESS: (state, value) => {
        state.status = 'success';
        state.user = value.user;
        state.token = value.token;
    },
    AUTH_ERROR: state => {
        state.status = 'error';
    },
    AUTH_LOGOUT: state => {
        state.user = null;
        state.token = null;
    },
    USER_SUCCESS: (state, value) => {
        state.user = value;
    }
}