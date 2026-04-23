import { Component, OnInit } from "@angular/core";
import { Cat } from "@services/cat.service";
import { CardComponent } from "../card/card.component";
import { SignalRService } from "@services/signalr.service";
import { NgFor } from "@angular/common";
import { takeUntilDestroyed } from "@angular/core/rxjs-interop";

type PodiumItem = {
    cat: Cat;
    rank: number;
    cssClass: string;
};

@Component({
    selector: 'app-winners',
    templateUrl: './winners.component.html',
    styleUrl: './winners.component.scss',
    standalone: true,
    imports: [CardComponent, NgFor]
})
export class WinnersComponent {

    podium: PodiumItem[] = [];

    constructor(private signalRService: SignalRService) {
        this.signalRService.winners$
            .pipe(takeUntilDestroyed())
            .subscribe((data) => this.podium = this.getPodium(data));
    }

    private getPodium(winners: Cat[]): PodiumItem[] {

        if (!winners || winners.length === 0) return [];
        
        let rank = 1;

        const ranked = winners.map((cat, index) => {
            if (index > 0 && cat.score !== winners[index - 1].score) {
                rank++;
            }

            return { cat, rank, cssClass: this.getClass(rank) };
        });

        const first = ranked[0];
        const second = ranked[1];
        const third = ranked[2];

        return [second, first, third].filter(Boolean);
    }

    private getClass(rank: number): string {
        if (rank === 1) return 'first-place';
        if (rank === 2) return 'second-place';
        if (rank === 3) return 'third-place';
        return '';
    }
}