import { UserListItem } from "../shared/models/user-list-item";

export interface UserState {
    [id: string]: UserListItem;
}

