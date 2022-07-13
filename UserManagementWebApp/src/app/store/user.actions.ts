import { createAction, props } from "@ngrx/store";
import { UserDetails } from "../shared/models/user-details";
import { UserImage } from "../shared/models/user-image";
import { UserListItem } from "../shared/models/user-list-item";

export const loadUserList = createAction('[User List] Load users');
export const loadUserListSuccess = createAction('[User List] Load users success', props<{users: UserListItem[]}>());
export const loadUserListError = createAction('[User list] Load user error');

export const loadUserDetails = createAction('[User details] Load details', props<{userId: number}>());
export const loadUserDetailsSuccess = createAction('[User details] Load details success', props<{userDetails: UserDetails}>());
export const loadUserDetailsError = createAction('[User details] Load details error');

// export const loadDefaultImage = createAction('[Default image] Load');
// export const loadDefaultImageSuccess = createAction('[Default image] Load success', props<{defaultImage: UserImage}>());
// export const loadDefaultImageError = createAction('[Default image] Load error');

export const saveNewUser = createAction('[New user] Save');
export const saveNewUserSuccess = createAction('[New user] Save success');
export const saveNewUserError = createAction('[New user] Save error');

// export const saveEditedUser = createAction('[Edit user] Save');
// export const saveEditedUserSuccess = createAction('[Edit user] Save success');
// export const saveEditedUserError = createAction('[Edit user] Save error');