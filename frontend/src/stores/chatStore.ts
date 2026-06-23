import { ref, computed } from 'vue'
import { defineStore } from 'pinia'
import type {Chat} from "@/types/Chat.ts";
import {getChatsList, createChat} from "@/api/apiChats.ts";
import type {Message} from "@/types/Message.ts";
import {useUserStore} from "@/stores/userStore.ts";

export const useChatStore = defineStore('chat', () => {
  const userStore = useUserStore();
  
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

  function sendMessage(content: string) {
    if (!currentChat.value) return

    if (userStore.user == null)
      return;
    const msg :Message = {
      content: content,
      class: "user",
      authorId: userStore.user.id,
      chatId: currentChat.value.id,
    }
    const msgDto = {
      content: content,
      authorId: userStore.user.id,
      chatId: currentChat.value.id
    }
    // sendWs(msgDto)
    addMessage(msg)
  }

  function addMessage(msg: Message) {
    console.log("addMessage", msg)
    messages.value.push(msg)
  }

  return {
    chats,
    currentChat,
    messages,
    loadChats,
    addChat,
    sendMessage,
    
  };
})
