import { MessageStatus } from '../enums/message-status.enum';

export interface MessageModel {
    text: string;
    username: string;
    date: string;
    status: MessageStatus;
}
