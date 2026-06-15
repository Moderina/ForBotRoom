<script setup lang="ts">
import {nextTick, ref, watch} from "vue"
import {useChatStore} from "@/stores/chatStore.ts";

const chatStore = useChatStore()

const chatName = ref("")
const messagesDiv = ref<HTMLDivElement | null>(null)
// const { messages } = storeToRefs(chatStore)

</script>

<template>
  <div>Chat VIew</div>
  <div class="chat-view">
    <div class="chat-title">{{ chatName ? 'Chatting with ' + chatName : 'Select a chat' }}</div>

    <div class="chat">
      <div class="messages" ref="messagesDiv">
<!--        <div-->
<!--            v-for="(msg, index) in messages"-->
<!--            :key="index"-->
<!--            :class="['msg', msg.authorId == chatStore.userId ? 'user' : 'agent']"-->
<!--        >-->
<!--          {{ msg.content }}-->
<!--        </div>-->
      </div>

<!--      <input v-model="input" placeholder="Napisz wiadomość..." @keydown.enter="sendMessage" />-->
    </div>
  </div>
</template>

<style scoped>

</style>