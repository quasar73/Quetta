import { TranslocoModule } from '@ngneat/transloco';
import {
    TuiMarkerIconModule,
    TuiInputModule,
    TuiAvatarModule,
    TuiSelectModule,
    TuiDataListWrapperModule,
    TuiIslandModule,
    TuiFieldErrorPipeModule,
} from '@taiga-ui/kit';
import {
    TuiScrollbarModule,
    TuiHostedDropdownModule,
    TuiDataListModule,
    TuiSvgModule,
    TuiHintModule,
    TuiButtonModule,
    TuiTextfieldControllerModule,
    TuiErrorModule,
} from '@taiga-ui/core';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { TuiActiveZoneModule } from '@taiga-ui/cdk';
import { TuiSidebarModule } from '@taiga-ui/addon-mobile';

import { SidebarContentComponent } from './sidebar-content/sidebar-content.component';
import { SidebarItemComponent } from './sidebar-content/sidebar-item/sidebar-item.component';
import { SidebarFooterComponent } from './sidebar-footer/sidebar-footer.component';
import { SidebarHeaderComponent } from './sidebar-header/sidebar-header.component';
import { SidebarComponent } from './sidebar.component';
import { AddChatDialogComponent } from './sidebar-header/add-chat-dialog/add-chat-dialog.component';
import { InviteItemComponent } from './sidebar-header/invites-list-dialog/invite-item/invite-item.component';
import { InvitesListDialogComponent } from './sidebar-header/invites-list-dialog/invites-list-dialog.component';

@NgModule({
    declarations: [
        SidebarComponent,
        SidebarHeaderComponent,
        SidebarFooterComponent,
        SidebarContentComponent,
        SidebarItemComponent,
        InvitesListDialogComponent,
        InviteItemComponent,
        AddChatDialogComponent,
    ],
    exports: [
        SidebarComponent,
        SidebarHeaderComponent,
        SidebarFooterComponent,
        SidebarContentComponent,
        SidebarItemComponent,
        InvitesListDialogComponent,
        InviteItemComponent,
        AddChatDialogComponent,
    ],
    imports: [
        CommonModule,
        TuiScrollbarModule,
        TuiHostedDropdownModule,
        TuiDataListModule,
        TuiMarkerIconModule,
        TuiInputModule,
        TuiAvatarModule,
        TuiSelectModule,
        TuiSvgModule,
        TuiDataListWrapperModule,
        TuiHintModule,
        TuiActiveZoneModule,
        TuiSidebarModule,
        TuiButtonModule,
        TuiTextfieldControllerModule,
        TuiIslandModule,
        TuiErrorModule,
        TuiFieldErrorPipeModule,
        TranslocoModule,
        ReactiveFormsModule,
    ],
})
export class SidebarModule {}
