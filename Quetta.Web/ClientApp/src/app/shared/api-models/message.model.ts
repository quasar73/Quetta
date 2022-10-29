import { ReaderModel } from '@models/reader.model';

export interface MessageModel {
    id: string | null;
    text: string;
    username: string;
    date: string;
    isSupported: boolean;
    readers: ReaderModel[];
    isOwner: boolean;
}
