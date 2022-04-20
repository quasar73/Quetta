export interface ChatItem {
    id: string;
    title: string;
    lastMessage: string;
    type: 'dialog' | 'group' | 'channel';
}
