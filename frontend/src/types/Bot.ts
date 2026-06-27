export interface IPersonality {
    coreIdentity: string
    personality: string
    textingStyle: string
    interests: string
    likes: string
    dislikes: string
}

export interface Bot {
    id: string | null
    name: string
    personalityData: IPersonality | null
}

export interface BotForm {
    name: string
    personalityData: IPersonality
}


export function createEmptyBotForm(): BotForm {
    return {
        name: "",
        personalityData: {
            coreIdentity: "",
            personality: "",
            textingStyle: "",
            interests: "",
            likes: "",
            dislikes: "",
        }
    };
}