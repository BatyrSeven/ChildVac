﻿<template>
    <div>
        <h3 class="mb-4 text-center">Оценить приложение</h3>

        <b-alert v-for="(alert, index) in alerts" :key="index" :variant="alert.variant" show dismissible>
            <strong>{{alert.title}}</strong>
            <br />
            <span>{{alert.text}}</span>
        </b-alert>

        <b-form @submit="onSubmit">
            <b-form-group id="input-group-rate" label="Оценка:" label-for="input-rate" label-cols-md="2" label-align-md="right">
                <b-form-select id="input-rate" v-model="form.rate" :options="rateOptions" required></b-form-select>
            </b-form-group>

            <b-row>
                <b-col sm="2" class="text-md-right">
                    <label for="textarea-small">Отзыв:</label>
                </b-col>
                <b-col sm="10">
                    <b-form-textarea id="textarea"
                                     class="mb-2"
                                     v-model="form.text"
                                     placeholder=""
                                     rows="2"
                                     required
                                     max-rows="10"></b-form-textarea>
                </b-col>
            </b-row>

            <b-button class="offset-md-2 mr-3" type="submit" variant="primary" :disabled="submited">
                <span v-if="submited">
                    <b-spinner small type="grow"></b-spinner>
                    Загрузка...
                </span>
                <span v-else>
                    Отправить
                </span>
            </b-button>
        </b-form>
    </div>
</template>

<script src="./rate.js"></script>
<style src="./rate.css"></style>