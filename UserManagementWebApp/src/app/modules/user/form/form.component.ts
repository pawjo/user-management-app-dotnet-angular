import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {

  userForm: FormGroup = this.fb.group({
    name: [''],
    surname: [''],
    email: [''],
    age: ['']
  });

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {


  }
}
