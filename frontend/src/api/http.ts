const API_URL = import.meta.env.VITE_API_URL;

export async function api<T>(
    path: string,
    options?: RequestInit
): Promise<T> {
    const token = localStorage.getItem("token");
    
    const response = await fetch(`${API_URL}${path}`, {
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`,
            ...options?.headers,
        },
        ...options,
    });

    if (!response.ok) {
        throw new Error(`API Error: ${response.status}`);
    }

    const text = await response.text();

    return (text ? JSON.parse(text) : undefined) as T;
}