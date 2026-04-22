import { Routes } from '@angular/router';
import { RankingComponent } from '@pages/ranking/ranking.component';
import { VoteComponent as VotingComponent } from '@pages/voting/voting.component';

export class RoutesPart {
    static voting = 'voting';
    static ranking = 'ranking';
}

export const routes: Routes = [
    { path: '', redirectTo: RoutesPart.voting, pathMatch: 'full' },
    {path: RoutesPart.voting, component: VotingComponent},
    {path: RoutesPart.ranking, component: RankingComponent}
];
