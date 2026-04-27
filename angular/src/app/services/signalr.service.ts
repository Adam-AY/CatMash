import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Cat, CatService } from './cat.service';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  private readonly VOTE_INCREMENTED_GROUP_NAME = 'VoteIncremented';
  private readonly WINNERS_UPDATED_GROUP_NAME = 'WinnersUpdated';

  private apiUrl = `${environment.apiUrl}/notificationHub`;
  private hubConnection!: signalR.HubConnection;

  private totalVotesSubject = new BehaviorSubject<number>(0);
  totalVotes$ = this.totalVotesSubject.asObservable();

  private winnersSubject = new BehaviorSubject<Cat[]>([]);
  winners$ = this.winnersSubject.asObservable();

  constructor(private catService: CatService) { }

  startConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.apiUrl, {
        withCredentials: false
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('SignalR Connected'))
      .catch(err => console.error('Error while starting connection: ' + err));
  }

  listenForUpdates() {
    this.hubConnection.on(this.VOTE_INCREMENTED_GROUP_NAME, (total) => {
      this.totalVotesSubject.next(total);
    });

    this.hubConnection.on(this.WINNERS_UPDATED_GROUP_NAME, (winners: Cat[]) => {
      this.winnersSubject.next(winners);
    });
  }

  initWinners() {
    this.catService.getWinners().subscribe(winners => {
      this.winnersSubject.next(winners);
    });
  }

  initTotalVotes() {
    this.catService.getTotalVotes().subscribe(total => {
      this.totalVotesSubject.next(total);
    });
  }
}