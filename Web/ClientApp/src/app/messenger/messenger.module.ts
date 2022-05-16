import { ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MessengerRoutingModule } from './messenger-routing.module';
import { MessengerComponent } from './messenger.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { TuiAvatarModule, TuiDataListWrapperModule, TuiInputModule, TuiSelectModule } from '@taiga-ui/kit';
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
    ],
})
export class MessengerModule {}
