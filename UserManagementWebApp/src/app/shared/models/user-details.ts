import { UserImage } from "./user-image";

export interface UserDetails {
    id: number,
    email: string,
    name: string,
    surname: string,
    age: number,
    isDefaultImage: boolean,
    image: UserImage
}
