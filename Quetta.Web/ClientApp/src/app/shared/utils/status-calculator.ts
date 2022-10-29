import { MessageStatus } from '@enums/message-status.enum';
import { MessageModel } from '@api-models/message.model';

export function getStatus(message: MessageModel): MessageStatus {
    if (message.readers?.length) {
        return MessageStatus.Read;
    } else {
        return MessageStatus.Unread;
    }
}
