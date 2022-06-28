import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Store } from '@ngrx/store';
import { User } from 'src/app/shared/models/user';
import { AppState } from 'src/app/store/app.state';
import { loadUserDetails } from 'src/app/store/user.actions';
import { selectUserDetails } from 'src/app/store/user.selectors';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {

  userId: number = 0;

  user?: User;

  constructor(private store: Store<AppState>,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    const param = this.route.snapshot.paramMap.get('userId');
    this.userId = parseInt(param!);
    this.store.dispatch(loadUserDetails({ userId: this.userId }));
    this.store.select(selectUserDetails).subscribe(x => {
      this.user = x;
    });
  }

}
