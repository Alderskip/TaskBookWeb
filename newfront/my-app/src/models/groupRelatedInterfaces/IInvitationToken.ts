export default interface IInvitationToken{
    Id:number;
    invitationToken:string;
    userCreated:number;
    groupId:number;
    creationTime:Date;
    currentAcceptedPeople:number;
    maxPeople:number;
}