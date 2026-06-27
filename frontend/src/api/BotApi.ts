import type {Message} from "@/types/Message.ts";
import {api} from "@/api/http.ts";
import type {SendMessageRequest} from "@/api/MessageApi.ts";
import type {Bot, BotForm, IPersonality} from "@/types/Bot.ts";
import type {Chat} from "@/types/Chat.ts";


//TODO: create new class for Bot in here
export function getAllBots(): Promise<Bot[]> {
    return api<Bot[]>(`/api/bots/getAll`);
}

export function getBotDetails(botId : string): Promise<Bot> {
    return api<Bot>(`/api/bots/${botId}/details`);
}

export function createBot(request : BotForm): Promise<Bot> {
    return api<Bot>("/api/bots/create", {
        method: "POST",
        body: JSON.stringify(request),
    });
}