<template>
    <div>
        <navbar></navbar>
        <b-container fluid>
            <div class="row">
                <nav-menu></nav-menu>
                <main role="main" class="col-md-9 ml-sm-auto col-lg-10 pt-3">

                    <div v-for="alert in alerts" :class="alert.className" class="alert" role="alert">
                        <strong>{{alert.title}}</strong>
                        <br />
                        <span>{{alert.text}}</span>
                    </div>

                    <router-view></router-view>

                </main>
            </div>
        </b-container>
    </div>
</template>

<script src="./app.js"></script>
<style src="./app.css"></style>