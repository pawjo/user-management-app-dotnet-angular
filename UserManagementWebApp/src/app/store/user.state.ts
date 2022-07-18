import { FormGroupState } from "ngrx-forms";
import { UserDetails } from "../shared/models/user-details";
import { UserForm } from "../shared/models/user-form";
import { UserListItem } from "../shared/models/user-list-item";

export interface UserState {
    users: UserListItem[],
    userDetails: UserDetails,
    userId: number,
    userForm: FormGroupState<UserForm>
    formImage: File
}

