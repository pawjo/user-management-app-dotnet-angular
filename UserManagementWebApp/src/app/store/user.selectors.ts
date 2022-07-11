import { createFeatureSelector, createSelector } from "@ngrx/store";
import { UserState } from "./user.state";
import { userFeatureKey } from "./userRoot.state";

export const selectUserFeature = createFeatureSelector<UserState>(userFeatureKey);

export const selectUsers = createSelector(selectUserFeature, (state: UserState) =>
    state.users);

export const selectUserDetails = createSelector(selectUserFeature, (state: UserState) =>
    state.userDetails);

// export const selectDefaultImage = createSelector(selectUserFeature, (state: UserState) =>
//     state.defaultUserImage);