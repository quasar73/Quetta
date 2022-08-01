import { MessageModel } from 'src/app/shared/api-models/message.model';

export interface ClientMessageModel extends MessageModel {
    code: string | undefined;
    isSelected: boolean;
}
