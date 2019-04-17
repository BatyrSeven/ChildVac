<template>
    <b-nav class="col-md-2 d-none d-md-block bg-light sidebar">
        <div class="sidebar-sticky">
            <ul class="nav flex-column">
                <b-nav-item to="/calendar">
                    Календарь
                </b-nav-item>
                <b-nav-item to="/create-tiket">
                    Запись на прием
                </b-nav-item>
                <b-nav-item to="/create-prescription">
                    Выписать назначение
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
    </b-nav>
</template>

<script src="./nav.js"></script>
<style src="./nav.css"></style>