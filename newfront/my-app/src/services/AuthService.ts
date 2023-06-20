import $api from "../http";
import { AxiosResponse } from "axios";
import AuthResponse from "../models/response/AuthResponse";
export default class AuthService {
  static async login(
    username: string,
    password: string
  ): Promise<AxiosResponse<AuthResponse>> {
    return $api.post<AuthResponse>("/", { username, password });
  }
  static async registration(
    username: string,
    password: string,
    firstName: string,
    lastName: string,
    email: string,
    secondOrFathersName: string
  ): Promise<AxiosResponse> {
    return $api.post("/User/", {
      username: username,
      password: password,
      firstName: firstName,
      lastName: lastName,
      email: email,
      secondOrFathersName: secondOrFathersName,
    });
  }
  static async logout(): Promise<AxiosResponse> {
    return $api.post("/revoke/");
  }
}
