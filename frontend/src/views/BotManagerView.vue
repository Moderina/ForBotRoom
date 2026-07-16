<script setup lang="ts">
import {computed, onMounted, ref} from "vue"
import {type Bot, type BotForm, createEmptyBotForm} from "@/types/Bot.ts";
import {useBotStore} from "@/stores/botStore.ts";
import {getFileUrl} from "@/utils/fileUrl.ts";

const botStore = useBotStore()

const form = ref<BotForm>({ ...createEmptyBotForm() })
const isEditing = computed(() => !!botStore.selectedBot)
const activeInput = ref<string>("Name")

const avatarFile = ref<File | null>(null);
const avatarPreview = ref<string | null>(null);

async function saveBot() {
  console.log(isEditing.value)
  console.log(form.value)
  const method = isEditing.value ? "PUT" : "POST"
  
  await botStore.addBot(form.value, avatarFile.value ?? null)
}

async function deleteBot() {
  if (!botStore.selectedBot) return

  // await fetch(`/agents/${id}`, { method: "DELETE" })
  // agentStore.removeAgent(id)
  // agentStore.selectAgent(null)
}

function newBot() {
  botStore.selectedBot = null;
  form.value = createEmptyBotForm();

  avatarFile.value = null;
  avatarPreview.value = null;
}

async function editBot(bot: Bot) {
  
  if(!bot.personalityProfile) {
    await botStore.loadBotDetails(bot);
  }
  if (botStore.selectedBot && botStore.selectedBot.personalityProfile) {
    form.value = {
      name: botStore.selectedBot.name,
      personalityProfile: botStore.selectedBot.personalityProfile
    };

    avatarPreview.value = botStore.selectedBot.profilePictureUrl
        ? getFileUrl(botStore.selectedBot.profilePictureUrl)
        : null;

    avatarFile.value = null;
  }
}

function changeActiveInput(inputname: string) {
  activeInput.value = inputname
}

function onAvatarSelected(event: Event) {
  const input = event.target as HTMLInputElement;

  if (!input.files?.length)
    return;

  avatarFile.value = input.files[0];
  avatarPreview.value = URL.createObjectURL(input.files[0]);
}


onMounted(() => {
  botStore.loadBots();
});
</script>

<template>
  <div class="bot-manager flex-1 border-l-4">
    <div class="bot-list">
      <h2>Agents</h2>

      <button @click="newBot">+ New Agent</button>

      <ul>
        <li
            class="m-2 p-1 border-2 bg-gray-900 border-gray-600 rounded-md"
            v-for="bot in botStore.bots"
            :key="bot.id ? bot.id : undefined"
            @click="editBot(bot)"
        >
          {{ bot.name }}
        </li>
      </ul>
    </div>

    <div class="bot-form">
      <div class="flex justify-between">
        <h2>{{ isEditing ? "Edit Bot" : "Create Bot" }}</h2>
        <h2>{{ activeInput }}</h2>
      </div>

      <div class="avatar-picker">
        <img
            :src="avatarPreview ?? '/default-avatar.png'"
            class="avatar-preview"
            alt="Bot avatar"
        />

        <input
            type="file"
            accept="image/*"
            @change="onAvatarSelected"
        />
      </div>

      <input v-model="form.name" placeholder="Name" @focusin="changeActiveInput('Name')" />

      <textarea v-model="form.personalityProfile.coreIdentity" placeholder="Core Identity" @focusin="changeActiveInput('Core Identity')" />

      <textarea v-model="form.personalityProfile.personality" placeholder="Personality" @focusin="changeActiveInput('Personality')" />

      <textarea v-model="form.personalityProfile.textingStyle" placeholder="textingStyle" @focusin="changeActiveInput('TextingStyle')" />

      <textarea v-model="form.personalityProfile.interests" placeholder="interests" @focusin="changeActiveInput('Interests')" />

      <textarea v-model="form.personalityProfile.likes" placeholder="likes" @focusin="changeActiveInput('Likes')" />

      <textarea v-model="form.personalityProfile.dislikes" placeholder="dislikes" @focusin="changeActiveInput('Dislikes')" />

      <!--      <input-->
      <!--          type="number"-->
      <!--          step="0.1"-->
      <!--          min="0"-->
      <!--          max="2"-->
      <!--          v-model.number="form.temperature"-->
      <!--      />-->

      <div class="buttons">
        <button @click="saveBot">Save
          {{ isEditing ? "Update" : "Create" }}
        </button>

        <button
            v-if="isEditing"
            @click="deleteBot()"
            class="danger"
        >
          Delete
        </button>
      </div>
    </div>
  </div>
</template>

<style scoped>
.bot-manager {
  display: flex;
  gap: 20px;
  padding: 20px;
  height: 100%;
  width: 100%;
}

.bot-list {
  width: 250px;
  flex: 1;
}

.bot-form {
  flex: 2;
  display: flex;
  flex-direction: column;
  gap: 10px;
}

.avatar-picker {
  display: flex;
  align-items: center;
  gap: 16px;
}

.avatar-preview {
  width: 80px;
  height: 80px;
  border-radius: 50%;
  object-fit: cover;
  background: #222;
  border: 1px solid #444;
}

.bot-form textarea {
  resize: none;
  flex: 0 0 60px;
  transition: flex 0.25s ease, height 0.25s ease;
}

.bot-form textarea:focus {
  flex: 1;
}

.bot-form input,
.bot-form textarea {
  padding: 8px;
  border-radius: 4px;
  border: 1px solid #444;
  background: #222;
  color: #fff;
}

.buttons {
  display: flex;
  gap: 10px;
}

button {
  padding: 6px 12px;
}

.danger {
  background: darkred;
}
</style>