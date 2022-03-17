import { createAction, props } from "@ngrx/store";
import { User } from "../shared/models/user";

const userListPrefix: string = '[User List]';

export const loadUserList = createAction(userListPrefix + ' Load users');
export const loadUserListSuccess = createAction(userListPrefix + ' Load users success', props<{ users: User[] }>());
export const loadUserListError = createAction(userListPrefix + ' Load user error');


const userDetailsPrefix: string = '[User details]';

export const loadUserDetails = createAction(userDetailsPrefix + ' Load user');
export const loadUserDetailsSuccess = createAction(userDetailsPrefix + ' Load user success', props<{ user: User[] }>());
export const loadUserDetailsError = createAction(userDetailsPrefix + ' Load user error');
