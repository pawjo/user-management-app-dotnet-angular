import { createAction, props } from "@ngrx/store";
import { UserListItem } from "../shared/models/user-list-item";

export const loadUserList = createAction('[User List] Load users');
export const loadUserListSuccess = createAction('[User List] Load users success', props<{users: UserListItem[]}>());
export const loadUserListError = createAction('[User list] Load user error');