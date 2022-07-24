import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

@Component({
    selector: 'qtt-note',
    templateUrl: './note.component.html',
    styleUrls: ['./note.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class NoteComponent implements OnInit {
    constructor() {}

    ngOnInit(): void {}
}
