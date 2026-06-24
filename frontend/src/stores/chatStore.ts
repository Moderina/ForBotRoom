import {ref} from 'vue'
import {defineStore} from 'pinia'
import type {Chat} from "@/types/Chat.ts";
import {createChat, getChatsList} from "@/api/chatsApi.ts";
import type {Message} from "@/types/Message.ts";
import {useUserStore} from "@/stores/userStore.ts";
import * as MessageApi from "@/api/MessageApi.ts";

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
  
  async function openChat(chat: Chat) {
    currentChat.value = chat;
    messages.value = [];

    const params = {};
    messages.value = await MessageApi.getMessagesHistory(currentChat.value.id, params);
  }

  async function sendMessage(content: string) {
    if (!currentChat.value) return;
    if (userStore.user == null) return;
    
    const msgDto: MessageApi.SendMessageRequest = {
      content: content
    }
    const msg = await MessageApi.createMessage(currentChat.value.id, msgDto);
    // sendWs(msgDto)
    addMessage(msg)
  }

  function addMessage(msg: Message) {
    console.log(msg)
    messages.value.push(msg)
  }

  return {
    chats,
    currentChat,
    messages,
    loadChats,
    openChat,
    addChat,
    sendMessage,
    
  };
})
