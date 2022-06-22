import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { TranslocoService } from '@ngneat/transloco';
import { availableLanguage } from 'src/app/shared/consts/languages.const';

@Component({
    selector: 'qtt-sidebar-footer',
    templateUrl: './sidebar-footer.component.html',
    styleUrls: ['./sidebar-footer.component.scss'],
})
export class SidebarFooterComponent implements OnInit {
    languagesControl = new FormControl('en');

    get languages(): string[] {
        return availableLanguage;
    }

    constructor(private translocoService: TranslocoService) {}

    ngOnInit(): void {
        this.languagesControl.setValue(this.translocoService.getActiveLang());

        this.languagesControl.valueChanges.subscribe(language => {
            this.translocoService.setActiveLang(language);
        });
    }
}
