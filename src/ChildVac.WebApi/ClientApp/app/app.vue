<template>
    <div class="pb-3">
        <navbar></navbar>
        <nav-menu></nav-menu>
        <b-container>
            <main role="main" class="pt-3">
                <router-view></router-view>
            </main>
        </b-container>
    </div>
</template>

<script src="./app.js"></script>
<style src="./app.css"></style>