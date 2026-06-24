import type {Chat} from "@/types/Chat.ts";
import {api} from "@/api/http.ts";
import type {Message} from "@/types/Message.ts";
import {toQuery} from "@/utils/query.ts";

export interface SendMessageRequest {
    content: string,
}

export interface MessageHistoryRequest {
    amount: number,
    before: number
}
export function createMessage(chatId : string, request : SendMessageRequest): Promise<Message> {
    return api<Message>(`/api/chats/${chatId}/messages`, {
        method: "POST",
        body: JSON.stringify(request),
    });
}

export function getMessagesHistory(chatId : string, params : any): Promise<Message[]> {
    return api<Message[]>(`/api/chats/${chatId}/messages?${toQuery(params)}`, {
        method: "GET",
    });
}