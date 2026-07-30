<script setup lang="ts">
import {computed, nextTick, ref, watch} from "vue"
import {useChatStore} from "@/stores/chatStore.ts";
import {storeToRefs} from "pinia";
import {useUserStore} from "@/stores/userStore.ts";
import {getFileUrl} from "@/utils/fileUrl.ts";

const chatStore = useChatStore()
const userStore = useUserStore()

const messagesDiv = ref<HTMLDivElement | null>(null)
const { messages } = storeToRefs(chatStore)
const input = ref("")

async function onSendMessage() {
  if (!input.value.trim()) return
  console.log(input.value)

  const text = input.value
  input.value = ""

  await chatStore.sendMessage(text)
}

const chatTitle = computed(() => {
  const chat = chatStore.currentChat;

  if (!chat)
    return "Select a chat";

  return `Chatting with ${chat.name}`;
});

//message received
watch(messages, async () => {
  await nextTick()

  if (messagesDiv.value) {
    messagesDiv.value.scrollTop =
        messagesDiv.value.scrollHeight
  }
})
</script>

<template>
  <div class="chat-view">
<!--    <div class="chat-title">{{ chatName ? 'Chatting with ' + chatName : 'Select a chat' }}</div>-->
    <div class="chat-header">
      <span class="hash">#</span>
      <span class="chat-title">{{ chatTitle }}</span>
    </div>


    <div class="chat">
      <div class="messages" ref="messagesDiv">
        <div
            v-for="(msg, index) in messages"
            :key="msg.id"
            :class="[
              'message-row',
              msg.authorId === userStore.user?.id ? 'user-row' : 'bot-row'
            ]"
        >
          <img
              v-if="msg.authorId !== userStore.user?.id && (index === messages.length - 1 || messages[index + 1]?.authorId !== msg.authorId)"
              :src="getFileUrl(chatStore.getMember(msg.authorId)?.profilePictureUrl)"
              class="avatar"
          />

          <div
              v-else-if="msg.authorId !== userStore.user?.id"
              class="avatar-spacer"
          ></div>

          <div
              :class="['msg', msg.authorId === userStore.user?.id ? 'user' : 'bot']"
          >
            {{ msg.content }}
          </div>

          <img
              v-if="msg.authorId === userStore.user?.id && (index === messages.length - 1 || messages[index + 1]?.authorId !== msg.authorId)"
              :src="getFileUrl(userStore.user.profilePictureUrl)"
              class="avatar"
          />
          <div
              v-else-if="msg.authorId === userStore.user?.id"
              class="avatar-spacer"
          ></div>
        </div>
      </div>

      <input v-model="input" placeholder="Napisz wiadomość..." @keydown.enter="onSendMessage" />
    </div>
  </div>
</template>

<style scoped>
.chat-view {
  height: 100%;
  display: flex;
  flex-direction: column;
}

.chat-header {
  height: 48px;
  flex-shrink: 0;
  display: flex;
  align-items: center;
  gap: 8px;
  padding: 0 16px;
  border-bottom: 1px solid var(--bg-app);
  box-shadow: 0 1px 0 rgba(0, 0, 0, 0.2);
}
.hash {
  color: var(--text-muted);
  font-weight: 700;
  font-size: 1.1rem;
}

.chat-title {
  font-weight: 600;
  font-size: 0.95rem;
}




.chat {
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

.message-row {
  display: flex;
  align-items: flex-end;
  gap: 8px;
  margin: 3px 0;
}

.bot-row {
  justify-content: flex-start;
}

.user-row {
  justify-content: flex-end;
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


.avatar,
.avatar-spacer {
  width: 36px;
  height: 36px;
  flex-shrink: 0;
}

.avatar {
  border-radius: 50%;
  object-fit: cover;
}

.avatar-spacer {
  visibility: hidden;
}

input {
  height: 48px;
  background: #111;
  color: #eee;
  border: none;
  padding: 16px;
  border-radius: 24px;
}
</style>