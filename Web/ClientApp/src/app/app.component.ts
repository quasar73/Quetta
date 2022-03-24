import { Component } from '@angular/core';

@Component({
    selector: 'qtt-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
})
export class AppComponent {
    title = 'ClientApp';

    hello(): void {}
}
