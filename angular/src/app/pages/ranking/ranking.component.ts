import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CatService, Cat } from '@services/cat.service';
import { Observable, map } from 'rxjs';
import { CardComponent } from '@shared/components/card/card.component';
import { WinnersComponent } from "@shared/components/winners/winners.component";

@Component({
    selector: 'app-ranking',
    templateUrl: './ranking.component.html',
    styleUrl: './ranking.component.scss',
    standalone: true,
    imports: [CommonModule, CardComponent, WinnersComponent]
})
export class RankingComponent implements OnInit {

    cats: Cat[] = [];
    winners: Cat[] = [];

    constructor(private catService: CatService) { }

    ngOnInit(): void {
        this.loadCats();
    }

    loadCats() {
        this.catService.getCats().subscribe({
            next: (data) => {
                this.cats = data;
                this.getWinners();
            },
            error: (err) => console.error(err)
        });
    }


    // Pour le moment on prend que les 3 premiers ! si un 4eme à le meme score, on l'ignore
    // Temporaire
    // A modifier
    getWinners() {
        if (!this.cats || this.cats.length === 0 || this.cats[0].score == 0) {
            this.winners = [];
            return;
        }

        const sorted = [...this.cats].filter(c => c.score > 0).sort((a, b) => b.score - a.score);

        this.winners = sorted.slice(0, 3);
        this.cats = this.cats.filter(c => !this.winners.includes(c));
    }
}