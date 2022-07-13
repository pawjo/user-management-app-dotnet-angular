import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'

import { AppComponent } from './app.component';
import { UserListComponent } from './modules/user/user-list/user-list.component';
import { AppRoutingModule } from './app-routing.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { UserEffects } from './store/user.effects';
import { userReducer } from './store/user.reducer';
import { userFeatureKey } from './store/userRoot.state';
import { UserDetailsComponent } from './modules/user/user-details/user-details.component';
import { FormComponent } from './modules/user/form/form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NgrxFormsModule } from 'ngrx-forms';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';

@NgModule({
  declarations: [
    AppComponent,
    UserListComponent,
    UserDetailsComponent,
    FormComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    NgbModule,
    StoreModule.forRoot({[userFeatureKey]: userReducer}),
    EffectsModule.forRoot([UserEffects]),
    ReactiveFormsModule,
    NgrxFormsModule,
    StoreDevtoolsModule.instrument()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
