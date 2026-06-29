import {api} from "@/api/http.ts";
import type {Chat} from "@/types/Chat.ts";

export interface CreateChatRequest {
    name: string;
    botId: string;
}

export function getChatsList(): Promise<Chat[]> {
    return api<Chat[]>("/api/chats/getAll");
}

export function createChat(request : CreateChatRequest): Promise<Chat> {
    return api<Chat>("/api/chats/new", {
        method: "POST",
        body: JSON.stringify(request),
    });
}