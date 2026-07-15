const API_URL = import.meta.env.VITE_API_URL;

export async function api<T>(
    path: string,
    options?: RequestInit,
    fileUpload: Boolean = false
): Promise<T> {
    const token = localStorage.getItem("token");

    const headers = new Headers(options?.headers);

    headers.set(
        "Authorization",
        `Bearer ${token}`
    );

    if (!fileUpload) {
        headers.set(
            "Content-Type",
            "application/json"
        );
    } else {
        headers.delete("Content-Type");
    }
    
    const response = await fetch(`${API_URL}${path}`, {
        ...options,
        headers,
    });

    if (!response.ok) {
        throw new Error(`API Error: ${response.status}`);
    }

    const text = await response.text();

    return (text ? JSON.parse(text) : undefined) as T;
}