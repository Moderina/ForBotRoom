<script setup lang="ts">
import {computed, onMounted, ref} from "vue"
import {type Bot, type BotForm, createEmptyBotForm} from "@/types/Bot.ts";
import {useBotStore} from "@/stores/botStore.ts";

const agentStore = ref([] as Bot[])
const botStore = useBotStore()

const selectedAgentId = ref<number | null>(null);

const form = ref<BotForm>({ ...createEmptyBotForm() })

const isEditing = computed(() => !!botStore.selectedBot)
const activeInput = ref<string>("Name")

async function saveBot() {
  console.log(isEditing.value)
  console.log(form.value)
  const method = isEditing.value ? "PUT" : "POST"
  
  await botStore.addBot(form.value)
}

async function deleteBot() {
  if (!botStore.selectedBot) return

  // await fetch(`/agents/${id}`, { method: "DELETE" })
  // agentStore.removeAgent(id)
  // agentStore.selectAgent(null)
}

function newAgent() {
  botStore.selectedBot = null;
  form.value = createEmptyBotForm();
}

async function editBot(bot: Bot) {
  
  if(!bot.personalityData) {
    await botStore.loadBotDetails(bot);
  }
  if (botStore.selectedBot && botStore.selectedBot.personalityData) {
    form.value = {
      name: botStore.selectedBot.name,
      personalityData: botStore.selectedBot.personalityData
    };
  }
}

function changeActiveInput(inputname: string) {
  activeInput.value = inputname
}

;
onMounted(() => {
  botStore.loadBots();
});
</script>

<template>
  <div class="agent-manager flex-1 border-l-4">
    <div class="agent-list">
      <h2>Agents</h2>

      <button @click="newAgent">+ New Agent</button>

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

    <div class="agent-form">
      <div class="flex justify-between">
        <h2>{{ isEditing ? "Edit Agent" : "Create Agent" }}</h2>
        <h2>{{activeInput}}</h2>

      </div>

      <input v-model="form.name" placeholder="Name" @focusin="changeActiveInput('Name')" />

      <textarea v-model="form.personalityData.coreIdentity" placeholder="Core Identity" @focusin="changeActiveInput('Core Identity')" />

      <textarea v-model="form.personalityData.personality" placeholder="Personality" @focusin="changeActiveInput('Personality')" />

      <textarea v-model="form.personalityData.textingStyle" placeholder="textingStyle" @focusin="changeActiveInput('TextingStyle')" />

      <textarea v-model="form.personalityData.interests" placeholder="interests" @focusin="changeActiveInput('Interests')" />

      <textarea v-model="form.personalityData.likes" placeholder="likes" @focusin="changeActiveInput('Likes')" />

      <textarea v-model="form.personalityData.dislikes" placeholder="dislikes" @focusin="changeActiveInput('Dislikes')" />

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
.agent-manager {
  display: flex;
  gap: 20px;
  padding: 20px;
  height: 100%;
  width: 100%;
}

.agent-list {
  width: 250px;
  flex: 1;
}

.agent-form {
  flex: 2;
  display: flex;
  flex-direction: column;
  gap: 10px;
}

.agent-form textarea {
  resize: none;
  flex: 0 0 60px; /* default small */
  transition: flex 0.25s ease, height 0.25s ease;

}

.agent-form textarea:focus {
  flex: 1;
}

.agent-form input,
.agent-form textarea {
  padding: 8px;
  border-radius: 4px;
  border: 1px solid #444;
  background: #222;
  color: #fff;
}

button {
  padding: 6px 12px;
}

.danger {
  background: darkred;
}
</style>