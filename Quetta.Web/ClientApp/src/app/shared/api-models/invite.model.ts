export interface InviteModel {
    senderUsername: string;
    inviteId: string;
    chatId: string | null;
    isGroupChat: boolean;
    dateTime: Date;
}
