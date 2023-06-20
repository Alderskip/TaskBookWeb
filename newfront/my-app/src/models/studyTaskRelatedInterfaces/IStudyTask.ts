import IStudyTaskGroup from "./IStudyTaskGroup";

export default interface IStudyTask {
  Id:number;
  taskName: string;
  taskDesc: string;
  taskPointValue: number;
  studyTaskGroup:IStudyTaskGroup;
  studyTaskType:string;
  taskDifficulty:string;
}
