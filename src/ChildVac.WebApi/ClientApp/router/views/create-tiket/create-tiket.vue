<template>
    <div>
        <h1 class="mb-4">Запись на прием</h1>

        <b-alert v-for="alert in alerts" :variant="alert.variant" show dismissible>
            <strong>{{alert.title}}</strong>
            <br />
            <span>{{alert.text}}</span>
        </b-alert>

        <b-form @submit="onSubmit" v-if="show">

            <b-form-group v-show="!childId" id="input-group-child-iin" label="ИИН ребёнка:" label-for="input-child-iin" label-cols-md="2" label-align-md="right">
                <b-form-input id="input-child-iin"
                              type="text"
                              v-model="searchChildIin"
                              placeholder="Начните вводить ИИН ребёнка"></b-form-input>
                <b-form-select v-show="children.length" v-model="childId" :options="children" :select-size="children.length + 1"></b-form-select>
            </b-form-group>

            <div class="mb-3" v-show="childId">
                <div class="row">
                    <div class="col-md-2 text-md-right pr-0">Ребёнок:</div>
                    <div class="col">
                        <div class="mb-1" v-html="child"></div>
                        <b-button variant="outline-primary" @click="resetSearchChildSuggestions">Изменить</b-button>
                    </div>
                </div>
            </div>

            <b-form-group id="input-group-date-of-birth" label="Время приема:" label-for="input-date-of-birth" label-cols-md="2" label-align-md="right">
                <div class="row">
                    <div class="col">
                        <b-form-input id="input-date-of-birth"
                                      type="date"
                                      v-model="form.date"
                                      required
                                      placeholder="Дата"></b-form-input>
                    </div>
                    <div class="col">
                        <b-form-input id="input-date-of-birth"
                                      type="time"
                                      v-model="form.time"
                                      required
                                      placeholder="Время"></b-form-input>
                    </div>
                </div>
            </b-form-group>

            <b-button class="offset-md-2 mr-3" type="submit" variant="primary" :disabled="submited">
                <span v-if="submited">
                    <b-spinner small type="grow"></b-spinner>
                    Загрузка...
                </span>
                <span v-else>
                    Сохранить
                </span>
            </b-button>
        </b-form>
    </div>
</template>

<script src="./create-tiket.js"></script>
<style src="./create-tiket.css"></style>