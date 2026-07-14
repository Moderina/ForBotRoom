export function getFileUrl(path: string | null | undefined): string {
    if (!path) {
        return `${import.meta.env.VITE_API_URL}/uploads/profile-pics/hampter.jpg`;
    }

    return `${import.meta.env.VITE_API_URL}${path}`;
}