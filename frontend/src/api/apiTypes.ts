import { api } from "./http";

export interface PingResponse {
    message: string;
}

export function getPing() {
    return api<PingResponse>("/api/ping");
}