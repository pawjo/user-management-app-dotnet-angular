import { createReducer, on } from "@ngrx/store";
import { UserForm } from "../shared/models/user-form";
import { EmptyImage } from "../shared/models/user-image";
import { loadUserDetails, loadUserDetailsSuccess, loadUserListSuccess, saveNewUserSuccess } from "./user.actions";
import { UserState } from "./user.state";
import { createFormGroupState, onNgrxForms } from "ngrx-forms";


const USER_FORM_ID = 'user-form-id';

const initialFormState = createFormGroupState<UserForm>(USER_FORM_ID, {
    name: '',
    surname: '',
    email: '',
    age: ''
});


const initialState: UserState = {
    users: [],
    userDetails: {
        id: 0,
        name: '',
        surname: '',
        age: 0,
        email: '',
        isDefaultImage: true,
        image: EmptyImage
    },
    userForm: initialFormState
};


export const userReducer = createReducer(
    initialState,
    onNgrxForms(),
    on(loadUserListSuccess, (state, { users }) => ({ ...state, users: users })),
    on(loadUserDetails, (state, { userId }) => ({ ...state, userId: userId })),
    on(loadUserDetailsSuccess, (state, { userDetails }) => ({ ...state, userDetails: userDetails })),
    on(saveNewUserSuccess, (state) => ({ ...state, userForm: initialFormState }))
);