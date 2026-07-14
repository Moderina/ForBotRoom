import type {Message} from "@/types/Message.ts";

export interface Chat {
    id: string,
    name: string,
    members: ChatMember[],
    messages: Message[]
}

export interface ChatMember {
    userId: string
    name: string
    profilePictureUrl: string | null
    type: "user" | "bot";
}