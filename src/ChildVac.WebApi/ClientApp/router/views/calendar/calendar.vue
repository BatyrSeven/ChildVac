<template>
    <div>
        <h1 class="mb-4">Календарь</h1>

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
                            <b-button class="mr-1 mb-1" variant="primary" size="sm">Изменить</b-button>
                            <b-button class="mb-1" variant="danger" size="sm">Отменить</b-button>
                        </div>
                    </div>
                </div>
            </template>
        </vue-event-calendar>
    </div>
</template>

<script src="./calendar.js"></script>
<style src="./calendar.css"></style>