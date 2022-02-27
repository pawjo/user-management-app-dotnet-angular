import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/user.service';
import { UserListItem } from 'src/app/shared/models/user-list-item';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  users: UserListItem[] = [];

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.getUserList().subscribe(x => this.users = x);
  }

}
