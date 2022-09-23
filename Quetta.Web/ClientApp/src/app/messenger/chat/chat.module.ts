import { ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TuiActiveZoneModule } from '@taiga-ui/cdk';
import { TuiCheckboxModule, TuiBadgeModule, TuiTextAreaModule, TuiMarkerIconModule, TuiDropdownContextModule } from '@taiga-ui/kit';
import { TuiScrollbarModule, TuiSvgModule, TuiDataListModule, TuiAlertModule, TuiRootModule, TuiButtonModule } from '@taiga-ui/core';
import { ChatRoutingModule } from './chat-routing.module';
import { ClipboardModule } from 'ngx-clipboard';
import { TranslocoModule } from '@ngneat/transloco';

import { ChatContentComponent } from './chat-content/chat-content.component';
import { NoteComponent } from './chat-content/note/note.component';
import { ChatHeaderComponent } from './chat-header/chat-header.component';
import { ChatInputComponent } from './chat-input/chat-input.component';
import { ChatComponent } from './chat.component';

import { SkeletonDirective } from 'src/app/shared/directives/skeleton.directive';

@NgModule({
    declarations: [ChatComponent, ChatHeaderComponent, ChatContentComponent, ChatInputComponent, NoteComponent, SkeletonDirective],
    imports: [
        CommonModule,
        ChatRoutingModule,
        ClipboardModule,
        TuiScrollbarModule,
        TuiCheckboxModule,
        TuiBadgeModule,
        TuiSvgModule,
        TuiDataListModule,
        ReactiveFormsModule,
        TuiTextAreaModule,
        TuiMarkerIconModule,
        TuiActiveZoneModule,
        TuiAlertModule,
        TuiDropdownContextModule,
        TuiRootModule,
        TuiButtonModule,
        TranslocoModule,
    ],
})
export class ChatModule {}
