import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { UserService } from "../core/user.service";
import { map, catchError, exhaustMap, switchMap, withLatestFrom } from 'rxjs/operators';
import { of } from 'rxjs';
import { deleteFormImage, loadUserDetails, loadUserDetailsError, loadUserDetailsSuccess, loadUserForEdit, loadUserForEditError, loadUserForEditSuccess, loadUserList, loadUserListError, loadUserListSuccess, saveEditedUser, saveEditedUserError, saveEditedUserSuccess, saveNewUser, saveNewUserError, saveNewUserSuccess, uploadFormImage, changeFormImageError, uploadFormImageSkipped, changeFormImageSuccess } from "./user.actions";
import { ImageService } from "../core/image.service";
import { AppState } from "./app.state";
import { Store } from "@ngrx/store";
import { selectEditUserFormWithId, selectFormImageWithId, selectUserForm, selectUserId } from "./user.selectors";
import { NewUser } from "../shared/models/new-user";
import { EditedUser } from "../shared/models/edited-user";
import { createFormGroupState } from "ngrx-forms";
import { UserForm } from "../shared/models/user-form";
import { initialFormImage, USER_FORM_ID } from "./user.reducer";


@Injectable()
export class UserEffects {

    loadUsers$ = createEffect(() => this.actions$.pipe(
        ofType(loadUserList),
        exhaustMap(() => this.userService.getUserList()
            .pipe(
                map(users => loadUserListSuccess({ users })),
                catchError(() => of(loadUserListError()))
            ))
    ));

    loadUserDetails$ = createEffect(() => this.actions$.pipe(
        ofType(loadUserDetails),
        switchMap(action => this.userService.getUserDetails(action.userId)
            .pipe(
                map(user => loadUserDetailsSuccess({ userDetails: user })),
                catchError(() => of(loadUserDetailsError()))
            ))
    ));

    // loadDefaultImage$ = createEffect(() => this.actions$.pipe(
    //     ofType(loadDefaultImage),
    //     switchMap(() => this.imageService.getImageByName('default.png')
    //         .pipe(
    //             map(image => {
    //                 console.log(image);
    //                 return loadDefaultImageSuccess({ defaultImage: image });
    //             }),
    //             catchError(() => of(loadDefaultImageError()))
    //         ))
    // ));

    saveNewUser$ = createEffect(() => this.actions$
        .pipe(
            ofType(saveNewUser),
            withLatestFrom(this.store.select(selectUserForm)),
            switchMap(([action, userForm]) => {
                const newUser: NewUser = {
                    name: userForm.controls.name.value,
                    surname: userForm.controls.surname.value,
                    email: userForm.controls.email.value,
                    age: parseInt(userForm.controls.age.value)
                    // age: userForm.controls.age.value
                };
                return this.userService.addNewUser(newUser).pipe(
                    map(id => saveNewUserSuccess({ userId: id })),
                    catchError(() => of(saveNewUserError()))
                );
            })
        )
    );

    loadUserForEdit$ = createEffect(() => this.actions$
        .pipe(
            ofType(loadUserForEdit),
            switchMap(action => this.userService.getUserDetails(action.userId)
                .pipe(
                    map(user => {
                        const data: UserForm = {
                            name: user.name,
                            surname: user.surname,
                            email: user.email,
                            age: user.age.toString()
                        };
                        const form = createFormGroupState<UserForm>(USER_FORM_ID, data);

                        return loadUserForEditSuccess({ userDetails: user, userForm: form });
                    }),
                    catchError(() => of(loadUserForEditError()))
                )
            )
        )
    );

    saveEditedUser$ = createEffect(() => this.actions$
        .pipe(
            ofType(saveEditedUser),
            withLatestFrom(this.store.select(selectEditUserFormWithId)),
            switchMap(([action, data]) => {
                const controls = data.userForm.controls;
                const editedUser: EditedUser = {
                    id: data.userId,
                    name: controls.name.value,
                    surname: controls.surname.value,
                    email: controls.email.value,
                    age: parseInt(controls.age.value)
                };
                return this.userService.updateUser(editedUser).pipe(
                    map(() => saveEditedUserSuccess()),
                    catchError(() => of(saveEditedUserError()))
                );
            })
        )
    );

    saveFormImage$ = createEffect(() => this.actions$
        .pipe(
            ofType(uploadFormImage, saveEditedUserSuccess, saveNewUserSuccess),
            withLatestFrom(this.store.select(selectFormImageWithId)),
            switchMap(([action, data]) => {
                if (data.formImage === initialFormImage) {
                    return of(uploadFormImageSkipped());
                }

                const formData = new FormData();
                formData.append('image', data.formImage);
                return this.imageService.upload(data.userId, formData).pipe(
                    map(() => changeFormImageSuccess()),
                    catchError(() => of(changeFormImageError()))
                );
            })
        )
    );

    deleteFormImage$ = createEffect(() => this.actions$
        .pipe(
            ofType(deleteFormImage),
            withLatestFrom(this.store.select(selectUserId)),
            switchMap(([action, userId]) => this.imageService.delete(userId).pipe(
                map(() => changeFormImageSuccess()),
                catchError(() => of(changeFormImageError()))
            ))
        )
    );

    constructor(private actions$: Actions,
        private userService: UserService,
        private imageService: ImageService,
        private store: Store<AppState>) { }
}