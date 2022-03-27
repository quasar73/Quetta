import { TranslocoTestingModule, TranslocoTestingOptions } from '@ngneat/transloco';
import * as en from '../../assets/i18n/en.json';
import * as signIn from '../../assets/i18n/sign-in/en.json';

export function getTranslocoModule(options: TranslocoTestingOptions = {}) {
    const { langs, translocoConfig, ...rest } = options;
    return TranslocoTestingModule.forRoot({
        langs: { en, 'sign-in/en': signIn, ...langs },
        translocoConfig: {
            availableLangs: ['en'],
            defaultLang: 'en',
            ...translocoConfig,
        },
        preloadLangs: true,
        ...rest,
    });
}
