<template>
    <div>
        <h1>Регистрация ребенка</h1>

        <div v-show="alert.show" :class="alert.className" class="alert" role="alert" v-text="alert.text"></div>

        <b-form @submit="onSubmit" @reset="onReset" v-if="show">
            <b-form-group id="input-group-first-name" label="Имя:" label-for="input-first-name">
                <b-form-input id="input-first-name"
                              v-model="form.firstName"
                              required
                              placeholder="Введите имя"></b-form-input>
            </b-form-group>

            <b-form-group id="input-group-last-name" label="Фамилия:" label-for="input-last-name">
                <b-form-input id="input-last-name"
                              v-model="form.lastName"
                              required
                              placeholder="Введите фамилию"></b-form-input>
            </b-form-group>

            <b-form-group id="input-group-patronim" label="Отчество:" label-for="input-patronim">
                <b-form-input id="input-patronim"
                              v-model="form.patronim"
                              placeholder="Введите отчество"></b-form-input>
            </b-form-group>

            <b-form-group id="input-group-iin" label="ИИН:" label-for="input-iin">
                <b-form-input id="input-iin"
                              type="number"
                              v-model="form.iin"
                              required
                              placeholder="Введите ИИН"></b-form-input>
            </b-form-group>

            <b-form-group label="Укажите пол:">
                <b-form-radio-group id="input-group-gender" v-model="form.gender" required>
                    <b-form-radio value="1">Мужской</b-form-radio>
                    <b-form-radio value="2">Женский</b-form-radio>
                </b-form-radio-group>
            </b-form-group>

            <b-form-group v-show="!parentId" id="input-group-parent-iin" label="ИИН родителя:" label-for="input-parent-iin">
                <b-form-input id="input-parent-iin"
                              type="text"
                              v-model="searchParentIin"
                              placeholder="Начните вводить ИИН родителя"></b-form-input>
                <b-form-select v-show="parents.length" v-model="parentId" :options="parents" :select-size="parents.length + 1"></b-form-select>
            </b-form-group>

            <div class="mb-3" v-show="parentId">
                <p class="mb-1">Родитель:</p>
                <div class="mb-1" v-html="parent"></div>
                <b-button variant="primary" @click="resetSearchParentSuggestions">Изменить</b-button>
            </div>

            <b-button type="submit" variant="primary">Зарегистрировать</b-button>
            <b-button type="reset" variant="danger">Сбросить</b-button>
        </b-form>
    </div>
</template>

<script src="./register-child.js"></script>
<style src="./register-child.css"></style>