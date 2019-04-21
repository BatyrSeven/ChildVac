export default {
    AUTH_REQUEST: ({ commit, dispatch }, data) => {
        return new Promise((resolve, reject) => {
            commit("AUTH_REQUEST");
            window.fetch('/api/account',
                {
                    headers: {
                        'Accept': 'application/json, text/plain, */*',
                        'Content-Type': 'application/json'
                    },
                    method: 'POST',
                    body: data
                }).then(response => {
                    try {
                        return response.json();
                    } catch (e) {
                        console.log("ERROR: " + e);
                    }
                }).then(response => {
                    console.log(response);
                    if (response.result) {
                        const token = response.result.token;
                        localStorage.setItem('user-token', token);
                        commit("AUTH_SUCCESS", response.result);
                    }
                    resolve(response);
                }).catch(error => {
                    commit("AUTH_ERROR", error);
                    localStorage.removeItem('user-token');
                    reject(error);
                });
        });
    },
    AUTH_LOGOUT: ({ commit, dispatch }) => {
        return new Promise((resolve, reject) => {
            commit("AUTH_LOGOUT");
            localStorage.removeItem('user-token');
            resolve();
        });
    },
    USER_REQUEST: ({ commit, state }) => {
        return new Promise((resolve, reject) => {
            var authHeader = 'Bearer ' + state.token;
            console.log("authHeader: " + authHeader);
            window.fetch('/api/account',
                {
                    headers: {
                        'Accept': 'application/json, text/plain, */*',
                        'Content-Type': 'application/json',
                        'Authorization': authHeader
                    },
                    method: 'GET'
                }).then(response => {
                    try {
                        return response.json();
                    } catch (e) {
                        reject(e);
                    }
                }).then(response => {
                    console.log(response);
                    if (response.result) {
                        commit("USER_SUCCESS", response.result);
                    } else {
                        localStorage.removeItem('user-token');
                    }
                    resolve(response);
                }).catch(error => {
                    localStorage.removeItem('user-token');
                    reject(error);
                });
        });
    }
}