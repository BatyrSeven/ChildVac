export default {
    props: {
        value: {
            required: true
        },
        items: {
            type: Array,
            required: true
        },
        returns: {
            type: String,
            default: "id"
        },
        shows: {
            type: String,
            required: true
        },
        firstLabel: {
            type: String,
            default: "Please select"
        },
        firstDisabled: {
            type: Boolean,
            default: true
        },
        search: {
            type: String,
            default: ""
        },
        addBtn: {
            type: Boolean,
            default: false
        }
    },
    data() {
        return {
            searchQuery: "",
            addedItem: this.value || 0,
            selectedItem: this.value || 0
        }
    },
    computed: {
        columns() {
            if (!this.items.length) return [];
            return Object.keys(this.items[0]);
        },
        searchColumns() {
            if (!this.search.length) return this.columns;
            return this.search.split("|");
        },
        searchedItems() {
            if (!this.searchQuery) {
                this.selectedItem = 0;
                if (this.addedItem !== 0 && this.checkItemExists(this.addedItem)) this.selectedItem = this.addedItem;
                else this.addedItem = 0;
                return this.items;
            }

            let items = this.items.filter(item => {
                return this.searchColumns.some((column) => {
                    return String(item[column]).toLowerCase().indexOf(this.searchQuery.toLowerCase()) > -1;
                });
            });

            if (items.length) this.selectedItem = items[0][this.returns];
            else this.selectedItem = 0;

            return items;
        }
    },
    methods: {
        checkItemExists(item) {
            return this.items.map(this.returns).indexOf(item) > -1;
        },
        addItem() {
            //if(this.selectedItem == 0) return
            this.addedItem = this.selectedItem;

            if (this.addedItem === 0 && this.firstDisabled) return;

            this.$emit("input", this.addedItem);
            this.$emit("selected");
        },
        prevItem() {
            if (!this.items.length) return;

            let i = this.searchedItems.map(itm => itm[this.returns]).indexOf(this.selectedItem);

            if (i === 0) {
                if (this.firstDisabled)
                    this.selectedItem = this.searchedItems[this.searchedItems.length - 1][this.returns];
                else this.selectedItem = 0;
            } else if (this.selectedItem === 0)
                this.selectedItem = this.searchedItems[this.searchedItems.length - 1][this.returns];
            else this.selectedItem = this.searchedItems[i - 1][this.returns];
        },
        nextItem() {
            if (!this.items.length) return;

            let i = this.searchedItems.map(itm => itm[this.returns]).indexOf(this.selectedItem);

            if (i === this.searchedItems.length - 1) {
                if (this.firstDisabled) this.selectedItem = this.searchedItems[0][this.returns];
                else this.selectedItem = 0;
            } else this.selectedItem = this.searchedItems[i + 1][this.returns];
        },
        close() {
            this.$emit('close ');
        },
        formOptionText(item) {
            let j = [];
            let s = this.shows.split('|');
            s.forEach(x => j.push(item[x]));

            return j.join(' | ');
        },
        getCount() {
            return this.firstDisabled ? this.searchedItems.length : this.searchedItems.length + 1;
        },
        manualChange() {
            this.addedItem = this.selectedItem;
            this.$emit('input', this.addedItem);
        }
    }
}