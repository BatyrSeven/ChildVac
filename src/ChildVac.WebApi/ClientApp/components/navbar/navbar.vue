<template>
    <b-navbar toggleable="md" type="dark" variant="childvac" :sticky="true">

        <b-navbar-brand to="/">ChildVac</b-navbar-brand>

        <b-navbar-nav class="ml-auto">
            <b-nav-item-dropdown v-if="isAuthenticated" right>
                <template slot="button-content">
                    <font-awesome-icon icon="user-md" size="lg" />
                    <strong>{{userName}}</strong>
                </template>
                <!--<b-dropdown-item to="/profile">Профиль</b-dropdown-item>-->
                <b-dropdown-item @click="logout">Выйти</b-dropdown-item>
            </b-nav-item-dropdown>
        </b-navbar-nav>
    </b-navbar>
</template>

<script src="./navbar.js"></script>
<style src="./navbar.css"></style>