import { createReducer, on } from "@ngrx/store";
import { UserForm } from "../shared/models/user-form";
import { EmptyImage } from "../shared/models/user-image";
import { changeFormImage, deleteFormImage as deleteFormImage, loadUserDetails, loadUserDetailsSuccess, loadUserForEdit, loadUserForEditSuccess, loadUserListSuccess, saveNewUserSuccess, changeFormImageSuccess, deleteUserSuccess, loadUserList, resetForm } from "./user.actions";
import { UserState } from "./user.state";
import { createFormGroupState, onNgrxForms, updateGroup, validate, wrapReducerWithFormStateUpdate } from "ngrx-forms";
import { required, email, pattern } from 'ngrx-forms/validation';
import { UserDetails } from "../shared/models/user-details";


export const USER_FORM_ID = 'user-form-id';

const initialUserDetails: UserDetails = {
    id: 0,
    name: '',
    surname: '',
    age: 0,
    email: '',
    isDefaultImage: true,
    image: EmptyImage
};

const initialFormState = createFormGroupState<UserForm>(USER_FORM_ID, {
    name: '',
    surname: '',
    email: '',
    age: ''
});

export const initialFormImage = new File([], '');

const initialState: UserState = {
    users: [],
    userDetails: initialUserDetails,
    userId: -1,
    userForm: initialFormState,
    formImage: initialFormImage
};


export const userReducer = createReducer(
    initialState,
    onNgrxForms(),
    on(loadUserListSuccess, (state, { users }) => ({ ...state, users: users })),
    on(loadUserDetails, (state, { userId }) => ({ ...state, userId: userId })),
    on(loadUserDetailsSuccess, (state, { userDetails }) => ({ ...state, userDetails: userDetails })),
    on(saveNewUserSuccess, (state, { userId }) => ({ ...state, userId: userId, userForm: initialFormState })),
    on(loadUserForEdit, (state, { userId }) => ({ ...state, userId: userId })),
    on(loadUserForEditSuccess, (state, { userDetails, userForm }) =>
        ({ ...state, userDetails: userDetails, userForm: userForm })),
    on(changeFormImage, (state, { formImage }) => ({ ...state, formImage: formImage })),
    on(changeFormImageSuccess, (state) => ({ ...state, formImage: initialFormImage })),
    on(deleteFormImage, (state) => ({ ...state, formImage: initialFormImage })),
    on(deleteUserSuccess, (state) => ({ ...state, userDetails: initialUserDetails })),
    on(resetForm, (state) => ({
        ...state,
        userForm: initialFormState,
        formImage: initialFormImage,
        userDetails: initialUserDetails
    }))
);

const validationReducer = updateGroup<UserForm>({
    name: validate(required),
    surname: validate(required),
    age: validate(required, pattern(new RegExp("^\\d+$"))),
    email: validate(required, email)
});

export const appReducerWithValidation = wrapReducerWithFormStateUpdate(
    userReducer,
    (state: UserState) => state.userForm,
    validationReducer
)