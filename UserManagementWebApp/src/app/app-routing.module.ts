import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserDetailsComponent } from './modules/user/user-details/user-details.component';
import { UserListComponent } from './modules/user/user-list/user-list.component';


const routes: Routes = [
  { path: 'users', component: UserListComponent },
  { path: 'details/:userId', component: UserDetailsComponent },
  { path: '', redirectTo: '/users', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
