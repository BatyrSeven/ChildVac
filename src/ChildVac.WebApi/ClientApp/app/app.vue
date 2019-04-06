<template>
    <div>
        <navbar></navbar>
        <b-container fluid>
            <div class="row">
                <nav-menu></nav-menu>
                <main role="main" class="col-md-9 ml-sm-auto col-lg-10 pt-3 px-4">
                    <transition>
                        <router-view></router-view>
                    </transition>
                </main>
            </div>
        </b-container>
    </div>
</template>

<script src="./app.js"></script>
<style src="./app.css"></style>