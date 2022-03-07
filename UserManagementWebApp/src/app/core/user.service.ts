import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  private baseUrl = environment.apiUrl + '/user';

  getUserList(): Observable<User[]> {
    return this.httpClient.get<User[]>(this.baseUrl);
  }
}
