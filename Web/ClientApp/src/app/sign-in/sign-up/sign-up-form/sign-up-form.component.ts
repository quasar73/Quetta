import { animate, style, transition, trigger } from '@angular/animations';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
    selector: 'qtt-sign-up-form',
    templateUrl: './sign-up-form.component.html',
    styleUrls: ['./sign-up-form.component.scss'],
    animations: [
        trigger('signUpFormTrigger', [transition(':enter', [style({ opacity: 0 }), animate('0.6s ease-in', style({ opacity: 1 }))])]),
    ],
})
export class SignUpFormComponent {
    @Input() title!: string;

    @Output() completed = new EventEmitter();

    constructor() {}
}
