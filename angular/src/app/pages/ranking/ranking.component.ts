import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CatService, Cat } from '@services/cat.service';
import { CardComponent } from '@shared/components/card/card.component';
import { WinnersComponent } from "@shared/components/winners/winners.component";
import { SignalRService } from '@services/signalr.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
    selector: 'app-ranking',
    templateUrl: './ranking.component.html',
    styleUrl: './ranking.component.scss',
    standalone: true,
    imports: [CommonModule, CardComponent, WinnersComponent]
})
export class RankingComponent implements OnInit {
    cats: Cat[] = [];
    winnersCount = 0;

    constructor(private catService: CatService, private signalRService: SignalRService) {
        this.signalRService.winners$
            .pipe(takeUntilDestroyed())
            .subscribe(w => this.winnersCount = w.length);
    }

    ngOnInit(): void {
        this.loadCats();
    }

    loadCats() {
        this.catService.getCats().subscribe({
            next: (data) => this.cats = data,
            error: (err) => console.error(err)
        });
    }
}
