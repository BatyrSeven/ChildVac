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

            <b-form-group id="input-group-date-of-birth" label="Дата рождения:" label-for="input-date-of-birth">
                <b-form-input id="input-date-of-birth"
                              type="date"
                              v-model="form.dateOfBirth"
                              required
                              placeholder="Введите дату рождения"></b-form-input>
            </b-form-group>

            <!--<b-form-group label="Укажите пол:">
            <b-form-radio-group id="input-group-gender" v-model="form.gender" required>
                <b-form-radio value="male">Мужской</b-form-radio>
                <b-form-radio value="female">Женский</b-form-radio>
            </b-form-radio-group>
        </b-form-group>-->

            <b-form-group id="input-group-iin" label="ИИН, если есть:" label-for="input-iin">
                <b-form-input id="input-iin"
                              type="number"
                              v-model="form.iin"
                              placeholder="Введите ИИН"></b-form-input>
            </b-form-group>

            <!--<b-form-group id="input-group-parent-iin" label="ИИН родителя, можно указать позже:" label-for="input-parent-iin">
            <b-form-input id="input-parent-iin"
                          type="number"
                          v-model="form.parentIin"
                          placeholder="Введите ИИН родителя"></b-form-input>
        </b-form-group>-->

            <b-button type="submit" variant="primary">Зарегистрировать</b-button>
            <b-button type="reset" variant="danger">Сбросить</b-button>
        </b-form>
    </div>
</template>

<script src="./register-child.js"></script>
<style src="./register-child.css"></style>