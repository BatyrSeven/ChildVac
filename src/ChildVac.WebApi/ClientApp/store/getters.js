export default {
    isAuthenticated: state => !!state.token,
    userName: state => {
        let user = state.user;
        if (user) {
            return user.lastName + " " + user.firstName;
        }
        return "";
    }
}