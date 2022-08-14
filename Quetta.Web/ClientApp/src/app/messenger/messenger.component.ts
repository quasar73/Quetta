import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
    selector: 'qtt-messenger',
    templateUrl: './messenger.component.html',
    styleUrls: ['./messenger.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MessengerComponent {
    constructor() {}
}
