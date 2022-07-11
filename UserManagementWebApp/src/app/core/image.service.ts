import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserImage } from '../shared/models/user-image';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  private baseUrl = environment.apiUrl + '/image/sas';

  constructor(private httpClient: HttpClient) { }

  getImageByName(name: string): Observable<UserImage> {
    return this.httpClient.get<UserImage>(`${this.baseUrl}/${name}`);
  }
}
