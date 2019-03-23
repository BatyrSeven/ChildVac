// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var tokenKey = "accessToken";
        $('#submitLogin').click(function (e) {
            e.preventDefault();
            var loginData = {
                login: $('#login').val(),
                password: $('#password').val()
            };

            $.ajax({
                type: 'POST',
                url: '/api/account',
                data: JSON.stringify(loginData),
                contentType: "application/json",
                success: function(data) {
                    $('.userName').text(data.login);
                    $('.userInfo').css('display', 'block');
                    $('.loginForm').css('display', 'none');
                    // сохраняем в хранилище sessionStorage токен доступа
                    sessionStorage.setItem(tokenKey, data.token);
                    console.log(data.token);
                },
                fail: function(data) {
                    console.log(data);
                    console.log(data.responseText);
                }
            });
        });

        $('#logOut').click(function (e) {
            e.preventDefault();
            $('.loginForm').css('display', 'block');
            $('.userInfo').css('display', 'none');
            sessionStorage.removeItem(tokenKey);
        });

        $('#getDataByLogin').click(function (e) {
            e.preventDefault();
            $.ajax({
                type: 'GET',
                url: '/api/account/getlogin',
                beforeSend: function (xhr) {

                    var token = sessionStorage.getItem(tokenKey);
                    xhr.setRequestHeader("Authorization", "Bearer " + token);
                },
                success: function (data) {
                    alert(data);
                },
                fail: function (data) {
                    console.log(data);
                }
            });
        });

        $('#getDataByRole').click(function (e) {
            e.preventDefault();
            $.ajax({
                type: 'GET',
                url: '/api/account/getrole',
                beforeSend: function (xhr) {

                    var token = sessionStorage.getItem(tokenKey);
                    xhr.setRequestHeader("Authorization", "Bearer " + token);
                },
                success: function (data) {
                    alert(data);
                },
                fail: function (data) {
                    console.log(data);
                }
            });
        });