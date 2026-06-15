export interface IPersonality {
    coreIdentity: string
    personality: string
    textingStyle: string
    interests: string
    likes: string
    dislikes: string
}

export interface IBot {
    id: number | null
    name: string
    personalityData: IPersonality
}