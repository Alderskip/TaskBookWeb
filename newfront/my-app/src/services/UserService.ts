import $api from "../http";
import IStudentRatingTableRow from "../models/IStudentRatingTable";
import IUser from "../models/IUser";
export default class UserService {
    static async  GetStudentRatingTable():Promise<IUser[]> {
        var response =await $api.get<IUser[]>(`User/StudentRaitingTable/`,{
            headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
          });
        return(response.data)
    }
    static async  GetStudentCount():Promise<number> {
        var response =await $api.get<number>("User/StudentCount",{
            headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
          });
        return(response.data)
    }
    static async GetMe():Promise<IUser>{
      var response =await $api.get<IUser>("User/GetMe/",{
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      });
    return(response.data)
    }
}