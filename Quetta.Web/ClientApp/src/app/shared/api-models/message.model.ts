import { MessageStatus } from '../enums/message-status.enum';

export interface MessageModel {
    id: string | null;
    text: string;
    username: string;
    date: string;
    status: MessageStatus;
    isSupported: boolean;
}
