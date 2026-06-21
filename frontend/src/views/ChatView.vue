<script setup lang="ts">
import {nextTick, ref, watch} from "vue"
import {useChatStore} from "@/stores/chatStore.ts";
import {storeToRefs} from "pinia";
import {useUserStore} from "@/stores/userStore.ts";

const chatStore = useChatStore()
const userStore = useUserStore()

const chatName = ref("")
const messagesDiv = ref<HTMLDivElement | null>(null)
const { messages } = storeToRefs(chatStore)


watch(() => chatStore.currentChat, async (newchat, oldchat) => {
  console.log("Zmieniono chat! Nowy id:", newchat);

  chatName.value = newchat?.name ?? "";
});
</script>

<template>
  <div>Chat View</div>
  <div class="chat-view">
    <div class="chat-title">{{ chatName ? 'Chatting with ' + chatName : 'Select a chat' }}</div>

    <div class="chat">
      <div class="messages" ref="messagesDiv">
        <div
            v-for="(msg, index) in messages"
            :key="index"
            :class="['msg', msg.authorId == userStore.user?.id ? 'user' : 'bot']"
        >
          {{ msg.content }}
        </div>
      </div>

<!--      <input v-model="input" placeholder="Napisz wiadomość..." @keydown.enter="sendMessage" />-->
    </div>
  </div>
</template>

<style scoped>
.chat-view {
  /*flex: 1;*/
  height: 100%;
  display: flex;
  flex-direction: column;
}

.chat {
  width: 400px;
  background: #1e1e1e;
  border-radius: 8px;
  padding: 10px;

  flex: 1;
  display: flex;
  flex-direction: column;
  min-height: 0;

  /*box-sizing: border-box;*/
}

.messages {
  flex: 1;
  overflow-y: auto;
  padding: 10px;
  display: flex;
  flex-direction: column;
  min-height: 0;
}

.msg {
  margin: 6px 0;
  padding: 6px 8px;
  border-radius: 6px;
  max-width: 80%;
  justify-self: end;
}

.user {
  background: #3a3a3a;
  align-self: flex-end;
}

.bot {
  background: #2a2a2a;
  align-self: flex-start;
}
</style>