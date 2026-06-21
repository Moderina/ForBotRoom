<script setup lang="ts">
import { ref, onMounted } from "vue"
import { useChatStore } from "@/stores/chatStore";
import type {Chat} from "@/types/Chat.ts";

const chatStore = useChatStore();

async function selectChat(chat: Chat) {
  chatStore.currentChat = chat
}

async function createNewChat() {
  let newChat = await chatStore.addChat("chat name");
  // chatStore.currentChat = chat
}


onMounted(() => {
  chatStore.loadChats();
});

</script>

<template>
  <div class="list-name">Contact List</div>
  <ul class="chats-list">
    <li v-for="chat in chatStore.chats" :key="chat.id" @click="selectChat(chat)">
      <div>{{ chat.name }}</div>
    </li>
    <button @click="createNewChat()">Add Chat</button>
  </ul>
</template>

<style scoped>

.list-name {
  padding: 10px;
  text-align: center;
  border-bottom: #0052ff solid 5px;
  font-family: fantasy;
}

.chats-list {
  padding: 5px;
  margin: 0;
}

.chats-list li {
  background: rgba(102, 153, 255, 0.1);
  list-style: none;
  padding: 20px 15px;
  margin: 5px;
  border-radius: 10px;
}
</style>