import {defineStore} from "pinia";
import {ref} from "vue";
import type {Bot, BotForm} from "@/types/Bot.ts";
import {createBot, editBotRequest, getAllBots, getBotDetails} from "@/api/BotApi.ts";
import {createChat} from "@/api/chatsApi.ts";

export const useBotStore = defineStore('bot', () => {
    const botStore = useBotStore();
    
    const bots = ref<Bot[]>([]);
    const selectedBot = ref<Bot | null>(null);

    async function loadBots() {
        bots.value = await getAllBots();
    }

    async function loadBotDetails(bot : Bot) {
        selectedBot.value = bot;
        if (selectedBot.value.id === null) return;
        selectedBot.value = await getBotDetails(selectedBot.value.id);
    }

    async function addBot(form: BotForm, avatar: File | null) {
        const data = new FormData();

        data.append("name", form.name);
        data.append(
            "personalityProfile",
            JSON.stringify(form.personalityProfile)
        );

        if (avatar) {
            data.append("profilePicture", avatar);
        }
        const newBot = await createBot(data);

        bots.value.push(newBot);
        selectedBot.value = newBot;
    }

    async function editBot(form: BotForm, avatar: File | null) {
        if (selectedBot.value === null) return;
        const data = new FormData();

        data.append("name", form.name);
        data.append(
            "personalityProfile",
            JSON.stringify(form.personalityProfile)
        );

        if (avatar) {
            data.append("profilePicture", avatar);
        }
        const newBot = await editBotRequest(selectedBot.value?.id, data);

        bots.value.push(newBot);
        selectedBot.value = newBot;
    }

    return {
        bots,
        selectedBot,
        addBot,
        editBot,
        loadBots,
        loadBotDetails,
    };
})