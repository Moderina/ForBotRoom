<script setup lang="ts">
import ChatsList from "@/views/ChatsListView.vue";
import Chat from "@/views/ChatView.vue";

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