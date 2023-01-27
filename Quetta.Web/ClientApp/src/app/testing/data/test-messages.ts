import { MessageStatus } from '@enums/message-status.enum';
import { ClientMessageModel } from '@models/client-message.model';

export const testMessages: ClientMessageModel[] = [
    {
        text: 'message one',
        username: 'username',
        date: '',
        status: MessageStatus.Readed,
        code: '1',
        isSelected: false,
    },
    {
        text: 'message two',
        username: 'username2',
        date: '',
        status: MessageStatus.Readed,
        code: '2',
        isSelected: false,
    },
    {
        text: 'message three',
        username: 'username2',
        date: '',
        status: MessageStatus.Readed,
        code: '3',
        isSelected: false,
    },
    {
        text: 'message four',
        username: 'username',
        date: '',
        status: MessageStatus.Unreaded,
        code: '4',
        isSelected: false,
    },
];
