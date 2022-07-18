import { createAction, props } from "@ngrx/store";
import { FormGroupState } from "ngrx-forms";
import { UserDetails } from "../shared/models/user-details";
import { UserForm } from "../shared/models/user-form";
import { UserListItem } from "../shared/models/user-list-item";

export const loadUserList = createAction('[User List] Load users');
export const loadUserListSuccess = createAction('[User List] Load users success', props<{ users: UserListItem[] }>());
export const loadUserListError = createAction('[User list] Load user error');

export const loadUserDetails = createAction('[User details] Load details', props<{ userId: number }>());
export const loadUserDetailsSuccess = createAction('[User details] Load details success', props<{ userDetails: UserDetails }>());
export const loadUserDetailsError = createAction('[User details] Load details error');

// export const loadDefaultImage = createAction('[Default image] Load');
// export const loadDefaultImageSuccess = createAction('[Default image] Load success', props<{defaultImage: UserImage}>());
// export const loadDefaultImageError = createAction('[Default image] Load error');

export const saveNewUser = createAction('[New user] Save');
export const saveNewUserSuccess = createAction('[New user] Save success', props<{ userId: number }>());
export const saveNewUserError = createAction('[New user] Save error');


const editUser = '[Edit user] ';

export const loadUserForEdit = createAction(editUser + 'Load details', props<{ userId: number }>());
export const loadUserForEditSuccess = createAction(editUser + 'Load details success',
    props<{ userDetails: UserDetails, userForm: FormGroupState<UserForm> }>());
export const loadUserForEditError = createAction(editUser + 'Load details error');

export const saveEditedUser = createAction(editUser + 'Save');
export const saveEditedUserSuccess = createAction(editUser + 'Save success');
export const saveEditedUserError = createAction(editUser + 'Save error');

const formImage = '[Form image] ';
export const changeFormImage = createAction(formImage + 'Change', props<{ formImage: File }>());
export const uploadFormImageSkipped = createAction(formImage + 'Skipped');
export const uploadFormImage = createAction(formImage + 'Upload');
export const deleteFormImage = createAction(formImage + 'Delete');
export const changeFormImageSuccess = createAction(formImage + 'Upload success');
export const changeFormImageError = createAction(formImage + 'Upload error');