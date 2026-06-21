import {api} from "@/api/http.ts";
import type {Chat} from "@/types/Chat.ts";

export interface CreateChatRequest {
    name: string;
}

export function getChatsList(): Promise<Chat[]> {
    return api<Chat[]>("/api/chat/getAll");
}

export function createChat(request : CreateChatRequest): Promise<Chat> {
    return api<Chat>("/api/chat/new", {
        method: "POST",
        body: JSON.stringify(request),
    });
}