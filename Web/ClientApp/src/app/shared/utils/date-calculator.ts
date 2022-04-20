import { TranslocoService } from '@ngneat/transloco';
export function dateCalculator(date: Date, translocoService: TranslocoService): string {
    const now = new Date();
    const diff = now.getTime() - date.getTime();
    const diffMinutes = Math.round(diff / (1000 * 60));
    const diffHours = Math.round(diff / (1000 * 60 * 60));
    const diffDays = Math.round(diff / (1000 * 60 * 60 * 24));
    const diffWeeks = Math.round(diff / (1000 * 60 * 60 * 24 * 7));
    const diffMonths = Math.round(diff / (1000 * 60 * 60 * 24 * 30));
    const diffYears = Math.round(diff / (1000 * 60 * 60 * 24 * 365));

    if (diffMinutes === 0) {
        return translocoService.translate('messenger.sidebar.items.date.justNow');
    } else if (diffMinutes < 60) {
        return diffMinutes === 1
            ? translocoService.translate('messenger.sidebar.items.date.minute')
            : translocoService.translate('messenger.sidebar.items.date.minutes', { minutes: diffMinutes });
    } else if (diffHours < 24) {
        return diffHours === 1
            ? translocoService.translate('messenger.sidebar.items.date.hour')
            : translocoService.translate('messenger.sidebar.items.date.hours', { hours: diffHours });
    } else if (diffDays < 7) {
        return diffDays === 1
            ? translocoService.translate('messenger.sidebar.items.date.day')
            : translocoService.translate('messenger.sidebar.items.date.days', { days: diffDays });
    } else if (diffWeeks < 4) {
        return diffWeeks === 1
            ? translocoService.translate('messenger.sidebar.items.date.week')
            : translocoService.translate('messenger.sidebar.items.date.weeks', { weeks: diffWeeks });
    } else if (diffMonths < 12) {
        return diffMonths === 1
            ? translocoService.translate('messenger.sidebar.items.date.month')
            : translocoService.translate('messenger.sidebar.items.date.months', { months: diffMonths });
    } else {
        return diffYears === 1
            ? translocoService.translate('messenger.sidebar.items.date.year')
            : translocoService.translate('messenger.sidebar.items.date.years', { years: diffYears });
    }
}
