<script setup lang="ts">
import { ref, onMounted } from "vue"

// const chatStore = useChatStore()
const userId = ref(0);

</script>

<template>
  <div class="sidebar">
    <div class="list-name">Contact List</div>
    <ul class="chats-list">
<!--      <li v-for="chat in chatStore.chats" :key="chat.id" @click="selectChat(chat)">-->
<!--        <div>{{ chat.name }}</div>-->
<!--      </li>-->
      <div>ChatList</div>
    </ul>
  </div>
</template>

<style scoped>

</style>