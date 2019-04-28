<template>
    <div>
        <h1 class="mb-4">Календарь</h1>

        <b-alert v-for="alert in alerts" :variant="alert.variant" show dismissible>
            <strong>{{alert.title}}</strong>
            <br />
            <span>{{alert.text}}</span>
        </b-alert>

        <vue-event-calendar :events="events"
                            @dayChanged="handleDayChange">
            <template scope="props">
                <div v-for="event in props.showEvents" :key="event.id" class="event-item">

                    <div class="wrapper">
                        <div class="row pt-2">
                            <div class="col-8 calendar_title">
                                <h6>{{event.childName}}</h6>
                            </div>
                            <div class="col-4 text-right calendar_datetime">
                                <span>{{event.date}} {{event.time}}</span>
                            </div>
                        </div>
                        <div class="pb-1 mb-2 calendar_title-wrapper">
                            <span class="calendar_desc">ИИН: {{event.childIin}}</span>
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