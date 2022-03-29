import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
    selector: 'qtt-sign-on-form',
    templateUrl: './sign-on-form.component.html',
    styleUrls: ['./sign-on-form.component.scss'],
})
export class SignOnFormComponent {
    @Input() title!: string;

    @Output() completed = new EventEmitter();

    constructor() {}
}
