import type {Chat} from "@/types/Chat.ts";
import {api} from "@/api/http.ts";
import type {Message} from "@/types/Message.ts";

export interface SendMessageRequest {
    content: string,
}

export interface MessageHistoryRequest {
    amount: number,
    lastMessageId: number
}
export function createMessage(chatId : string, request : SendMessageRequest): Promise<Message> {
    return api<Message>(`/api/chats/${chatId}/messages`, {
        method: "POST",
        body: JSON.stringify(request),
    });
}
