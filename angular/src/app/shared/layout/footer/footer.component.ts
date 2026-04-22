import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router, RouterLink } from '@angular/router';
import { CatService } from '@services/cat.service';
import { filter } from 'rxjs';
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
    imports: [RouterLink],
})
export class FooterComponent implements OnInit {

    footer: Footer | undefined = undefined;

    constructor(private router: Router, public catService: CatService) { }

    ngOnInit(): void {
        this.router.events.pipe(
            filter(e => e instanceof NavigationEnd)
        ).subscribe((event: NavigationEnd) => {
            const currentRoute = event.urlAfterRedirects?.replace('/', '');
            this.init(currentRoute);
        });
    }

    private init(currentRoute: string): void {

        switch (currentRoute) {
            case RoutesPart.ranking:
                this.setFooter('Revenir au vote', RoutesPart.voting)
                break;
            case RoutesPart.voting:
                this.setFooter('Voir le classement des chats', RoutesPart.ranking)
                break;
        }

        this.catService.getTotalVotes();
    }

    private setFooter(caption: string, url: string): void {
        this.footer = { caption: caption, goToUrl: url };
    }
}