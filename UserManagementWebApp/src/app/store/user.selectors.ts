import { createFeatureSelector, createSelector } from "@ngrx/store";
import { UserState } from "./user.state";
import { userFeatureKey } from "./userRoot.state";

export const selectUserFeature = createFeatureSelector<UserState>(userFeatureKey);

export const selectUsers = createSelector(selectUserFeature, (state: UserState) =>
    state.users);
