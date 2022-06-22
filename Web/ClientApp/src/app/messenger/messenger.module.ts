import { ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MessengerRoutingModule } from './messenger-routing.module';
import { MessengerComponent } from './messenger.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import {
    TuiAvatarModule,
    TuiBadgeModule,
    TuiDataListWrapperModule,
    TuiInputModule,
    TuiSelectModule,
    TuiTextAreaModule,
} from '@taiga-ui/kit';
import { SidebarHeaderComponent } from './sidebar/sidebar-header/sidebar-header.component';
import {
    TuiButtonModule,
    TuiDataListModule,
    TuiHostedDropdownModule,
    TuiScrollbarModule,
    TuiSvgModule,
    TuiTextfieldControllerModule,
} from '@taiga-ui/core';
import { TuiSidebarModule } from '@taiga-ui/addon-mobile';
import { TranslocoModule, TRANSLOCO_SCOPE } from '@ngneat/transloco';
import { SidebarFooterComponent } from './sidebar/sidebar-footer/sidebar-footer.component';
import { SidebarContentComponent } from './sidebar/sidebar-content/sidebar-content.component';
import { SidebarItemComponent } from './sidebar/sidebar-content/sidebar-item/sidebar-item.component';
import { TuiActiveZoneModule } from '@taiga-ui/cdk';
import { ChatComponent } from './chat/chat.component';
import { EmptyChatComponent } from './empty-chat/empty-chat.component';
import { ChatHeaderComponent } from './chat/chat-header/chat-header.component';
import { ChatContentComponent } from './chat/chat-content/chat-content.component';
import { ChatInputComponent } from './chat/chat-input/chat-input.component';

@NgModule({
    providers: [{ provide: TRANSLOCO_SCOPE, useValue: 'messenger' }],
    declarations: [
        MessengerComponent,
        SidebarComponent,
        SidebarHeaderComponent,
        SidebarFooterComponent,
        SidebarContentComponent,
        SidebarItemComponent,
        ChatComponent,
        EmptyChatComponent,
        ChatHeaderComponent,
        ChatContentComponent,
        ChatInputComponent,
    ],
    imports: [
        CommonModule,
        MessengerRoutingModule,
        TranslocoModule,
        ReactiveFormsModule,
        TuiAvatarModule,
        TuiHostedDropdownModule,
        TuiDataListModule,
        TuiSvgModule,
        TuiSelectModule,
        TuiDataListWrapperModule,
        TuiTextfieldControllerModule,
        TuiInputModule,
        TuiScrollbarModule,
        TuiButtonModule,
        TuiActiveZoneModule,
        TuiSidebarModule,
        TuiBadgeModule,
        TuiTextAreaModule,
    ],
})
export class MessengerModule {}
