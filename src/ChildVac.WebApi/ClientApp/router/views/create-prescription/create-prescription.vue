<template>
    <div>
        <h3 class="mb-4 text-center">Создать назначение</h3>

        <b-alert v-for="(alert, index) in alerts" :key="index" :variant="alert.variant" show dismissible>
            <strong>{{alert.title}}</strong>
            <br />
            <span>{{alert.text}}</span>
        </b-alert>

        <b-form @submit="onSubmit">

            <b-row class="mb-2">
                <b-col sm="2" class="text-md-right">
                    <span>Пациент:</span>
                </b-col>
                <b-col sm="10">
                    <strong>{{child.lastName}} {{child.firstName}} {{child.patronim}}</strong>
                </b-col>
            </b-row>

            <b-row class="mb-2">
                <b-col sm="2" class="text-md-right">
                    <span>ИИН:</span>
                </b-col>
                <b-col sm="10">
                    <strong>{{child.iin}}</strong>
                </b-col>
            </b-row>

            <b-row>
                <b-col sm="2" class="text-md-right">
                    <label for="textarea-small">Диагноз:</label>
                </b-col>
                <b-col sm="10">
                    <b-form-textarea id="textarea"
                                     class="mb-2"
                                     v-model="form.diagnosis"
                                     placeholder=""
                                     rows="2"
                                     required
                                     max-rows="10"></b-form-textarea>
                </b-col>
            </b-row>

            <b-row>
                <b-col sm="2" class="text-md-right">
                    <label for="textarea-small">Примечания:</label>
                </b-col>
                <b-col sm="10">
                    <b-form-textarea id="textarea"
                                     class="mb-2"
                                     v-model="form.description"
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
                    Создать
                </span>
            </b-button>

        </b-form>
    </div>
</template>

<script src="./create-prescription.js"></script>
<style src="./create-prescription.css"></style>