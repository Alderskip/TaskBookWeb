import $api from "../http";
import IGroup from "../models/groupRelatedInterfaces/IGroup";
import IGroupTableRowStudent from "../models/IGroupTableRowStudent";
import IInvitationToken from "../models/groupRelatedInterfaces/IInvitationToken";
import IUser from "../models/IUser";
import IGroupTask from "../models/groupRelatedInterfaces/IGroupTask";
import IStudyTaskGroup from "../models/studyTaskRelatedInterfaces/IStudyTaskGroup";
import IGroupResult from "../models/groupRelatedInterfaces/IGroupResult";
import { AxiosResponse } from "axios";

export default class GroupService {
  static async GetGroupTableStudent(
    curentPage: number
  ): Promise<IGroupTableRowStudent[]> {
    var response = await $api.get<IGroupTableRowStudent[]>(
      `Group/GetGroupsStudent1/?currentPage=${curentPage.toString()}`,
      {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      }
    );
    return response.data;
  }

  static async GetGroupCount(): Promise<number> {
    var response = await $api.get<number>(`Group/GetGroupCount`, {
      headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
    });
    return response.data;
  }

  static async AddByTokenS(token: string): Promise<AxiosResponse> {
    var response = await $api.post(`Group/AddByTokenS/?token=${token}`, {
      headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
    });
    return response;
  }

  static async GetGroupTasks(
    groupId: number,
    currentPage: number = 1
  ): Promise<IGroupTask[]> {
    var response = await $api.get<IGroupTask[]>(
      `Group/GetGroupTasks?groupId=${groupId}`,
      {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      }
    );
    return response.data;
  }

  static async GetGroupTaskCount(groupId: number): Promise<number> {
    var response = await $api.get<number>(
      `Group/GetGroupTaskCount?groupId=${groupId}`,
      {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      }
    );
    return response.data;
  }

  static async GetGroupTableTeacher(
    curentPage: number
  ): Promise<IGroupTableRowStudent[]> {
    var response = await $api.get<IGroupTableRowStudent[]>(
      `Group/GetGroupsTeacher?currentPage=${curentPage.toString()}`,
      {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      }
    );
    return response.data;
  }

  static async GetGroupCountTeacher(): Promise<number> {
    var response = await $api.get<number>(`Group/GetGroupCountTeacher`, {
      headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
    });
    return response.data;
  }

  static async GetGroupStudentsTeacher(groupId: number): Promise<IUser[]> {
    var response = await $api.get<IUser[]>(
      `Group/GetGroupStudentsTeacher?groupId=${groupId.toString()}`,
      {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      }
    );
    return response.data;
  }

  static async CreateGroup(group: IGroup) {
    console.log(group);
    var response = await $api.post(
      `Group/CreateGroupTeacher`,
      {
        groupName: group.groupName,
        groupDesc: group.groupDesc,
        groupEnv: group.groupEnv,
        gradeSys: group.gradeSys,
      },
      {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      }
    );
    return response.data;
  }

  static async GetGroupsTeacher(): Promise<IGroup[]> {
    var response = await $api.get<IGroup[]>(`Group/GetGroupsTeacher`, {
      headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
    });
    return response.data;
  }

  static async GetGroupInvitationToken(
    groupId: number
  ): Promise<IInvitationToken> {
    var response = await $api.get<IInvitationToken>(
      `Group/GetGroupInvitationTokenTeacher?groupId=${groupId}`,
      {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      }
    );
    return response.data;
  }

  static async GetGroupTeacher(groupId: number): Promise<IGroup> {
    var response = await $api.get<IGroup>(
      `Group/GetGroupTeacher?groupId=${groupId}`,
      {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      }
    );
    return response.data;
  }

  static async AddStudentToGroupByUsernameTeacher(
    groupId: number,
    username: string
  ) {
    var response = await $api.post(
      `Group/AddStudentToGroupByUsernameTeacher`,
      {
        groupId: groupId,
        user: {
          username: username,
          email: "email@email.com",
          password: "password",
        },
      },
      {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      }
    );
    if (response.status === 200) return true;
    else return false;
  }

  static async GetStudyTaskGroups(): Promise<IStudyTaskGroup[]> {
    var response = await $api.get<IStudyTaskGroup[]>(
      `Group/GetStudyTaskGroups`
    );
    return response.data;
  }

  static async GetGroupTasksAddingModel(
    groupId: number,
    studyTaskGroupId: number
  ): Promise<IGroupTask[]> {
    var response = await $api.get<IGroupTask[]>(
      `Group/GetGroupTasksAddingModel?groupId=${groupId}&studyTaskGroupId=${studyTaskGroupId}`,
      {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      }
    );
    return response.data;
  }

  static async AddTaskToGroupByTaskAddingModel(
    studyTaskId: number,
    isMandatory: boolean,
    isAutoGraded: boolean,
    maxTaskPointValueInGroup: number,
    groupId: number
  ) {
    var response = await $api.post(
      `Group/AddTaskToGroupTeacher`,
      {
        studyTaskId: studyTaskId,
        isMandatory: isMandatory,
        isAutoGraded: isAutoGraded,
        groupId: groupId,
        maxTaskPointValueInGroup: maxTaskPointValueInGroup,
      },
      {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      }
    );
    return response.data;
  }

  static async ChangeGroupTaskParams(task: IGroupTask) {
    var response = await $api.put(
      `Group/ChangeGroupTaskParams`,
      {
        Id: task.Id,
        isMandatory: task.isMandatory,
        isAutoGraded: task.isAutoGraded,
        maxTaskPointValueInGroup: task.maxTaskPointValueInGroup,
      },
      {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      }
    );
    return response.data;
  }

  static async GetGroupResTeacher(groupId: number): Promise<IGroupResult[]> {
    var response = await $api.get<IGroupResult[]>(
      `Group/GetGroupResTeacher?groupId=${groupId}`,
      {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      }
    );
    return response.data;
  }

  static async GradeTaskTeacher(result: IGroupResult) {
    var response = await $api.put(
      `Group/GradeTaskTeacher`,
      {
        Id: result.Id,
        isGraded: result.IsGraded,
        seen: result.seen,
        pointGradeValue: result.pointGradeValue,
      },
      {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      }
    );
    return response.data;
  }

  static async DowlandAccessDatStudent(groupId: number) {
    var response = await $api.get(
      `Group/DowlandAccessDatStudent?groupId=${groupId}`,
      {
        responseType: 'arraybuffer',
        
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      }
    );
  
    return response.data;
  }

  static async UpdateGroupResults(groupId: number) {
    var response = await $api.post(
      `Group/UpdateGroupResults`,
      {
        Id: groupId,
      },
      {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      }
    );
    return response.data;
  }

  static async GetStudentResults(
    userId: number,
    groupId: number
  ): Promise<IGroupResult[]> {
    var response = await $api.get(
      `Group/GetStudentResults?groupId=${groupId}&userId=${userId}`,
      {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      }
    );
    return response.data;
  }

  static async RemoveFromGroupTeacher(studentId: number, groupId: number) {
    console.log(studentId)
    var response = await $api.put(
      
      `Group/RemoveFromGroupTeacher`,
      {
        userId: studentId,
        groupId: groupId,
      },
      {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      }
    );
    return response.data;
  }

  static async GetGroupsStudent(): Promise<IGroup[]> {
    var response = await $api.get<IGroup[]>(`Group/GetGroupsStudent/`, {
      headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
    });
    return response.data;
  }

  static async DowlandInitialResultFile(groupId: number)  {
    var response = await $api.get(
      `Group/DowlandInitialResultFile?groupId=${groupId}`,
      {
        responseType: 'arraybuffer',
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      }
    );
    return response.data;
  }
}
