<script setup lang="ts">
import HeaderBar from "@/components/HeaderBar.vue";
import ChatListView from "@/views/ChatsListView.vue";
import ChatView from "@/views/ChatView.vue";
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
  height: 100vh;
  display: flex;
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
</style>