<template>
    <b-navbar toggleable="md" type="dark" variant="childvac" :sticky="true">
        <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

        <b-navbar-brand to="/">ChildVac</b-navbar-brand>

        <b-collapse id="nav-collapse" is-nav>
            <!-- Right aligned nav items -->
            <b-navbar-nav class="ml-auto">
                <!--<b-nav-form class="mb-0" @submit="onSearchSubmit">
                    <b-form-input size="sm" class="mr-sm-2" placeholder="Что вы хотите найти?"></b-form-input>
                    <b-button variant="light" size="sm" class="my-2 my-sm-0" type="submit">Поиск</b-button>
                </b-nav-form>-->

                <b-nav-item-dropdown v-if="isAuthenticated" right>
                    <template slot="button-content">
                        <font-awesome-icon icon="user" />
                        <strong>{{userName}}</strong>
                    </template>
                    <b-dropdown-item to="/profile">Профиль</b-dropdown-item>
                    <b-dropdown-item @click="logout">Выйти</b-dropdown-item>
                </b-nav-item-dropdown>
            </b-navbar-nav>
        </b-collapse>
    </b-navbar>
</template>

<script src="./navbar.js"></script>
<style src="./navbar.css"></style>