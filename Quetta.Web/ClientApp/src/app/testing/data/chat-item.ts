import { ChatType } from '@enums/chat-type.enum';
import { ChatItemModel } from '@api-models/chat-item.model';

export const testChatItem: ChatItemModel = {
    id: 'some-id',
    title: 'Test chat',
    chatType: ChatType.PersonalChat,
    lastMessage: 'This is last message',
};
