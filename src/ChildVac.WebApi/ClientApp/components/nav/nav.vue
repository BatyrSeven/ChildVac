<template>
    <b-nav class="col-md-2 d-none d-md-block bg-light sidebar">
        <div v-if="isAuthenticated" class="sidebar-sticky">
            <ul class="nav flex-column">
                <b-nav-item to="/calendar">
                    Календарь
                </b-nav-item>
                <b-nav-item to="/ticket">
                    Запись на прием
                </b-nav-item>
            </ul>

            <h6 class="sidebar-heading d-flex justify-content-between align-items-center px-3 mt-4 mb-1 text-muted">
                <span>Регистрация</span>
            </h6>
            <ul class="nav flex-column">
                <b-nav-item to="/register-child">
                    Регистрация ребенка
                </b-nav-item>
                <b-nav-item to="/register-parent">
                    Регистрация родителя
                </b-nav-item>
            </ul>
        </div>
        <div v-else class="sidebar-sticky">
            <ul class="nav flex-column">
                <b-nav-item to="/">
                    Главная
                </b-nav-item>
                <b-nav-item to="/login">
                    Авторизация
                </b-nav-item>
            </ul>
        </div>
    </b-nav>
</template>

<script src="./nav.js"></script>
<style src="./nav.css"></style>