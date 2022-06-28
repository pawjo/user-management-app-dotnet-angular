import { createReducer, on } from "@ngrx/store";
import { loadUserDetails, loadUserDetailsSuccess, loadUserListSuccess } from "./user.actions";
import { UserState } from "./user.state";

const initialState: UserState = {
    users: [],
    userDetails: {
        id: 0,
        name: '',
        surname: '',
        age: 0,
        email: ''
    }
};

// export const userReducer = createReducer(
//     initialState,
//     on(loadUserListSuccess, (state, { users }) => users.reduce((acc, user) => ({
//         ...acc,
//         [user.id]: user
//     }), { users: [] }))
// );

export const userReducer = createReducer(
    initialState,
    on(loadUserListSuccess, (state, { users }) => ({ ...state, users: users })),
    on(loadUserDetails, (state, { userId }) => ({ ...state, userId: userId })),
    on(loadUserDetailsSuccess, (state, { userDetails }) => ({ ...state, userDetails: userDetails }))
);