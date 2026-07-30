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
  await chatStore.addChat(name, botId);
  showCreateModal.value = false;
}

async function removeChat(chat: Chat) {
  chatStore.removeChat(chat);
}

onMounted(() => {
  chatStore.loadChats();
});

</script>

<template>
  <div class="chat-list">
    <div class="chat-list-header">
      <span>Direct Messages</span>
      <button class="icon-btn" @click="showCreateModal = true" title="Add chat">+</button>
    </div>
      
    <ul class="chats">
      
      <li v-for="chat in chatStore.chats" :key="chat.id" @click="selectChat(chat)"
          class="chat-item"
      >
        <span class="chat-name">{{ chat.name }}</span>
        <button
            @click.stop="removeChat(chat)"
            class="chat-remove"
        >
          🗑️
        </button>
      </li>
    </ul>
  </div>

  <CreateChatModal
      v-if="showCreateModal"
      :bots="botStore.bots"
      @close="showCreateModal = false"
      @create="createNewChat"
  />
</template>

<style scoped>

.chat-list {
  height: 100%;
  display: flex;
  flex-direction: column;
  padding: 12px 8px;
}


.chat-list-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 4px 8px 12px;
  font-size: 0.7rem;
  font-weight: 700;
  letter-spacing: 0.04em;
  text-transform: uppercase;
  color: var(--text-muted);
}

.icon-btn {
  width: 20px;
  height: 20px;
  border-radius: 6px;
  border: none;
  background: var(--bg-elevated);
  color: var(--text-primary);
  font-size: 0.9rem;
  line-height: 1;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
}

.icon-btn:hover {
  background: var(--accent);
}

.chats {
  list-style: none;
  margin: 0;
  padding: 0;
  overflow-y: auto;
  flex: 1;
}

.chat-item {
  display: flex;
  align-items: center;
  gap: 10px;
  padding: 8px;
  border-radius: 8px;
  cursor: pointer;
  margin-bottom: 2px;
  background: var(--bg-chat);
}

.chat-item:hover {
  background: var(--bg-elevated);
}

.chat-item:hover .chat-remove {
  opacity: 1;
}

.chat-name {
  flex: 1;
  font-size: 0.9rem;
  font-weight: 500;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.chat-remove {
  opacity: 0;
  transition: opacity 0.15s ease;
  background: none;
  border: none;
  color: var(--text-muted);
  cursor: pointer;
  font-size: 0.85rem;
  padding: 2px;
}

.chat-remove:hover {
  color: var(--danger);
}

</style>