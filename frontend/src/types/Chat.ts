import type {IMessage} from "@/types/Message.ts";

export interface IChat {
    id: number
    name: string
    history: IMessage[]
}