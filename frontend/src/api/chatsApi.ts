import {api} from "@/api/http.ts";
import type {Chat} from "@/types/Chat.ts";

export interface CreateChatRequest {
    name: string;
    botId: string;
}

export function getChatsList(): Promise<Chat[]> {
    return api<Chat[]>("/api/chats/");
}

export function getDisabledChats(): Promise<Chat[]> {
    return api<Chat[]>("/api/chats?active=false");
}

export function getChatDetails(chatId: string): Promise<Chat> {
    return api<Chat>(`/api/chats/${chatId}`);
}

export function createChat(request : CreateChatRequest): Promise<Chat> {
    return api<Chat>("/api/chats/new", {
        method: "POST",
        body: JSON.stringify(request),
    });
}

export function disableChat(chatid: string) {
    api(`/api/chats/${chatid}/disable`, {
        method: "POST",
    });
}