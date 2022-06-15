import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { TuiDialogContext } from '@taiga-ui/core';
import { TUI_VALIDATION_ERRORS } from '@taiga-ui/kit';
import { POLYMORPHEUS_CONTEXT } from '@tinkoff/ng-polymorpheus';
import { usernameMinLength } from 'src/app/shared/consts/sign-on.const';

@Component({
    selector: 'qtt-add-chat-dialog',
    templateUrl: './add-chat-dialog.component.html',
    styleUrls: ['./add-chat-dialog.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
    providers: [
        {
            provide: TUI_VALIDATION_ERRORS,
            useValue: {
                required: 'Enter this!',
                minlength: `Too short! Min length is ${usernameMinLength}!`,
            },
        },
    ],
})
export class AddChatDialogComponent {
    usernameControl = new FormControl('', [Validators.required, Validators.minLength(3)]);

    constructor(
        @Inject(POLYMORPHEUS_CONTEXT)
        private readonly context: TuiDialogContext<string | null>
    ) {}

    invite(): void {
        if (this.usernameControl.valid) {
            this.context.completeWith(this.usernameControl.value);
        }
    }

    cancel(): void {
        this.context.completeWith(null);
    }
}
