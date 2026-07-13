export function getFileUrl(path: string | null | undefined): string {
    if (!path) {
        return "/default-avatar.png";
    }

    return `${import.meta.env.VITE_API_URL}${path}`;
}