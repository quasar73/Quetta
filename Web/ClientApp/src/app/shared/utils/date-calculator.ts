import { TranslocoService } from '@ngneat/transloco';
import dateFormat from 'dateformat';

export function dateCalculator(date: Date, translocoService: TranslocoService): string {
    const now = new Date();
    const diff = now.getTime() - date.getTime();
    const diffMinutes = Math.round(diff / (1000 * 60));
    const diffHours = Math.round(diff / (1000 * 60 * 60));

    if (diffMinutes < 1) {
        return translocoService.translate('messenger.sidebar.items.date.justNow');
    } else if (diffHours < 23) {
        return dateFormat(date, 'H:MM');
    } else {
        return dateFormat(date, 'dd.mm.yyyy');
    }
}
