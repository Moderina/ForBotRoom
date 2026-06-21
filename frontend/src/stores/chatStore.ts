import { ref, computed } from 'vue'
import { defineStore } from 'pinia'
import type {Chat} from "@/types/Chat.ts";
import {getChatsList, createChat} from "@/api/apiChats.ts";
import type {Message} from "@/types/Message.ts";

export const useChatStore = defineStore('chat', () => {
  
  const chats = ref<Chat[]>([]);

  const currentChat = ref<Chat | null>(null)

  const messages = ref<Message[]>([])

  async function loadChats() {
    const response = await getChatsList();

    chats.value = response;
  }

  async function addChat(name: string) {
    const newChat = await createChat({
      name: name,
    });

    chats.value.push(newChat);
    return newChat;
  }

  return {
    chats,
    currentChat,
    messages,
    loadChats,
    addChat,
    
  };
})
