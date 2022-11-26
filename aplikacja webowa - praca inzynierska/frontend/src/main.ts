import BootstrapVue3 from 'bootstrap-vue-3';
import { createApp } from 'vue';
import App from './App.vue';

import router from './router';

import 'bootstrap-vue-3/dist/bootstrap-vue-3.css';
import 'bootstrap/dist/css/bootstrap.css';

import './assets/main.css';

/* import the fontawesome core */
import { library } from '@fortawesome/fontawesome-svg-core';

/* import font awesome icon component */
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';

/* import specific icons */
import { faFilter, faFolderTree, faSearch, faSort, faSortAsc, faSortDesc, faXmark } from '@fortawesome/free-solid-svg-icons';

/* add icons to the library */
library.add(faSearch, faSort, faSortAsc, faSortDesc, faFilter, faFolderTree, faXmark);

const app = createApp(App);
app.component('font-awesome-icon', FontAwesomeIcon);
app.use(router);
app.use(BootstrapVue3);
app.mount('#app');
