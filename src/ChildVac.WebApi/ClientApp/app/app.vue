<template>
    <div>
        <navbar></navbar>
        <b-container fluid>
            <div class="row">
                <nav-menu></nav-menu>
                <main role="main" class="col-md-9 ml-sm-auto col-lg-10 pt-3">
                    <router-view></router-view>
                </main>
            </div>
        </b-container>
    </div>
</template>

<script src="./app.js"></script>
<style src="./app.css"></style>