import $api from "../http";
import ICourse from "../models/CourseRelatedInterfaces/ICourse";
import IStudyTask from "../models/studyTaskRelatedInterfaces/IStudyTask";

export default class CouseService {
    static async  AddUserToCouse(courseId:number,userId:number) {
        var response =await $api.post(`Course/AddStudentToCourse/`,{courseId:courseId,studentId:userId},{
            headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
          });
        return(response.data)
    }
    static async  GetCourses(env:string):Promise<ICourse[]> {
      console.log(env)
      if(env==="C++")
        env="C1"
      if(env==="C#")
        env="C2"
        console.log(env)
        var response =await $api.get<ICourse[]>(`Course?env=${env}`,{
            headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
          });
          console.log(response.data)
        return(response.data)
    }
    static async GetCourseTasksPag(courseId:number,currentPage:number):Promise<IStudyTask[]>
    {
      var response =await $api.get<IStudyTask[]>(`Course/GetCourseTasks?courseId=${courseId}&currentPage=${currentPage}`,{
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      });
      return(response.data);
    }
    static async GetUserCourses():Promise<ICourse[]>
    {
      var response =await $api.get<ICourse[]>(`Course/GetUserCourses`,{
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      });
      return(response.data);
    }


    static async GetCourseTasksFull(courseId:number):Promise<IStudyTask[]>
    {
      var response =await $api.get<IStudyTask[]>(`Course/GetCourseTasks?courseId=${courseId}`,{
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      });
      return(response.data);
    }
    static async GetCourseTaskCount(courseId:number):Promise<number>
    {
      var response =await $api.get<number>(`Course/GetCourseTaskCount?courseId=${courseId}`,{
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      });
      return(response.data);
    }

}