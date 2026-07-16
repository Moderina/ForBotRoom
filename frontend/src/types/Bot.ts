export interface IPersonality {
    coreIdentity: string
    personality: string
    textingStyle: string
    interests: string
    likes: string
    dislikes: string
}

export interface Bot {
    id: string
    name: string
    personalityProfile: IPersonality | null
    profilePictureUrl: string

}

export interface BotForm {
    name: string
    personalityProfile: IPersonality
}


export function createEmptyBotForm(): BotForm {
    return {
        name: "",
        personalityProfile: {
            coreIdentity: "",
            personality: "",
            textingStyle: "",
            interests: "",
            likes: "",
            dislikes: "",
        }
    };
}