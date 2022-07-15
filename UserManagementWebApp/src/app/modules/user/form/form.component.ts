import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormBuilder } from '@angular/forms';
import { Store } from '@ngrx/store';
import { FormControlState, FormGroupState } from 'ngrx-forms';
import { Observable } from 'rxjs';
import { UserForm } from 'src/app/shared/models/user-form';
import { AppState } from 'src/app/store/app.state';
import { loadUserForEdit, saveEditedUser, saveNewUser } from 'src/app/store/user.actions';
import { selectUserForm } from 'src/app/store/user.selectors';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {

  title: string = 'Add new user';

  formState$: Observable<FormGroupState<UserForm>> = this.store.select(selectUserForm);

  editedUserId: number = -1;

  constructor(private fb: FormBuilder,
    private store: Store<AppState>,
    private location: Location,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    const param = this.route.snapshot.paramMap.get('userId');
    this.editedUserId = parseInt(param!);
    if (isNaN(this.editedUserId)) {
      console.error('Wrong user id');
    }
    else {
      this.title = 'Edit user ' + this.editedUserId;
      this.store.dispatch(loadUserForEdit({ userId: this.editedUserId }));
    }
  }

  save(): void {
    if (this.editedUserId !== -1) {
      this.store.dispatch(saveEditedUser({ userId: this.editedUserId }));
    }
    else {
      this.store.dispatch(saveNewUser());
    }
    this.router.navigate(['']);
  }

  cancel(): void {
    return this.location.back();
  }

  isControlInvalid(control: FormControlState<string>): boolean {
    const result = control.isTouched && Object.keys(control.errors).length !== 0;
    return result;
  }
}
