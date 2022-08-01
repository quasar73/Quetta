import { ChatComponent } from './chat/chat.component';
import { EmptyChatComponent } from './empty-chat/empty-chat.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MessengerComponent } from './messenger.component';
import { MessagesResolver } from '../shared/resolvers/messages/messages.resolver';

const routes: Routes = [
    {
        path: '',
        component: MessengerComponent,
        children: [
            { path: '', component: EmptyChatComponent },
            { path: ':id', component: ChatComponent, resolve: { messages: MessagesResolver } },
            { path: '**', redirectTo: '' },
        ],
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class MessengerRoutingModule {}