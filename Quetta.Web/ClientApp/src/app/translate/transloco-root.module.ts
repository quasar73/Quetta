import { HttpClient } from '@angular/common/http';
import { TRANSLOCO_LOADER, Translation, TranslocoLoader, TRANSLOCO_CONFIG, translocoConfig, TranslocoModule } from '@ngneat/transloco';
import { TRANSLOCO_PERSIST_LANG_STORAGE, TranslocoPersistLangModule } from '@ngneat/transloco-persist-lang';
import { Injectable, NgModule } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class TranslocoHttpLoader implements TranslocoLoader {
    constructor(private readonly http: HttpClient) {}

    getTranslation(lang: string) {
        return this.http.get<Translation>(`/assets/i18n/${lang}.json`);
    }
}

@NgModule({
    exports: [TranslocoModule],
    imports: [
        TranslocoPersistLangModule.forRoot({
            storage: {
                provide: TRANSLOCO_PERSIST_LANG_STORAGE,
                useValue: localStorage,
            },
        }),
    ],
    providers: [
        {
            provide: TRANSLOCO_CONFIG,
            useValue: translocoConfig({
                availableLangs: ['en', 'ru'],
                defaultLang: 'en',
                reRenderOnLangChange: true,
                prodMode: environment.production,
                fallbackLang: 'en',
                missingHandler: {
                    useFallbackTranslation: true,
                },
            }),
        },
        { provide: TRANSLOCO_LOADER, useClass: TranslocoHttpLoader },
    ],
})
export class TranslocoRootModule {}
