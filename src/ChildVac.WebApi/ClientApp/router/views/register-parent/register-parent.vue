<template>
    <div>
        <h1>Регистрация родителя</h1>

        <div v-show="alert.show" :class="alert.className" class="alert" role="alert" v-html="alert.text"></div>

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
                    <b-form-radio value="male">Мужской</b-form-radio>
                    <b-form-radio value="female">Женский</b-form-radio>
                </b-form-radio-group>
            </b-form-group>

            <b-form-group id="input-group-address" label="Адрес проживания:" label-for="input-address">
                <b-form-input id="input-address"
                              type="text"
                              v-model="form.address"
                              placeholder="Введите текущий адрес проживания"></b-form-input>
            </b-form-group>

            <b-button type="submit" variant="primary">Зарегистрировать</b-button>
            <b-button type="reset" variant="danger">Сбросить</b-button>
        </b-form>
    </div>
</template>

<script src="./register-parent.js"></script>
<style src="./register-parent.css"></style>