import { createFeatureSelector, createSelector } from "@ngrx/store";
import { UserState } from "./user.state";
import { userFeatureKey, UserRootState } from "./userRoot.state";

const selectUserFeature = createFeatureSelector<UserState>(userFeatureKey);

export const selectUsers = createSelector(selectUserFeature, state =>
    Object.keys(state).map(key => state[key]));