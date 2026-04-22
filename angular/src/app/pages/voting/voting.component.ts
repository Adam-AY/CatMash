import { Component, OnInit } from '@angular/core';
import { EmptyComponent } from '@shared/components/empty/empty.component';
import { Cat, CatService } from '@services/cat.service';

@Component({
  selector: 'app-voting',
  standalone: true,
  templateUrl: './voting.component.html',
  styleUrl: './voting.component.scss',
  imports: [EmptyComponent]
})
export class VoteComponent implements OnInit {

  pair!: Cat[];

  constructor(private catService: CatService) { }

  ngOnInit(): void {
    this.loadPair();
  }

  vote(winner: Cat): void {
    const loser = this.pair.find(c => c.id !== winner.id);
    this.catService.vote({ winnerId: winner.id, loserId: loser!.id }).subscribe(() => {
      this.loadPair();
      this.catService.getTotalVotes(); // Temporaire - à améliorer
    });
  }

  private loadPair() {
    this.catService.getRandomCats().subscribe({
      next: (data) => this.pair = data,
      error: (err) => console.error(err)
    });
  }
}