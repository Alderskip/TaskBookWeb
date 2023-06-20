import axios from "axios";
import { makeAutoObservable } from "mobx";
import React from "react";
import { API_URL } from "../http";
import IUser from "../models/IUser";
import AuthResponse from "../models/response/AuthResponse";
import AuthService from "../services/AuthService";

export default class Store {
  user = {} as IUser;
  isAuth = false;
  constructor() {
    makeAutoObservable(this);
  }
  setAuth(bool: boolean) {
    this.isAuth = bool;
  }
  setUser(user: IUser) {
    this.user = user;
  }
  getUser():IUser
  {
    
    return this.user
  }
  
  async login(username: string, password: string): Promise<boolean> {
    try {
      const response = await AuthService.login(username, password);
      if (response.status === 200) {
        localStorage.setItem("token", response.data.token);
        this.setAuth(true);
        console.log(this.isAuth);
        this.setUser(response.data.user);
        return true;
      } else return false;
    } catch (e) {
      console.log(e);
    }
    return false;
  }
  async registration(
    username: string,
    password: string,
    firstName: string,
    lastName: string,
    email: string,
    secondOrFathersName: string
  ): Promise<boolean> {
    try {
      const response = await AuthService.registration(
        username,
        password,
        firstName,
        lastName,
        email,
        secondOrFathersName
      );

      const response1 = await AuthService.login(username, password);
      localStorage.setItem("token", response1.data.token);
      this.setAuth(true);
      this.setUser(response1.data.user);
      if (response.status === 201 && response1.status === 200) return true;
    } catch (e) {
      console.log(e);
    }
    return false;
  }
  logout() {
    try {
      console.log("logout");
      localStorage.removeItem("token");
      this.setAuth(false);
      this.setUser({} as IUser);
    } catch (e) {
      console.log(e);
    }
  }
  async checkUsernameValid(username: string) {
    const response = await axios.post(
      `${API_URL}/checkUsernameValid`,
      { username: username },
      { withCredentials: true }
    );
    if (response.data === "valid") return true;
    else return false;
  }
  async ChangePassword(oldpassword: string, newpassword: string) {
    await axios.put(
      `${API_URL}/User/PAСhangeForPassword`,
      { oldpassword: oldpassword, password: newpassword },
      {
        withCredentials: true,
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      }
    );
  }
  async checkAuto() {
    try {
      const response = await axios.get<AuthResponse>(`${API_URL}/refresh`, {
        withCredentials: true,
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      });
      if (response.status !== 401) {
        localStorage.setItem("token", response.data.token);
        this.setAuth(true);
        this.setUser(response.data.user);
      } else this.logout();
    } catch (e) {
      console.log(e);
    }
  }
  public async ChackAuthStatus() : Promise<boolean>
  {
    try{
      const response = await axios.get<AuthResponse>(`${API_URL}/`, {
        withCredentials: true,
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
      });
      if(response.status===200)
      return true;
    }catch(e)
    {
      return false;
    }
    
    return false;
  }
  
}


