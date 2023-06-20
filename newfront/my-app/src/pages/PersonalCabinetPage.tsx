import { observer } from "mobx-react-lite";
import { useContext, useEffect, useState } from "react";
import { Context } from "..";
import PersonalCabinet from "../components/PersonalCabinet";
import IUser from "../models/IUser";
import UserService from "../services/UserService";
import Store from "../store/store";
interface IProps
{
  user:IUser
}
const RegistrationPage = (props:IProps) => {
  const [user,setUser]=useState<IUser>()
  useEffect(() => {
    const GetMe = async () => {
        setUser(await UserService.GetMe());
    };
    GetMe();
    
  }, []);
  console.log(props.user)
  return (
    <PersonalCabinet user1={props.user}   />
  );
};
export default observer(RegistrationPage);
