<template>
    <div class="pb-3">
        <navbar></navbar>
        <b-container>
            <nav-menu></nav-menu>
            <main role="main" class="pt-3">
                <router-view></router-view>
            </main>
        </b-container>
    </div>
</template>

<script src="./app.js"></script>
<style src="./app.css"></style>