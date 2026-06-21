export interface Message {
    content: string
    class: "user" | "bot"
    authorId: string
    chatId: string
}