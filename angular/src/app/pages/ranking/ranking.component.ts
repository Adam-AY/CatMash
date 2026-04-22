import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CatService, Cat } from '@services/cat.service';
import { Observable, map } from 'rxjs';
import { CardComponent } from '@shared/components/card/card.component';

@Component({
    selector: 'app-ranking',
    templateUrl: './ranking.component.html',
    styleUrl: './ranking.component.scss',
    standalone: true,
    imports: [CommonModule, CardComponent]
})
export class RankingComponent implements OnInit {

  cats: Cat[] = [];

  constructor(private catService: CatService) {}

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