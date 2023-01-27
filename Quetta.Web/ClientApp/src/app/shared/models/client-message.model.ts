import { MessageModel } from '@api-models/message.model';
import { MessageStatus } from '@enums/message-status.enum';

export interface ClientMessageModel extends MessageModel {
    isSelected: boolean;
    code: string | undefined;
    status: MessageStatus;
}
