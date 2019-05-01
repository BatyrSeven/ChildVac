<template>
    <div>
        <h3 class="mb-4 text-center">Календарь</h3>

        <b-alert v-for="(alert, index) in alerts" :key="index" :variant="alert.variant" show dismissible>
            <strong>{{alert.title}}</strong>
            <br />
            <span>{{alert.text}}</span>
        </b-alert>

        <vue-event-calendar :events="events"
                            @dayChanged="handleDayChange">
            <template slot-scope="props">
                <div v-for="event in props.showEvents" :key="event.id" class="event-item">
                    <div class="wrapper">
                        <div class="row pt-2">
                            <div class="col-8 calendar_title">
                                <h6 class="mb-0">{{event.child.lastName}}</h6>
                                <p>{{event.child.firstName}} {{event.child.patronim}}</p>
                            </div>
                            <div class="col-4 text-right calendar_datetime">
                                <div>{{event.date}}</div>
                                <div>{{event.time}}</div>
                            </div>
                        </div>
                        <div class="pb-1 mb-2 calendar_title-wrapper">
                            <span class="calendar_desc">ИИН: {{event.child.iin}}</span>
                        </div>
                        <div class="pb-1 mb-2">
                            <span class="calendar_desc">{{event.title}}</span>
                        </div>
                        <div v-for="prescription in event.prescriptions" :key="prescription.id" class="calendar_prescription mb-2 pb-1">
                            <p v-if="prescription.type"
                               class="calendar_prescription-title">
                                {{prescription.type}}
                            </p>
                            <p v-if="prescription.diagnosis"
                               class="calendar_prescription-text">
                                Диагноз: {{prescription.diagnosis}}
                            </p>
                            <p v-if="prescription.medication"
                               class="calendar_prescription-text">
                                Лечение: {{prescription.medication}}
                            </p>
                            <p v-if="prescription.description"
                               class="calendar_prescription-text">
                                Примечания: {{prescription.description}}
                            </p>
                        </div>
                        <div>
                            <b-button class="mr-1 mb-1" variant="success" size="sm" :to="'/create-prescription/' + event.id">Назначение</b-button>
                            <b-button class="mr-1 mb-1" variant="primary" size="sm" :to="'/ticket/' + event.id">Изменить</b-button>
                            <b-button class="mb-1" variant="danger" size="sm" @click="onDeleteTicket(event.id)">Отменить</b-button>
                        </div>
                    </div>
                </div>
            </template>
        </vue-event-calendar>

        <b-modal v-model="modalDeleteShow">
            <p>Вы уверены, что хотите отменить данный прием?</p>

            <div slot="modal-footer">
                <b-button variant="primary"
                          @click="deleteTicket">
                    Да
                </b-button>
                <b-button variant="danger"
                          @click="modalDeleteShow=false">
                    Нет
                </b-button>
            </div>
        </b-modal>
    </div>
</template>

<script src="./calendar.js"></script>
<style src="./calendar.css"></style>