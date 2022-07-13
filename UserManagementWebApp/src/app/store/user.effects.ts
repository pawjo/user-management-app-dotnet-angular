import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { UserService } from "../core/user.service";
import { map, catchError, exhaustMap, switchMap, withLatestFrom } from 'rxjs/operators';
import { of } from 'rxjs';
import { loadUserDetails, loadUserDetailsError, loadUserDetailsSuccess, loadUserList, loadUserListError, loadUserListSuccess, saveNewUser, saveNewUserError, saveNewUserSuccess } from "./user.actions";
import { ImageService } from "../core/image.service";
import { AppState } from "./app.state";
import { Store } from "@ngrx/store";
import { selectUserForm } from "./user.selectors";
import { NewUser } from "../shared/models/new-user";


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
                    map(() => saveNewUserSuccess()),
                    catchError(() => of(saveNewUserError()))
                )
            })
        )
    );

    // saveEditedUser$ = createEffect(()=>this.actions$.pipe(
    //     ofType(saveEditedUser),
    //     switchMap(action=>)
    // ))

    constructor(private actions$: Actions,
        private userService: UserService,
        private imageService: ImageService,
        private store: Store<AppState>) { }
}