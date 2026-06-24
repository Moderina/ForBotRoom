import { defineStore } from "pinia";
import {computed, ref} from "vue";
import { getCurrentUser } from "@/api/UsersApi.ts";
import type { User } from "@/types/User";

export const useUserStore = defineStore("user", () => {
    const user = ref<User | null>(null);

    const isLoaded = computed(() => user.value !== null);
    
    async function loadUser() {
        if(user.value === null) {
            user.value = await getCurrentUser();
            localStorage.setItem("token", user.value.id);
        }
    }

    return {
        user,
        isLoaded,
        loadUser,
    };
});