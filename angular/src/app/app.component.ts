import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from '@shared/layout/header/header.component';
import { FooterComponent } from "@shared/layout/footer/footer.component";
import { SignalRService } from '@services/signalr.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, FooterComponent],
})
export class AppComponent {

  constructor(private signalRService: SignalRService) { }

  ngOnInit() {
    this.signalRService.startConnection();
  }
}
