import { Routes } from '@angular/router';
import { RankingComponent } from '@pages/ranking/ranking.component';

export class RoutesPart {
    static voting = 'voting';
    static ranking = 'ranking';
}

export const routes: Routes = [
    { path: '', redirectTo: RoutesPart.ranking, pathMatch: 'full' },
    {path: RoutesPart.ranking, component: RankingComponent}
];
