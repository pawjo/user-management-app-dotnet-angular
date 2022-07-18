import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserImage } from '../shared/models/user-image';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  private baseUrl = environment.apiUrl + '/image';
  private sasUrl = this.baseUrl + '/sas';

  constructor(private httpClient: HttpClient) { }

  getImageByName(name: string): Observable<UserImage> {
    return this.httpClient.get<UserImage>(`${this.sasUrl}/${name}`);
  }

  upload(userId: number, image: FormData): Observable<any> {
    return this.httpClient.put(`${this.baseUrl}/${userId}`, image);
  }

  delete(userId: number): Observable<any> {
    return this.httpClient.delete(`${this.baseUrl}/${userId}`);
  }
}
