import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

export interface Cat {
  id: string;
  url: string;
  score: number;
}

@Injectable({
  providedIn: 'root'
})
export class CatService {

private apiUrl = `${environment.apiUrl}/cats`;

  constructor(private http: HttpClient) {}

  getCats(): Observable<Cat[]> {
    return this.http.get<Cat[]>(this.apiUrl);
  }
}
