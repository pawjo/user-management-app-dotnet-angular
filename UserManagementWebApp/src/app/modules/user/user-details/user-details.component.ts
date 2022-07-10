import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Store } from '@ngrx/store';
import { UserDetails } from 'src/app/shared/models/user-details';
import { UserImage } from 'src/app/shared/models/user-image';
import { UserListItem } from 'src/app/shared/models/user-list-item';
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

  user?: UserDetails;

  constructor(private store: Store<AppState>,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    const param = this.route.snapshot.paramMap.get('userId');
    this.userId = parseInt(param!);
    this.store.dispatch(loadUserDetails({ userId: this.userId }));
    this.store.select(selectUserDetails).subscribe(x => {
      // if (!x.image) {
      //   x.image = { name: '', url: this.userImageUrl, expiresOn: new Date() };
      // }
      this.user = x;
    });
  }

  get userImageUrl(): string {
    return this.user?.image ? this.user?.image.url : '';
  }

}
