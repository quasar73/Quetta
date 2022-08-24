import { ChatType } from 'src/app/shared/enums/chat-type.enum';
import { ChatItemModel } from './../../shared/api-models/chat-item.model';

export const testChatItem: ChatItemModel = {
    id: 'some-id',
    title: 'Test chat',
    chatType: ChatType.PersonalChat,
    lastMessage: 'This is last message',
};
