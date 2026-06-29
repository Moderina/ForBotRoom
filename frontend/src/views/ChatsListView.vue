<script setup lang="ts">
import { ref, onMounted } from "vue"
import { useChatStore } from "@/stores/chatStore";
import type {Chat} from "@/types/Chat.ts";
import {useBotStore} from "@/stores/botStore.ts";
import CreateChatModal from "@/components/CreateChatModal.vue";

const showCreateModal = ref(false);
const chatStore = useChatStore();
const botStore = useBotStore();

async function selectChat(chat: Chat) {
  chatStore.openChat(chat);
}

async function createNewChat(name: string, botId: string) {
  console.log("new chat name:", name);
  let newChat = await chatStore.addChat(name, botId);
  showCreateModal.value = false;
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
    <button @click="showCreateModal = true">Add Chat</button>
  </ul>

  <CreateChatModal
      v-if="showCreateModal"
      :bots="botStore.bots"
      @close="showCreateModal = false"
      @create="createNewChat"
  />
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