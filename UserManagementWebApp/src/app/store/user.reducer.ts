import { createReducer, on } from "@ngrx/store";
import { loadUserListSuccess } from "./user.actions";
import { UserState } from "./user.state";

const initialState: UserState = {};

export const userReducer = createReducer(
    initialState,
    on(loadUserListSuccess, (state, { users }) => users.reduce((acc, user) => ({
        ...acc,
        [user.id]: user
    }), {}))
);