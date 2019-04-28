<template>
    <div v-if="isAuthenticated" class="text-center" style="overflow-x: auto;">
        <div class="nav_button-wrapper pt-3 pb-1">
            <b-button to="/calendar" :variant="getButtonVariant('calendar')" class="nav_button">
                <font-awesome-icon icon="calendar-week" size="3x" />
                <p class="mt-2 mb-0">Календарь</p>
            </b-button>
            <b-button to="/ticket" :variant="getButtonVariant('ticket')" class="nav_button">
                <font-awesome-icon icon="pen" size="3x" />
                <p class="mt-2 mb-0">Запись на прием</p>
            </b-button>
            <b-button to="/register-child" :variant="getButtonVariant('register-child')" class="nav_button">
                <font-awesome-icon icon="baby" size="3x" />
                <p class="mt-2 mb-0">Регистрация ребенка</p>
            </b-button>
            <b-button to="/register-parent" :variant="getButtonVariant('register-parent')" class="nav_button">
                <font-awesome-icon icon="user-plus" size="3x" />
                <p class="mt-2 mb-0">Регистрация родителя</p>
            </b-button>
        </div>
    </div>
</template>

<script src="./nav.js"></script>
<style src="./nav.css"></style>