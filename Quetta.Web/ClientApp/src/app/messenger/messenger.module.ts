import { SidebarModule } from './sidebar/sidebar.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MessengerRoutingModule } from './messenger-routing.module';
import { TranslocoModule, TRANSLOCO_SCOPE } from '@ngneat/transloco';

import { EmptyChatComponent } from './empty-chat/empty-chat.component';
import { MessengerComponent } from './messenger.component';

@NgModule({
    providers: [{ provide: TRANSLOCO_SCOPE, useValue: 'messenger' }],
    declarations: [EmptyChatComponent, MessengerComponent],
    imports: [CommonModule, MessengerRoutingModule, TranslocoModule, SidebarModule],
})
export class MessengerModule {}
