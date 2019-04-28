export default [
    {
        path: "/", 
        name: "home",
        component: require("./views/home/home.vue").default,
        meta: { requiresUnauth: true }
    },
    {
        path: "/calendar", 
        name: "calendar",
        component: require("./views/calendar/calendar.vue").default,
        meta: { requiresAuth: true }
    },
    {
        path: "/create-prescription/:ticket_id", 
        name: "create-prescription",
        component: require("./views/create-prescription/create-prescription.vue").default,
        meta: { requiresAuth: true }
    },
    {
        path: "/tiket", 
        name: "tiket",
        component: require("./views/tiket/tiket.vue").default,
        meta: { requiresAuth: true }
    },
    {
        path: "/register-child", 
        name: "register-child",
        component: require("./views/register-child/register-child.vue").default,
        meta: { requiresAuth: true }
    },
    {
        path: "/register-parent", 
        name: "register-parent",
        component: require("./views/register-parent/register-parent.vue").default,
        meta: { requiresAuth: true }
    },
    {
        path: "*",
        name: "not-found",
        component: require("./views/not-found/not-found.vue").default
    }
]