import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Store } from '@ngrx/store';
import { FormControlState, FormGroupState } from 'ngrx-forms';
import { Observable } from 'rxjs';
import { UserForm } from 'src/app/shared/models/user-form';
import { AppState } from 'src/app/store/app.state';
import { saveNewUser } from 'src/app/store/user.actions';
import { selectUserForm } from 'src/app/store/user.selectors';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {

  formState$: Observable<FormGroupState<UserForm>> = this.store.select(selectUserForm);

  // userForm: UntypedFormGroup = this.fb.group({
  //   name: [''],
  //   surname: [''],
  //   email: [''],
  //   age: ['']
  // });

  constructor(private fb: FormBuilder,
    private store: Store<AppState>) { }

  ngOnInit(): void {
  }

  save(): void {
    this.store.dispatch(saveNewUser());
  }

  isControlInvalid(control: FormControlState<string>): boolean {
    const result = control.isTouched && Object.keys(control.errors).length !== 0;
    return result;
  }
}
