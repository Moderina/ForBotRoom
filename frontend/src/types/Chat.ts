import type {Message} from "@/types/Message.ts";

export interface Chat {
    id: string
    name: string
    history: Message[]
}