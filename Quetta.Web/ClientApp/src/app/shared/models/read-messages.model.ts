import { ReaderModel } from '@models/reader.model';

export interface ReadMessagesModel {
    messageIds: string[];
    reader: ReaderModel;
}
