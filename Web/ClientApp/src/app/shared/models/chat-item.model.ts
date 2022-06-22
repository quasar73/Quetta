export interface ChatItem {
    id: string;
    title: string;
    lastMessage: string;
    lastMessageDate: Date;
    type: 'dialog' | 'group' | 'channel';
}
