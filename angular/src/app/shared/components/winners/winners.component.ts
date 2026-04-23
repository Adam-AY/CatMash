import { Component, OnInit } from "@angular/core";
import { Cat } from "@services/cat.service";
import { CardComponent } from "../card/card.component";
import { SignalRService } from "@services/signalr.service";
import { Observable } from "rxjs";
import { AsyncPipe, NgIf } from "@angular/common";

@Component({
    selector: 'app-winners',
    templateUrl: './winners.component.html',
    styleUrl: './winners.component.scss',
    standalone: true,
    imports: [CardComponent, NgIf, AsyncPipe]
})
export class WinnersComponent implements OnInit {

    winners$!: Observable<Cat[]>;

    constructor(private signalRService: SignalRService) { }

    ngOnInit() {
        this.winners$ = this.signalRService.winners$;
    }
}