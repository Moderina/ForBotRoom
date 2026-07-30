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
  
  if (isEditing.value) {
    await botStore.editBot(form.value, avatarFile.value ?? null)
  }
  else {
    await botStore.addBot(form.value, avatarFile.value ?? null)
  }
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

    console.log(botStore.selectedBot.profilePictureUrl);
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
  <div class="bot-manager">
    <div class="bot-list">
      <div class="panel-header">
        <span>Characters</span>
        <button class="icon-btn" @click="newBot" title="New agent">+</button>
      </div>

      <ul>
        <li
            class="bot-item"
            v-for="bot in botStore.bots"
            :key="bot.id ? bot.id : undefined"
            @click="editBot(bot)"
        >
          <span class="bot-name">{{ bot.name }}</span>
        </li>
      </ul>
    </div>

    <div class="bot-form">
      <div class="form-header">
        <h2>{{ isEditing ? "Edit Bot" : "Create Bot" }}</h2>
        <span class="active-field" v-if="activeInput">{{ activeInput }}</span>
      </div>

      <div class="avatar-picker">
        <img
            :src="avatarPreview ?? '/default-avatar.png'"
            class="avatar-preview"
            alt="Character avatar"
        />

        <label class="file-btn">
          Change avatar
          <input type="file" accept="image/*" @change="onAvatarSelected" hidden />
        </label>

      </div>


      <input v-model="form.name" placeholder="Character name" @focusin="changeActiveInput('Name')" />

      
      <textarea v-model="form.personalityProfile.coreIdentity" placeholder="Core Identity" @focusin="changeActiveInput('Core Identity')" />

      <textarea v-model="form.personalityProfile.personality" placeholder="Personality" @focusin="changeActiveInput('Personality')" />

      <textarea v-model="form.personalityProfile.textingStyle" placeholder="Texting Style" @focusin="changeActiveInput('TextingStyle')" />

      <textarea v-model="form.personalityProfile.interests" placeholder="Interests" @focusin="changeActiveInput('Interests')" />

      <textarea v-model="form.personalityProfile.likes" placeholder="Likes" @focusin="changeActiveInput('Likes')" />

      <textarea v-model="form.personalityProfile.dislikes" placeholder="Dislikes" @focusin="changeActiveInput('Dislikes')" />

      <!--      <input-->
      <!--          type="number"-->
      <!--          step="0.1"-->
      <!--          min="0"-->
      <!--          max="2"-->
      <!--          v-model.number="form.temperature"-->
      <!--      />-->

      <div class="buttons">
        <button class="primary" @click="saveBot">{{ isEditing ? "Update" : "Create" }}</button>
        <button v-if="isEditing" class="danger" @click="deleteBot()">Delete</button>
      </div>

    </div>
  </div>
</template>

<style scoped>
.bot-manager {
  display: flex;

  height: 100%;
  width: 100%;
}

.bot-list {
  width: 200px;
  flex-shrink: 0;
  display: flex;
  flex-direction: column;
  border-right: 1px solid var(--bg-app);
  padding: 12px 8px;
}

.panel-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 4px 8px 12px;
  font-size: 0.7rem;
  font-weight: 700;
  letter-spacing: 0.04em;
  text-transform: uppercase;
  color: var(--text-muted);
}

.icon-btn {
  width: 20px;
  height: 20px;
  border-radius: 6px;
  border: none;
  background: var(--bg-elevated);
  color: var(--text-primary);
  cursor: pointer;
}

.icon-btn:hover {
  background: var(--accent);
}

.bot-list ul {
  list-style: none;
  margin: 0;
  padding: 0;
  overflow-y: auto;
}

.bot-item {
  display: flex;
  align-items: center;
  gap: 10px;
  padding: 8px;
  border-radius: 8px;
  cursor: pointer;
  margin-bottom: 2px;
}

.bot-item:hover {
  background: var(--bg-elevated);
}

.bot-name {
  font-size: 0.88rem;
  font-weight: 500;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}


.bot-form {
  flex: 1;
  display: flex;
  flex-direction: column;
  gap: 14px;
  padding: 20px 24px;
  overflow-y: auto;
}

.form-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.form-header h2 {
  margin: 0;
  font-size: 1.1rem;
  font-weight: 700;
}

.active-field {
  font-size: 0.7rem;
  font-weight: 600;
  letter-spacing: 0.03em;
  text-transform: uppercase;
  color: var(--color-text);
  background: rgba(88, 101, 242, 0.15);
  padding: 3px 8px;
  border-radius: 6px;
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
  background: var(--bg-elevated);
  border: 1px solid #444;
}

.file-btn {
  font-size: 0.82rem;
  font-weight: 600;
  color: #fff;
  background: var(--bg-elevated);
  padding: 8px 14px;
  border-radius: 6px;
  cursor: pointer;
}

.file-btn:hover {
  background: var(--bg-elevated-hover);
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

.field input:focus,
.field textarea:focus {
  outline: none;
  border-color: var(--accent);
}

.field textarea:focus {
  min-height: 88px;
}


.buttons {
  display: flex;
  gap: 10px;
  margin-top: 8px;
}

.buttons button {
  padding: 10px 18px;
  border-radius: 8px;
  border: none;
  font-weight: 600;
  font-size: 0.85rem;
  cursor: pointer;
}

.primary {
  background: var(--accent);
  color: #fff;
}

.primary:hover {
  background: var(--accent-hover);
}

.danger {
  background: transparent;
  color: var(--danger);
  border: 1px solid var(--danger);
}

.danger:hover {
  background: var(--danger);
  color: #fff;
}

</style>