<template>
    <div>
        <h3 class="mb-4 text-center">Часто задаваемые вопросы</h3>

        <b-card v-for="(advice, index) in advices" 
                :key="index" 
                :title="advice.title"
                class="mb-3">
            <b-card-text>
                {{advice.text}}
            </b-card-text>
        </b-card>
    </div>
</template>

<script src="./faq.js"></script>
<style src="./faq.css"></style>