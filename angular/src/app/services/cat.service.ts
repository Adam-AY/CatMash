import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

export interface Cat {
  id: string;
  url: string;
  score: number;
}

export interface Vote {
  winnerId: string;
  loserId: string;
}

@Injectable({
  providedIn: 'root'
})
export class CatService {

  private apiUrl = `${environment.apiUrl}/cats`;

  totalVotes: number = 0;

  constructor(private http: HttpClient) { }

  getCats(): Observable<Cat[]> {
    return this.http.get<Cat[]>(this.apiUrl);
  }

  getRandomCats(): Observable<Cat[]> {
    return this.http.get<Cat[]>(`${this.apiUrl}/random`);
  }

  vote(data: Vote) {
    return this.http.post<void>(`${this.apiUrl}/vote`, data);
  }

  getTotalVotes(): void {
    this.http.get<number>(`${this.apiUrl}/totalVotes`).subscribe({
      next: (data) => this.totalVotes = data,
      error: (err) => console.error(err)
    });
  }
}
