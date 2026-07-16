export function getFileUrl(path: string | null | undefined): string {
    if (!path) {
        return `${import.meta.env.VITE_API_URL}ProfilePictures/hampter.jpg`;
    }

    return `${import.meta.env.VITE_API_URL}${path}`;
}