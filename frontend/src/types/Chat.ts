import type {IMessage} from "@/types/Message.ts";

export interface Chat {
    id: number
    name: string
    history: IMessage[]
}