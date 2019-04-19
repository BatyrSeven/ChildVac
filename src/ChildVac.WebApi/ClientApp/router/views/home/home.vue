<template>
    <div>
        <!--<h2>Добро пожаловать!</h2>-->

        <h4 class="text-center mb-3">Авторизация для врачей</h4>

        <div class="home__formWrapper mb-4">

            <div v-show="alert.show" :class="alert.className" class="alert" role="alert" v-html="alert.text"></div>

            <b-form @submit="onSubmit" v-if="show">
                <b-form-group id="input-group-1"
                              label="Ваш ИИН:"
                              label-for="input-1">
                    <b-form-input id="input-1"
                                  v-model="iin"
                                  type="number"
                                  :state="iinState"
                                  aria-describedby="iin-minlength-feedback"
                                  placeholder="Введите ИИН"></b-form-input>

                    <b-form-invalid-feedback id="iin-minlength-feedback">
                        ИИН состоит из 12 символов
                    </b-form-invalid-feedback>
                </b-form-group>

                <b-form-group id="input-group-2" label="Пароль:" label-for="input-2">
                    <b-form-input id="input-2"
                                  v-model="password"
                                  type="password"
                                  :state="passwordState"
                                  aria-describedby="password-minlength-feedback"
                                  placeholder="Введите пароль"></b-form-input>

                    <b-form-invalid-feedback id="password-minlength-feedback">
                        Введите пароль
                    </b-form-invalid-feedback>
                </b-form-group>

                <b-button class="mr-3" type="submit" variant="primary">Войти</b-button>
                <b-button to="forgot-password" variant="outline-danger">Забыли пароль?</b-button>
            </b-form>
        </div>

        <h4 class="text-center  mb-2">Мобильное приложение ChildVac для родителей</h4>

        <p class="text-center">
            Установите мобильное приложение ChildVac и следите расписанием прививок ваших детей.
        </p>

        <div class="home__carouselWrapper mb-3">
            <b-carousel id="carousel-1"
                        v-model="slide"
                        :interval="4000"
                        background="#ffffff"
                        img-width="300"
                        img-height="480">

                <b-carousel-slide>
                    <img slot="img"
                         class="d-block img-fluid"
                         src="../../../images/app-screens/schedule.png"
                         alt="">
                </b-carousel-slide>

                <b-carousel-slide>
                    <img slot="img"
                         class="d-block img-fluid"
                         src="../../../images/app-screens/prescriptions.png"
                         alt="">
                </b-carousel-slide>

                <b-carousel-slide>
                    <img slot="img"
                         class="d-block img-fluid"
                         src="../../../images/app-screens/tickets.png"
                         alt="">
                </b-carousel-slide>

            </b-carousel>
        </div>

        <h4 class="text-center">О проекте</h4>

        <p>
            В Казахстане с рождения и до одного года  ребенок получает 10 прививок, две из которых - против туберкулеза и вирусного гепатита «В» - в первые четыре дня жизни.
            Остальные 8 прививок согласно национальному календарю профилактических прививок ребенок получает в 2,3,4 и 12-15 месяцев, при этом вакцинируется против коклюша,
            дифтерии, столбняка, гемофильной инфекции типа b, полиомиелита, пневмококковой инфекции, паротита, кори, вирусного гепатита «В». Детям необходимо прививаться при
            наступлении того или иного возраста, однако сроки прививок для грудных детей определяет лечащий врач, исходя из особенностей имунной системы малыша. Если ввести
            ребенку вакцину раньше назначенного срока, его иммунная система не сможет правильно «ответить», а если позже,  возникает риск, что за это время малыш заболеет.
        </p>

        <p>
            Данное приложение автоматизации процесса получения клинического лечения может облегчить работу медсестёр, врачей поликлиники и родителей прививающихся детей
            за счет уменьшения ручного труда и увеличения производительности. Более того, проект реализует планы развития Казахстана, благодаря программе, которую проводил
            национальный лидер Нурсултан Назарбаев.
        </p>

        <p>В программу входит:</p>

        <ul>
            <li>Предоставление качественных и доступных медицинских услуг;</li>
            <li>Диагностирование и лечение максимально широкого спектра болезней;</li>
            <li>Профилактическая медицина;</li>
            <li>Внедрение услуг «смарт-медицины», дистанционной профилактики и лечения, «электронной медицины»;</li>
            <li>Охват всех детей в возрасте до 16 лет всем спектром медицинского обслуживания;</li>
        </ul>

        <hr />

        <p>ChildVac © 2019</p>

    </div>
</template>

<script src="./home.js"></script>
<style src="./home.css"></style>