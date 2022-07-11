import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { UserService } from "../core/user.service";
import { map, catchError, exhaustMap, switchMap } from 'rxjs/operators';
import { of } from 'rxjs';
import { loadUserDetails, loadUserDetailsError, loadUserDetailsSuccess, loadUserList, loadUserListError, loadUserListSuccess } from "./user.actions";
import { ImageService } from "../core/image.service";


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

    constructor(private actions$: Actions,
        private userService: UserService,
        private imageService: ImageService) { }
}