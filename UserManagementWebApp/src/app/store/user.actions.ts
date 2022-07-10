import { createAction, props } from "@ngrx/store";
import { UserDetails } from "../shared/models/user-details";
import { UserListItem } from "../shared/models/user-list-item";

export const loadUserList = createAction('[User List] Load users');
export const loadUserListSuccess = createAction('[User List] Load users success', props<{users: UserListItem[]}>());
export const loadUserListError = createAction('[User list] Load user error');

export const loadUserDetails = createAction('[User details] Load details', props<{userId: number}>());
export const loadUserDetailsSuccess = createAction('[User details] Load details success', props<{userDetails: UserDetails}>());
export const loadUserDetailsError = createAction('[User details] Load details error');