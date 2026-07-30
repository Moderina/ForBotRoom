<script setup lang="ts">
import { useUserStore } from "@/stores/userStore";
import {getFileUrl} from "@/utils/fileUrl.ts";

const userStore = useUserStore();
</script>

<template>
  <header class="header">
    <div class="app-name">
      <h1>ForBotRoom</h1>
    </div>

    <div class="user" v-if="userStore.isLoaded">
      <div class="avatar-wrap">
        <img
            :src="getFileUrl(userStore.user?.profilePictureUrl)"
            alt="Profile"
            class="avatar"
        />
        <span class="status-dot"></span>
      </div>
      <span class="username">{{ userStore.user?.name }}</span>
    </div>
    <span class="username loading" v-else>Loading…</span>

  </header>
</template>

<style scoped>
.header {
  height: 56px;
  flex-shrink: 0;
  display: flex;

  align-items: center;
  justify-content: space-between;

  padding: 0 16px;

  background: var(--bg-app);
  border-bottom: 1px solid #000;
}

.app-name {
  display: flex;
  align-items: center;
  gap: 10px;
}

.app-name h1 {
  margin: 0;
  font-size: 1rem;
  font-weight: 600;
}

.user {
  display: flex;
  align-items: center;
  gap: 10px;
  padding: 4px 12px 4px 4px;
  border-radius: 20px;
  background: var(--bg-elevated);
}

.avatar-wrap {
  position: relative;
  width: 32px;
  height: 32px;
}

.avatar {
  width: 32px;
  height: 32px;
  border-radius: 50%;
  object-fit: cover;
  background: var(--bg-elevated-hover);
}

.status-dot {
  position: absolute;
  bottom: -1px;
  right: -1px;
  width: 10px;
  height: 10px;
  border-radius: 50%;
  background: var(--online);
  border: 2px solid var(--bg-elevated);
}

.username {
  font-size: 0.85rem;
  font-weight: 500;
}

.username.loading {
  color: var(--text-muted);
}

</style>