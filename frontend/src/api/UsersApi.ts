import type {User} from "@/types/User.ts";
import {api} from "@/api/http.ts";

export function getCurrentUser(): Promise<User> {
    return api<User>("/api/user/me");
}