<script setup lang="ts">
import HeaderBar from "@/components/HeaderBar.vue";
import ChatListView from "@/views/ChatsListView.vue";
import ChatView from "@/views/ChatView.vue";
import BotManagerView from "@/views/BotManagerView.vue";
</script>

<template>
  <div class="layout">
    <HeaderBar />

    <main class="content">
      <aside class="sidebar">
        <ChatListView />
      </aside>

      <section class="chat">
        <ChatView />
      </section>
      <section class="botManager">
        <BotManagerView />
      </section>
    </main>
  </div>
</template>

<style scoped>
.layout {
    display: flex;
    height: 100vh;
    justify-content: center;
    background: #111;
    color: #eee;
  flex-direction: column;
}

.content {
  flex: 1;
  display: flex;
  overflow: hidden;
}

.sidebar {
  width: 30%;
  border-right: 1px solid #ddd;
  border: #0052ff solid 5px;
  border-radius: 5px;
}

.chat {
  flex: 1;
}

.botManager {
  flex: 2;
}
</style>