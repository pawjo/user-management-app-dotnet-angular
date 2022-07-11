export interface UserImage {
    name: string,
    url: string,
    expiresOn: Date,
    isDefault: boolean
}

export const EmptyImage: UserImage = {
    name: '',
    url: '',
    expiresOn: new Date(),
    isDefault: true
}