<template>
    <div v-if="isAuthenticated" class="text-center" style="overflow-x: auto;">
        <div class="nav_button-wrapper pt-3 pb-1">
            <b-button to="/calendar" :variant="getButtonVariant('calendar')" class="nav_button">
                <font-awesome-icon icon="calendar-week" size="2x" />
                <p class="mt-2 mb-0 nav_text-wrapper">
                    <span class="nav_text">Календарь</span>
                </p>
            </b-button>
            <b-button to="/ticket" :variant="getButtonVariant('ticket')" class="nav_button">
                <font-awesome-icon icon="pen" size="2x" />
                <p class="mt-2 mb-0 nav_text-wrapper">
                    <span class="nav_text">Запись на прием</span>
                </p>
            </b-button>
            <b-button to="/register-child" :variant="getButtonVariant('register-child')" class="nav_button">
                <font-awesome-icon icon="baby" size="2x" />
                <p class="mt-2 mb-0 nav_text-wrapper">
                    <span class="nav_text">Регистрация ребенка</span>
                </p>
            </b-button>
            <b-button to="/register-parent" :variant="getButtonVariant('register-parent')" class="nav_button">
                <font-awesome-icon icon="user-plus" size="2x" />
                <p class="mt-2 mb-0 nav_text-wrapper">
                    <span class="nav_text">Регистрация родителя</span>
                </p>
            </b-button>
        </div>
    </div>
</template>

<script src="./nav.js"></script>
<style src="./nav.css"></style>