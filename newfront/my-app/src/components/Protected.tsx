import { observer } from "mobx-react-lite";
import { useContext } from "react";
import { Navigate } from "react-router-dom";
import { JsxElement } from "typescript";
import { Context } from "..";
interface IProtectedProps
{
    isLoggedIn:boolean;
    children:JSX.Element;
    role:string|null;
    AutorizedRole:string|null;
}
const Protected = observer((props:IProtectedProps) => {
 
 if (0) {
 return <Navigate to="/login" replace />;
 }
 if(0)
 {
    if(props.role===props.AutorizedRole)
    return props.children;
 }
 if(0)
 {
    return <Navigate to="/" replace />;
 }
 return props.children;
})
export default (Protected);