export function getFileUrl(filename: string | null | undefined): string {
    if (!filename) {
        return `${import.meta.env.VITE_API_URL}/assets/images/default-profile.jpg`;
    }
    return `${import.meta.env.VITE_API_URL}${filename}`;
}