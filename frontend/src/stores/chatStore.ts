import {ref} from 'vue'
import {defineStore} from 'pinia'
import type {Chat} from "@/types/Chat.ts";
import {createChat, disableChat, getChatDetails, getChatsList} from "@/api/chatsApi.ts";
import type {Message} from "@/types/Message.ts";
import {useUserStore} from "@/stores/userStore.ts";
import * as MessageApi from "@/api/MessageApi.ts";
import {onReceiveMessage} from "@/services/WebSocketService.ts";

export const useChatStore = defineStore('chat', () => {
  const userStore = useUserStore();
  
  const chats = ref<Chat[]>([]);
  const currentChat = ref<Chat | null>(null)
  const messages = ref<Message[]>([])

  function initSignalR() {
    onReceiveMessage((message) => {
      addMessage(message);
    });
  }

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

    currentChat.value = await getChatDetails(chat.id);
    messages.value = currentChat.value.messages;
    
  }

  async function sendMessage(content: string) {
    if (!currentChat.value) return;
    if (userStore.user == null) return;
    
    const msgDto: MessageApi.SendMessageRequest = {
      content: content
    }
    const msg = await MessageApi.createMessage(currentChat.value.id, msgDto);
    addMessage(msg)
  }

  function addMessage(msg: Message) {
    console.log(msg)
    messages.value.push(msg)
  }

  function getMember(authorId: string) {
    return currentChat.value?.members.find(m => m.userId === authorId);
  }

  return {
    chats,
    currentChat,
    messages,
    initSignalR,
    loadChats,
    openChat,
    addChat,
    removeChat,
    sendMessage,
    getMember,
  };
})
