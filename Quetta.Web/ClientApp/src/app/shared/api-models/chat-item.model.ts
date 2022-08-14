import { ChatType } from '../enums/chat-type.enum';

export interface ChatItemModel {
    id: string;
    title: string;
    chatType: ChatType;
    lastMessage: string | null;
}
