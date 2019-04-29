<template>
    <div>
        <h3 class="mb-4 text-center">Регистрация ребенка</h3>

        <b-alert v-for="(alert, index) in alerts" :key="index" :variant="alert.variant" show dismissible>
            <strong>{{alert.title}}</strong>
            <br />
            <span>{{alert.text}}</span>
        </b-alert>

        <b-form @submit="onSubmit" v-if="show">
            <b-form-group id="input-group-first-name" label="Имя:" label-for="input-first-name" label-cols-md="2" label-align-md="right">
                <b-form-input id="input-first-name"
                              v-model="form.firstName"
                              required
                              placeholder="Введите имя"></b-form-input>
            </b-form-group>

            <b-form-group id="input-group-last-name" label="Фамилия:" label-for="input-last-name" label-cols-md="2" label-align-md="right">
                <b-form-input id="input-last-name"
                              v-model="form.lastName"
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

            <b-form-group id="input-group-date-of-birth" label="Дата рождения:" label-for="input-date-of-birth" label-cols-md="2" label-align-md="right">
                <b-form-input id="input-date-of-birth"
                              type="date"
                              v-model="form.dateOfBirth"
                              required
                              placeholder="Введите дату рождения"></b-form-input>
            </b-form-group>

            <b-form-group label="Укажите пол:" label-for="input-group-gender" label-cols-md="2" label-align-md="right">
                <b-form-radio-group id="input-group-gender" class="pt-2" v-model="form.gender" required>
                    <b-form-radio value="1">Мужской</b-form-radio>
                    <b-form-radio value="2">Женский</b-form-radio>
                </b-form-radio-group>
            </b-form-group>

            <b-form-group v-show="!parentId" id="input-group-parent-iin" label="ИИН родителя:" label-for="input-parent-iin" label-cols-md="2" label-align-md="right">
                <b-form-input id="input-parent-iin"
                              type="text"
                              v-model="searchParentIin"
                              placeholder="Начните вводить ИИН родителя"></b-form-input>
                <b-form-select v-show="parents.length" v-model="parentId" :options="parents" :select-size="parents.length + 1"></b-form-select>
            </b-form-group>

            <div class="mb-3" v-show="parentId">
                <div class="row">
                    <div class="col-md-2 text-md-right pr-0">
                        Родитель:
                    </div>
                    <div class="col">
                        <div class="mb-1" v-html="parent"></div>
                        <b-button variant="outline-primary" @click="resetSearchParentSuggestions">Изменить</b-button>
                    </div>
                </div>
            </div>

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

<script src="./register-child.js"></script>
<style src="./register-child.css"></style>