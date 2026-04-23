import { AsyncPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { NavigationEnd, Router, RouterLink } from '@angular/router';
import { SignalRService } from '@services/signalr.service';
import { filter, Observable } from 'rxjs';
import { RoutesPart } from 'src/app/app.routes';

export interface Footer {
    caption: string,
    goToUrl: string
}

@Component({
    selector: 'app-footer',
    standalone: true,
    templateUrl: './footer.component.html',
    styleUrl: './footer.component.scss',
    imports: [RouterLink, AsyncPipe],
})
export class FooterComponent implements OnInit {

    footer: Footer | null = null;

    totalVotes$!: Observable<number>;

    constructor(private router: Router, private signalRService: SignalRService) {
        this.router.events.pipe(filter(e => e instanceof NavigationEnd), takeUntilDestroyed())
            .subscribe((event: NavigationEnd) => {
              const currentRoute = event.urlAfterRedirects?.replace('/', '');
                this.init(currentRoute);
            });
    }

    ngOnInit(): void {
        this.totalVotes$ = this.signalRService.totalVotes$;
    }

    private init(currentRoute: string): void {
        switch (currentRoute) {
            case RoutesPart.ranking:
                this.footer = { caption: 'Revenir au vote', goToUrl: RoutesPart.voting };
                break;
            case RoutesPart.voting:
                this.footer = { caption: 'Voir le classement des chats', goToUrl: RoutesPart.ranking };
                break;
        }
    }
}
