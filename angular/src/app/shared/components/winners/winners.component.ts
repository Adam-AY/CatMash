import { Component, Input } from "@angular/core";
import { Cat } from "@services/cat.service";
import { CardComponent } from "../card/card.component";

// Version 1 - A modifier
@Component({
    selector: 'app-winners',
    templateUrl: './winners.component.html',
    styleUrl: './winners.component.scss',
    standalone: true,
    imports: [CardComponent]
})
export class WinnersComponent {

    @Input() winners: Cat[] = [];

}