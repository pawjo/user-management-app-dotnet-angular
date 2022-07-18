import { createFeatureSelector, createSelector } from "@ngrx/store";
import { UserDetails } from "../shared/models/user-details";
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
    })
);

export const selectFormImage = createSelector(selectUserFeature, (state: UserState) =>
    state.formImage);

export const selectFormImageWithId = createSelector(selectUserId,
    selectFormImage,
    (userId, formImage) => ({
        userId, formImage
    })
);

export const selectEditUserForm = createSelector(selectUserId,
    selectUserForm,
    selectFormImage,
    (userId, userForm, formImage) => ({
        userId, userForm, formImage
    })
);

export const selectUserImage = createSelector(selectUserDetails,
    (userDetails: UserDetails) => ({
        image: userDetails.image,
        isDefaultImage: userDetails.isDefaultImage
    })
);

// export const selectDefaultImage = createSelector(selectUserFeature, (state: UserState) =>
//     state.defaultUserImage);