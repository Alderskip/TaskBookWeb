export default interface IGroup
{
    Id:number;
    groupName:string;
    groupDesc:string;
    groupEnv:string;
    gradeSys:string;
    lastUpdateTime:Date | null; 
}