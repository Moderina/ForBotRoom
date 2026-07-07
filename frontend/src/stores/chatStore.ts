import {ref} from 'vue'
import {defineStore} from 'pinia'
import type {Chat} from "@/types/Chat.ts";
import {createChat, disableChat, getChatsList} from "@/api/chatsApi.ts";
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

  async function addChat(name: string, botId: string) {
    const newChat = await createChat({
      name: name,
      botId
    });

    chats.value.push(newChat);
    currentChat.value = newChat;
    return newChat;
  }
  
  async function removeChat(chat : Chat) {
    await disableChat(chat.id);
    chats.value.splice(chats.value.indexOf(chat), 1);
    if (currentChat.value?.id === chat.id) {
      currentChat.value = null;
      messages.value = [];
    }
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
    
    //TODO: temporary till signalR not implemeneted
    const msgSent: Message = {
      id: "temp",
      chatId: currentChat.value.id,
      content: content,
      authorId: userStore.user.id,
      class: "user",
      sent: false
    }
    addMessage(msgSent)
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
    removeChat,
    sendMessage,
    
  };
})
