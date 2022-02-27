import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserListItem } from '../shared/models/user-list-item';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  private baseUrl = environment.apiUrl + '/user';

  getUserList(): Observable<UserListItem[]> {
    return this.httpClient.get<UserListItem[]>(this.baseUrl);
  }
}
