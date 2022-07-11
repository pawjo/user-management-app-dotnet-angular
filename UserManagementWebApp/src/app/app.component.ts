import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from './store/app.state';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  title = 'UserManagementWebApp';

  constructor(private store: Store<AppState>) { }

  // ngOnInit(): void {
  //   this.store.dispatch(loadDefaultImage());
  // }
}
