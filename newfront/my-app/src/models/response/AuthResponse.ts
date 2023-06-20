import IUser from "../IUser";

export default interface AuthResponse {
  token: string;
  refreshToken: string;
  user: IUser;
}
