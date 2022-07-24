import { animate, style, transition, trigger } from '@angular/animations';
import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
    selector: 'qtt-sign-up-form',
    templateUrl: './sign-up-form.component.html',
    styleUrls: ['./sign-up-form.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: [
        trigger('signUpFormTrigger', [transition(':enter', [style({ opacity: 0 }), animate('0.6s ease-in', style({ opacity: 1 }))])]),
    ],
})
export class SignUpFormComponent {
    @Input() subtitle!: string;

    @Output() completed = new EventEmitter();

    constructor() {}
}
