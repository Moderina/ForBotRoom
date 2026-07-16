export function getFileUrl(filename: string | null | undefined): string {
    if (!filename) {
        return `${import.meta.env.VITE_API_URL}/api/media/profile-pictures/hampter.jpg`;
    }
    return `${import.meta.env.VITE_API_URL}/api/media/profile-pictures/${filename}`;
}