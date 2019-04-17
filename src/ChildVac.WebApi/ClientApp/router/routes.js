export default [
    {
        path: "/", 
        name: "home",
        component: require("./views/home/home.vue").default
    },
    {
        path: "/calendar", 
        name: "calendar",
        component: require("./views/calendar/calendar.vue").default
    },
    {
        path: "/create-prescription", 
        name: "create-prescription",
        component: require("./views/create-prescription/create-prescription.vue").default
    },
    {
        path: "/create-tiket", 
        name: "create-tiket",
        component: require("./views/create-tiket/create-tiket.vue").default
    },
    {
        path: "/register-child", 
        name: "register-child",
        component: require("./views/register-child/register-child.vue").default
    },
    {
        path: "/register-parent", 
        name: "register-parent",
        component: require("./views/register-parent/register-parent.vue").default
    }
]