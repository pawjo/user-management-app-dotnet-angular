import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserDetails } from '../shared/models/user-details';
import { UserListItem } from '../shared/models/user-list-item';
import { tap } from 'rxjs/operators'
import { NewUser } from '../shared/models/new-user';
import { EditedUser } from '../shared/models/edited-user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  private baseUrl = environment.apiUrl + '/user';

  getUserList(): Observable<UserListItem[]> {
    return this.httpClient.get<UserListItem[]>(this.baseUrl);
  }

  getUserDetails(userId: number): Observable<UserDetails> {
    return this.httpClient.get<UserDetails>(`${this.baseUrl}/details/${userId}`)
      .pipe(tap(x => console.log(x)));
  }

  addNewUser(newUser: NewUser): Observable<number> {
    return this.httpClient.post<number>(this.baseUrl, newUser);
  }

  updateUser(editedUser: EditedUser):Observable<any>{
    return this.httpClient.put(this.baseUrl, editedUser);
  }

  delete(userId:number):Observable<any>{
    return this.httpClient.delete(`${this.baseUrl}/${userId}`);
  }
}
