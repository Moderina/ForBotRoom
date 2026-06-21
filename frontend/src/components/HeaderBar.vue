<script setup lang="ts">
import { useUserStore } from "@/stores/userStore";

const userStore = useUserStore();
</script>

<template>
  <header class="header">
    <div class="left">
      <h1>Messenger</h1>
    </div>

    <div class="right">
      <span class="username" v-if="userStore.isLoaded">
        {{ userStore.user?.name }}
      </span>
      <span class="username" v-else>
        Loading...
      </span>
    </div>
  </header>
</template>

<style scoped>
.header {
  height: 64px;

  display: flex;
  align-items: center;
  justify-content: space-between;

  padding: 0 1rem;

  border-bottom: 1px solid #ddd;
  background: white;
}

.left h1 {
  margin: 0;
  font-size: 1.25rem;
}

.username {
  font-size: 0.9rem;
  color: #666;
}
</style>