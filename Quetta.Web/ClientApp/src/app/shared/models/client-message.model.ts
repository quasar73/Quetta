import { MessageModel } from '@api-models/message.model';

export interface ClientMessageModel extends MessageModel {
    isSelected: boolean;
    code: string | undefined;
}
