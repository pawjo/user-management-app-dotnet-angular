import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store/app.state';
import { deleteUser, loadUserDetails, loadUserDetailsError } from 'src/app/store/user.actions';
import { selectUserDetails } from 'src/app/store/user.selectors';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {

  userId: number = 0;

  user$ = this.store.select(selectUserDetails);


  // defaultImage$ = this.store.select(selectDefaultImage);

  constructor(private store: Store<AppState>,
    private route: ActivatedRoute,
    private router: Router) { }


  ngOnInit(): void {
    const param = this.route.snapshot.paramMap.get('userId');
    this.userId = parseInt(param!);
    if (isNaN(this.userId)) {
      console.error('Wrong user id');
    }
    else {
      this.store.dispatch(loadUserDetails({ userId: this.userId }));
    }
  }

  delete(): void {
    if (confirm("Are you sure to delete this user?")) {
      this.store.dispatch(deleteUser());
      this.router.navigate(['']);
    }
  }
}
