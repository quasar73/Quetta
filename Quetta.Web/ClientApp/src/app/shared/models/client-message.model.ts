import { MessageModel } from '@api-models/message.model';

export interface ClientMessageModel extends MessageModel {
    code: string | undefined;
    isSelected: boolean;
}
