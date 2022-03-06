import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { UserService } from "../core/user.service";
import { map, mergeMap, catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { loadUserList, loadUserListError, loadUserListSuccess } from "./user.actions";


@Injectable()
export class UserEffects {

    loadUsers$ = createEffect(() => this.actions$.pipe(
        ofType(loadUserList),
        mergeMap(() => this.userService.getUserList()
            .pipe(
                map(users => loadUserListSuccess({users})),
                catchError(() => of(loadUserListError()))
            ))
        )
    );

    constructor(private actions$: Actions,
        private userService: UserService) { }
}