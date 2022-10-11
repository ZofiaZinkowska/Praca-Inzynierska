import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import ListView from '../views/ListView.vue'
import EditView from '../views/EditView.vue'
import PlantProtectionView from '../views/PlantProtectionView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/List',
      name: 'List',
      component: ListView
    },
    {
      path: '/Edit/:id',
      name: 'Edit',
      component: EditView
    },
    {
      path: '/PlantProtection',
      name: 'PlantProtection',
      component: PlantProtectionView
    },
  ]
})

export default router
