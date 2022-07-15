import { createFeatureSelector, createSelector } from "@ngrx/store";
import { UserState } from "./user.state";
import { userFeatureKey } from "./userRoot.state";

export const selectUserFeature = createFeatureSelector<UserState>(userFeatureKey);

export const selectUsers = createSelector(selectUserFeature, (state: UserState) =>
    state.users);

export const selectUserDetails = createSelector(selectUserFeature, (state: UserState) =>
    state.userDetails);

export const selectUserForm = createSelector(selectUserFeature, (state: UserState) =>
    state.userForm);

export const selectUserId = createSelector(selectUserFeature, (state: UserState) =>
    state.userId);

export const selectEditUserFormWithId = createSelector(selectUserId, selectUserForm,
    (userId, userForm) => ({
        userId, userForm
    }));

// export const selectDefaultImage = createSelector(selectUserFeature, (state: UserState) =>
//     state.defaultUserImage);