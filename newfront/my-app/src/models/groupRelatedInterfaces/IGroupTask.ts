
import IStudyTask from "../studyTaskRelatedInterfaces/IStudyTask";
import IGroup from "./IGroup";

export default interface IGroupTask
{
    Id:number;
    groupId:number;
    group:IGroup;
    studyTaskId:number;
    studyTask:IStudyTask;
    maxTaskPointValueInGroup:number;
    isMandatory:boolean;
    active:boolean;
    isAutoGraded:boolean;
}