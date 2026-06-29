<script setup lang="ts">
import { ref } from "vue";
import type { Bot } from "@/types/Bot";

defineProps<{
  bots: Bot[];
}>();

const emit = defineEmits<{
  close: [];
  create: [name: string, botId: string];
}>();

const chatName = ref("");
const selectedBot = ref<Bot | null>(null);

function onBotSelected() {
  if (selectedBot.value) {
    chatName.value = selectedBot.value.name;
  }
}

function create() {
  if (!selectedBot.value) return;

  emit("create", chatName.value, selectedBot.value.id);
}
</script>

<template>
  <div class="fixed inset-0 z-50 flex items-center justify-center bg-black/50">
    <div class="w-full max-w-md rounded-xl bg-gray-800 shadow-2xl">

      <!-- Header -->
      <div class="border-b px-6 py-4">
        <h2 class="text-xl font-semibold">
          Create new chat
        </h2>
        <p class="text-sm text-gray-500">
          Choose a bot and give the chat a name.
        </p>
      </div>

      <!-- Body -->
      <div class="space-y-5 p-6">

        <!-- Bot selection -->
        <div>
          <label class="mb-2 block text-sm font-medium">
            Bot
          </label>

          <select
              v-model="selectedBot"
              @change="onBotSelected"
              class="w-full rounded-lg border px-4 py-2 outline-none focus:ring-2"
          >
            <option disabled value="">
              Select a bot
            </option>

            <option
                v-for="bot in bots"
                :key="bot.id"
                :value="bot"
            >
              {{ bot.name }}
            </option>
          </select>
        </div>

        <!-- Chat name -->
        <div>
          <label class="mb-2 block text-sm font-medium">
            Chat name
          </label>

          <input
              v-model="chatName"
              type="text"
              placeholder="My conversation"
              class="w-full rounded-lg border px-4 py-2 outline-none focus:ring-2"
          />
        </div>
      </div>

      <!-- Footer -->
      <div class="flex justify-end gap-3 border-t px-6 py-4">
        <button
            @click="$emit('close')"
            class="rounded-lg border px-4 py-2 transition hover:bg-gray-100"
        >
          Cancel
        </button>

        <button
            @click="create"
            class="rounded-lg bg-blue-600 px-4 py-2 text-white transition hover:bg-blue-700"
        >
          Create chat
        </button>
      </div>

    </div>
  </div>
</template>