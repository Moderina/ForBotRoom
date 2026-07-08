<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import {useUserStore} from "@/stores/userStore.ts";
import {onMounted} from "vue";
import MainLayout from "@/layouts/MainLayout.vue";
import {startSignalR} from "@/services/WebSocketService.ts";
import {useChatStore} from "@/stores/chatStore.ts";

const userStore = useUserStore();
const chatStore = useChatStore();

onMounted(async () => {
  await userStore.loadUser();
  if (userStore.user) {
    console.log("users id:" + userStore.user.id);
    await startSignalR(userStore.user.id);
    chatStore.initSignalR();
  }
});
</script>

<template>
    <MainLayout/>
<!--    <UserProfile />-->

</template>

<style scoped>


</style>
