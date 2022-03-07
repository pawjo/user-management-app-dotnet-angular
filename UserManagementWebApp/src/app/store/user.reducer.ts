import { createReducer, on } from "@ngrx/store";
import { AppState } from "./app.state";
import { loadUserListSuccess } from "./user.actions";
import { UserState } from "./user.state";

const initialState: UserState = { users: [] };

// export const userReducer = createReducer(
//     initialState,
//     on(loadUserListSuccess, (state, { users }) => users.reduce((acc, user) => ({
//         ...acc,
//         [user.id]: user
//     }), { users: [] }))
// );

export const userReducer = createReducer(
    initialState,
    on(loadUserListSuccess, (state, { users }) => ({ ...state, users: users }))
);