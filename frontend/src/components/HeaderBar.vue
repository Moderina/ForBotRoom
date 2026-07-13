<script setup lang="ts">
import { useUserStore } from "@/stores/userStore";
import {getFileUrl} from "@/utils/fileUrl.ts";

const userStore = useUserStore();
</script>

<template>
  <header class="header">
    <div class="left">
      <h1>Messenger</h1>
    </div>

    <div class="right">
      <div v-if="userStore.isLoaded" class="flex justify-center items-center">
        <span class="username pr-4">
          {{ userStore.user?.name }}
        </span>
        <img
            :src="getFileUrl(userStore.user?.profilePictureUrl)"
            alt="Profile"
            class="w-20 h-20 rounded-full object-cover bg-amber-50"
        />
      </div>
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
  align-content: center;
}
</style>