export interface IMessage {
    content: string
    class: "user" | "agent"
    authorId: number
    chatId: number
}