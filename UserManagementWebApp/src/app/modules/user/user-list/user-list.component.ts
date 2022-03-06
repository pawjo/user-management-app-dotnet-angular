import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { UserService } from 'src/app/core/user.service';
import { UserListItem } from 'src/app/shared/models/user-list-item';
import { AppState } from 'src/app/store/app.state';
import { loadUserList } from 'src/app/store/user.actions';
import { selectUsers } from 'src/app/store/user.selectors';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  users$ = this.store.select(selectUsers);

  constructor(private store: Store<AppState>) { }

  ngOnInit(): void {
    this.store.dispatch(loadUserList());
  }

}
