import { ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MessengerRoutingModule } from './messenger-routing.module';
import { MessengerComponent } from './messenger.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { TuiAvatarModule, TuiDataListWrapperModule, TuiInputModule, TuiSelectModule } from '@taiga-ui/kit';
import { SidebarHeaderComponent } from './sidebar/sidebar-header/sidebar-header.component';
import { TuiDataListModule, TuiHostedDropdownModule, TuiScrollbarModule, TuiSvgModule, TuiTextfieldControllerModule } from '@taiga-ui/core';
import { TranslocoModule, TRANSLOCO_SCOPE } from '@ngneat/transloco';
import { SidebarFooterComponent } from './sidebar/sidebar-footer/sidebar-footer.component';
import { SidebarContentComponent } from './sidebar/sidebar-content/sidebar-content.component';
import { SidebarItemComponent } from './sidebar/sidebar-content/sidebar-item/sidebar-item.component';

@NgModule({
    providers: [{ provide: TRANSLOCO_SCOPE, useValue: 'messenger' }],
    declarations: [
        MessengerComponent,
        SidebarComponent,
        SidebarHeaderComponent,
        SidebarFooterComponent,
        SidebarContentComponent,
        SidebarItemComponent,
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
    ],
})
export class MessengerModule {}
