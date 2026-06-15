<script setup lang="ts">
import ChatsList from "@/layouts/ChatsList.vue";
import Chat from "@/layouts/Chat.vue";

import { onMounted, ref } from "vue";
import { getPing } from "../api/apiTypes";

const message = ref("");

onMounted(async () => {
  const response = await getPing();
  message.value = response.message;
});
</script>

<template>
  <ChatsList/>
  <Chat/>
  {{ message }}
</template>

<style scoped>

</style>