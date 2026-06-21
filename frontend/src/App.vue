<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import UserProfile from "@/components/UserProfile.vue";
import {useUserStore} from "@/stores/userStore.ts";
import {onMounted} from "vue";
import MainLayout from "@/layouts/MainLayout.vue";

const userStore = useUserStore();

onMounted(async () => {
  await userStore.loadUser();
});
</script>

<template>
  <div class="app">
    <MainLayout/>
<!--    <UserProfile />-->
  </div>

</template>

<style scoped>


</style>
