<template>
    <b-navbar toggleable="md" type="dark" variant="childvac" :sticky="true">

        <b-navbar-brand to="/">ChildVac</b-navbar-brand>

        <b-navbar-toggle target="nav-collapse" v-if="!isAuthenticated"></b-navbar-toggle>

        <b-collapse id="nav-collapse" is-nav v-if="!isAuthenticated">
            <b-navbar-nav>
                <b-nav-item to="faq">FAQ</b-nav-item>
            </b-navbar-nav>
        </b-collapse>

        <b-navbar-nav class="ml-auto" v-if="isAuthenticated">
            <b-nav-item-dropdown right>
                <template slot="button-content">
                    <font-awesome-icon icon="user-md" size="lg" />
                    <strong>{{userName}}</strong>
                </template>
                <b-dropdown-item @click="logout">Выйти</b-dropdown-item>
            </b-nav-item-dropdown>
        </b-navbar-nav>
        
    </b-navbar>
</template>

<script src="./navbar.js"></script>
<style src="./navbar.css"></style>