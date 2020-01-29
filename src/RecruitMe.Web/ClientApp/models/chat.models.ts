export interface IMessage {
    isMine: boolean,
    message: string,
    timestamp: Date
}
export interface IUserThread {
    userId: number,
    displayName: string,
    newMessagesCount: number
}