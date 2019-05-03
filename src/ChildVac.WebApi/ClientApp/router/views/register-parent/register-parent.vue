<template>
    <div>
        <h3 class="mb-4 text-center">Регистрация родителя</h3>

        <b-alert v-for="(alert, index) in alerts" :key="index" :variant="alert.variant" show dismissible>
            <strong>{{alert.title}}</strong>
            <br />
            <span>{{alert.text}}</span>
        </b-alert>

        <b-form @submit="onSubmit" v-if="show">
            <b-form-group id="input-group-first-name" label="Имя:" label-for="input-first-name" label-cols-md="2" label-align-md="right">
                <b-form-input id="input-first-name"
                              v-model="form.firstName"
                              type="text"
                              required
                              placeholder="Введите имя"></b-form-input>
            </b-form-group>

            <b-form-group id="input-group-last-name" label="Фамилия:" label-for="input-last-name" label-cols-md="2" label-align-md="right">
                <b-form-input id="input-last-name"
                              v-model="form.lastName"
                              type="text"
                              required
                              placeholder="Введите фамилию"></b-form-input>
            </b-form-group>

            <b-form-group id="input-group-patronim" label="Отчество:" label-for="input-patronim" label-cols-md="2" label-align-md="right">
                <b-form-input id="input-patronim"
                              v-model="form.patronim"
                              placeholder="Введите отчество"></b-form-input>
            </b-form-group>

            <b-form-group id="input-group-iin" label="ИИН:" label-for="input-iin" label-cols-md="2" label-align-md="right">
                <b-form-input id="input-iin"
                              type="number"
                              v-model="form.iin"
                              required
                              placeholder="Введите ИИН"></b-form-input>
            </b-form-group>

            <b-form-group label="Укажите пол:" label-for="input-group-gender" label-cols-md="2" label-align-md="right">
                <b-form-radio-group id="input-group-gender" class="pt-2" v-model="form.gender" required>
                    <b-form-radio value="male">Мужской</b-form-radio>
                    <b-form-radio value="female">Женский</b-form-radio>
                </b-form-radio-group>
            </b-form-group>

            <b-form-group id="input-group-phone" label="Телефон:" label-for="input-phone" label-cols-md="2" label-align-md="right">
                <b-form-input id="input-phone"
                              type="tel"
                              v-model="form.phoneNumber"
                              placeholder="Введите номер телефона"></b-form-input>
            </b-form-group>

            <b-form-group id="input-group-address" label="Адрес проживания:" label-for="input-address" label-cols-md="2" label-align-md="right">
                <b-form-input id="input-address"
                              type="text"
                              v-model="form.address"
                              placeholder="Введите текущий адрес проживания"></b-form-input>
            </b-form-group>

            <b-button class="offset-md-2 mr-3" type="submit" variant="primary" :disabled="submited">
                <span v-if="submited">
                    <b-spinner small type="grow"></b-spinner>
                    Загрузка...
                </span>
                <span v-else>
                    Зарегистрировать
                </span>
            </b-button>
        </b-form>
    </div>
</template>

<script src="./register-parent.js"></script>
<style src="./register-parent.css"></style>