import { animate, style, transition, trigger } from '@angular/animations';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
    selector: 'qtt-sign-on-form',
    templateUrl: './sign-on-form.component.html',
    styleUrls: ['./sign-on-form.component.scss'],
    animations: [
        trigger('signOnFormTrigger', [
            transition(':enter', [style({ opacity: 0 }), animate('0.6s ease-in', style({ opacity: 1 }))]),
        ]),
    ],
})
export class SignOnFormComponent {
    @Input() title!: string;
    @Input() isFinish = false;
    @Input() disabled = false;

    @Output() completed = new EventEmitter();

    constructor() {}
}
