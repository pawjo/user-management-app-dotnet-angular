import { createAction, props } from "@ngrx/store";
import { User } from "../shared/models/user";

export const loadUserList = createAction('[User List] Load users');
export const loadUserListSuccess = createAction('[User List] Load users success', props<{users: User[]}>());
export const loadUserListError = createAction('[User list] Load user error');