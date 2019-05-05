<template>
    <div class="input-group col" style='padding:0'>
        <div class="input-group-prepend">
            <span class="input-group-text">
                <font-awesome-icon icon="search" />
            </span>
        </div>
        <input type="text"
               class="form-control"
               v-model="searchQuery"
               placeholder="Поиск"
               @keyup.enter="addItem"
               @keyup.up="prevItem"
               @keyup.down="nextItem"
               @keyup.esc="close" />
        <select class="form-control d-inline"
                v-model="selectedItem"
                @change="manualChange">
            <option :disabled="firstDisabled" value="0">
                <template v-if="addedItem == 0 && !firstDisabled">
                    =>
                </template> {{firstLabel}}
            </option>
            <option v-for="item in searchedItems" :key="item.id" :value="item[returns]">
                <template v-if="addedItem == item[returns]">
                    =>
                </template> {{formOptionText(item)}}
            </option>
            <div class="input-group-append" v-if="addBtn">
                <button class="btn btn-primary" @click="addItem"></button>
            </div>
        </select>
    </div>
</template>

<script src="./select-search.js"></script>
<!--<style src="./select-search.css"></style>-->