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
      <aside class="panel sidebar">
        <ChatListView />
      </aside>

      <section class="panel chat">
        <ChatView />
      </section>
      <aside class="panel botManager">
        <BotManagerView />
      </aside>
    </main>
  </div>
</template>

<style scoped>
:global(:root) {
  --bg-app: #1e1f22;
  --bg-sidebar: #2b2d31;
  --bg-chat: #313338;
  --bg-elevated: #383a40;
  --bg-elevated-hover: #3f4147;

  --accent: #5865f2;
  --accent: #5865f2;
  --accent-hover: #4752c4;

  --text-primary: #f2f3f5;
  --text-muted: #949ba4;
  --text-faint: #6d6f78;

  --danger: #da373c;
  --online: #23a55a;
}


.layout {
  display: flex;
  height: 100vh;
  justify-content: center;
  background: var(--bg-app);
  color: var(--text-primary);
  flex-direction: column;
  font-family: 'Inter', -apple-system, 'Segoe UI', Roboto, sans-serif;
}

.content {
  flex: 1;
  display: flex;
  overflow: hidden;
}

.panel {
  height: 100%;
  overflow: hidden;
}


.sidebar {
  width: 20%;
  flex-shrink: 0;
  background: var(--bg-sidebar);
  border-right: 1px solid var(--bg-app);
  border-radius: 5px;
}

.chat {
  flex: 1.4;
  min-width: 0;
  background: var(--bg-chat);

}

.botManager {
  flex: 1;
  min-width: 320px;
  background: var(--bg-sidebar);
  border-left: 1px solid var(--bg-app);

}
</style>