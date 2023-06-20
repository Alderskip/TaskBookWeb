import axios, { AxiosRequestConfig, } from "axios";

 



export const API_URL = "https://localhost:44365/api"

const $api=axios.create({
    
    baseURL:API_URL,
    withCredentials : true,
    
})

$api.interceptors.request.use(  (config:AxiosRequestConfig):AxiosRequestConfig=>{
    if (config.headers === undefined) {
        config.headers = {};
      }
    config.headers.Authorization = `Bearer ${localStorage.getItem("token")}`
    return config;
})
$api.interceptors.request.use(  (config:AxiosRequestConfig):AxiosRequestConfig=>{
    if (config.headers === undefined) {
        config.headers = {};
      }
    config.headers.Authorization = `Bearer ${localStorage.getItem("token")}`
    return config;
},(error)=>{
    console.log(Promise.reject(error))
    return Promise.reject(error);})



export default $api;