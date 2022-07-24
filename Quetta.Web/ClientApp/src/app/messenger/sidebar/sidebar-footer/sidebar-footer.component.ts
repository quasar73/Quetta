import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { UntypedFormControl } from '@angular/forms';
import { TranslocoService } from '@ngneat/transloco';
import { availableLanguage } from 'src/app/shared/consts/languages.const';

@Component({
    selector: 'qtt-sidebar-footer',
    templateUrl: './sidebar-footer.component.html',
    styleUrls: ['./sidebar-footer.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SidebarFooterComponent implements OnInit {
    languagesControl = new UntypedFormControl('en');

    get languages(): string[] {
        return availableLanguage;
    }

    constructor(private readonly translocoService: TranslocoService) {}

    ngOnInit(): void {
        this.languagesControl.setValue(this.translocoService.getActiveLang());

        this.languagesControl.valueChanges.subscribe(language => {
            this.translocoService.setActiveLang(language);
        });
    }
}
