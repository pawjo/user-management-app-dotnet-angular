import { UserDetails } from "../shared/models/user-details";
import { UserImage } from "../shared/models/user-image";
import { UserListItem } from "../shared/models/user-list-item";

export interface UserState {
    users: UserListItem[],
    userDetails: UserDetails
    // defaultUserImage: UserImage
}

