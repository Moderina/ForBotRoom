export interface Message {
    id: string
    chatId: string
    authorId: string
    class: "user" | "bot"
    content: string
    sent : boolean
}