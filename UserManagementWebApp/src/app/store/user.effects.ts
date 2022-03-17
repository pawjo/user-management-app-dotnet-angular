import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { UserService } from "../core/user.service";
import { map, mergeMap, catchError, exhaustMap } from 'rxjs/operators';
import { of } from 'rxjs';
import { loadUserDetails, loadUserList, loadUserListError, loadUserListSuccess } from "./user.actions";


@Injectable()
export class UserEffects {

    loadUsers$ = createEffect(() => this.actions$.pipe(
        ofType(loadUserList),
        exhaustMap(() => this.userService.getUserList()
            .pipe(
                map(users => loadUserListSuccess({users})),
                catchError(() => of(loadUserListError()))
            ))
        )
    );

    // loadUserDetails$=createEffect(()=> this.actions$.pipe(
    //     ofType(loadUserDetails),
    //     exhaustMap(()=>this.userService)
    // ))

    constructor(private actions$: Actions,
        private userService: UserService) { }
}