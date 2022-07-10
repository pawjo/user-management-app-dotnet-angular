import { UserDetails } from "../shared/models/user-details";
import { UserListItem } from "../shared/models/user-list-item";

export interface UserState {
    users: UserListItem[],
    userDetails: UserDetails
}

