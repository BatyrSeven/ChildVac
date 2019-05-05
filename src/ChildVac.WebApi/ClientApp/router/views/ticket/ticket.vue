<template>
    <div>
        <h3 class="mb-4 text-center" v-if="ticket_id">Изменение записи на прием</h3>
        <h3 class="mb-4 text-center" v-else>Создание записи на прием</h3>

        <b-alert v-for="(alert, index) in alerts" :key="index" :variant="alert.variant" show dismissible>
            <strong>{{alert.title}}</strong>
            <br />
            <span>{{alert.text}}</span>
        </b-alert>

        <b-form @submit="onSubmit" v-if="show">

            <b-row v-if="ticket_id" class="mb-3">
                <b-col sm="2" class="text-md-right">
                    <span>Пациент:</span>
                </b-col>
                <b-col sm="10">
                    <strong>{{child}}</strong>
                </b-col>
            </b-row>

            <div v-else>
                <b-form-group id="input-group-child-iin" label="ИИН ребёнка:" label-for="input-child-iin" label-cols-md="2" label-align-md="right">
                    <b-form-input id="input-child-iin"
                                  class="mb-3"
                                  type="text"
                                  v-model="searchChildIin"
                                  placeholder="Начните вводить ИИН ребёнка"></b-form-input>
                    <b-form-select v-model="form.childId"
                                   :options="children">
                        <template slot="first">
                            <option :value="null" disabled>Выберите из поиска</option>
                        </template>
                    </b-form-select>
                </b-form-group>
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

            <b-form-group label="Тип приема:" label-for="input-group-ticket-type" label-cols-md="2" label-align-md="right">
                <b-form-radio-group id="input-group-ticket-type" class="pt-2" v-model="form.ticketType" required>
                    <b-form-radio value="1">Консультация</b-form-radio>
                    <b-form-radio value="2">Вакцинация</b-form-radio>
                </b-form-radio-group>
            </b-form-group>

            <b-form-group v-if="form.ticketType == 2" label="Вакцина:" label-for="input-group-vaccine" label-cols-md="2" label-align-md="right">
                <b-form-select v-model="form.vaccineId" id="input-group-vaccine" :options="vaccines">
                    <template slot="first">
                        <option :value="null" disabled>Выберите вакцину</option>
                    </template>
                </b-form-select>
            </b-form-group>

            <b-form-group id="input-group-room" label="Кабинет:" label-for="input-room"
                          label-cols-md="2" label-align-md="right">
                <b-form-input id="input-room"
                              v-model="form.room"
                              type="number"
                              required
                              placeholder="Введите номер кабинета"></b-form-input>
            </b-form-group>

            <b-row>
                <b-col sm="2" class="text-md-right">
                    <label for="textarea-title">Заголовок:</label>
                </b-col>
                <b-col sm="10">
                    <b-form-textarea id="textarea-title"
                                     class="mb-2"
                                     v-model="form.title"
                                     placeholder=""
                                     rows="2"
                                     max-rows="10"></b-form-textarea>
                </b-col>
            </b-row>

            <b-row>
                <b-col sm="2" class="text-md-right">
                    <label for="textarea-text">Текст:</label>
                </b-col>
                <b-col sm="10">
                    <b-form-textarea id="textarea-text"
                                     class="mb-2"
                                     v-model="form.text"
                                     placeholder=""
                                     rows="2"
                                     max-rows="10"></b-form-textarea>
                </b-col>
            </b-row>

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

<script src="./ticket.js"></script>
<style src="./ticket.css"></style>