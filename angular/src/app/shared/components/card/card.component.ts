import { DecimalPipe, NgClass } from "@angular/common";
import { Component, Input } from "@angular/core";
import { Cat } from "@services/cat.service";


@Component({
    selector: 'app-card',
    templateUrl: './card.component.html',
    styleUrl: './card.component.scss',
    standalone: true,
    imports: [NgClass, DecimalPipe]
})
export class CardComponent {

    @Input({ required: true }) item!: Cat;
    @Input({ required: true }) rank!: number;
    @Input() cssClass: string = '';
}