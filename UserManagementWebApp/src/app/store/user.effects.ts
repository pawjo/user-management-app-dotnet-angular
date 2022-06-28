import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { UserService } from "../core/user.service";
import { map, catchError, exhaustMap, switchMap } from 'rxjs/operators';
import { of } from 'rxjs';
import { loadUserDetails, loadUserDetailsError, loadUserDetailsSuccess, loadUserList, loadUserListError, loadUserListSuccess } from "./user.actions";


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
                map(user => {
                    return loadUserDetailsSuccess({ userDetails: user });
                }),
                catchError(() => of(loadUserDetailsError()))
            ))
    ));

    constructor(private actions$: Actions,
        private userService: UserService) { }
}