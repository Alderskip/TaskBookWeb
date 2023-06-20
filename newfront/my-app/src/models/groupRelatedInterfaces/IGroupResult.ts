import IUser from "../IUser";
import IGroup from "./IGroup";
import IGroupTask from "./IGroupTask";

export default interface IGroupResult
{
    Id:number;
    studentId:number;
    student:IUser;
    groupId:number;
    group:IGroup;
    groupTaskId:number;
    groupTask:IGroupTask;
    IsGraded:boolean;
    pointGradeValue:number;
    seen:boolean;
    resTime:Date;
    resType:string;
}