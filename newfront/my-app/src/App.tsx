import { useContext, useEffect, useState } from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { LoginPage } from "./pages/LoginPage";
import { HomePage } from "./pages/HomePage";
//import { Footer } from "./components/Footer";
import { RegistrationPage } from "./pages/RegistrationPage";
import { Context } from ".";
import NavbarSite from "./components/Navbar";
import { observer } from "mobx-react-lite";
import PersonalCabinetPage from "./pages/PersonalCabinetPage";
import { ErrorPage } from "./pages/ErrorPage";
import { GettingStartedPage } from "./pages/GettingStartedPage";
import { CoursePage } from "./pages/courseRelatedPages/CoursePage";
import { GroupPage } from "./pages/groupRelatedPages/GroupPage";
import { AboutCoursePage } from "./pages/courseRelatedPages/AboutCoursePage";
import { GettingStartedPageTeacher } from "./pages/GettingStartedPageTeacher";
import AddTaskToGroupPage from "./pages/groupRelatedPages/AddTaskToGroupPage";

import { toJS } from "mobx";
import { Footer } from "./components/Footer";
import Protected from "./components/Protected";
import EnviromentCourseCard from "./components/coursesRelatedComp/EnviromentCourseCard";
import ChooseCourseEnviromentPage from "./pages/courseRelatedPages/ChooseCourseEnviromentPage";
import CouseService from "./services/CourseService";
import MyCoursesPage from "./pages/courseRelatedPages/MyCoursesPage";
import IUser from "./models/IUser";
import UserService from "./services/UserService";
import TaskAnalysis from "./components/TaskAnalysis";

function App() {
  const { store } = useContext(Context);
  const [user,setUser]=useState<IUser>({Id:0,username:"",secondOrFathersName:"",lastName:"",lastVisit:new Date(2),totalStudyTaskPoints:0,email:"",role:"",firstName:"",gender:"Undefined"});
  useEffect(() => {

    async function GetMe() {
      setUser(await UserService.GetMe())
    }
    if (localStorage.getItem("token")) {
      store.checkAuto();
    }
    GetMe()
  }, []);
  console.log(store.isAuth)
  return (
    <BrowserRouter>
      <NavbarSite store={store} />

      <main className="flex-shrink-0">
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/registration" element={<RegistrationPage />} />
          <Route
            path="/personalCabinet"
            element={
              <Protected
                role={null}
                AutorizedRole={null}
                isLoggedIn={toJS(store.isAuth)}
              >
                <PersonalCabinetPage user={user}  />
              </Protected>
            }
          />
          <Route path="/error" element={<ErrorPage />} />
          <Route path="/FAQ" element={<GettingStartedPage />} />
          <Route path="/courses" element={<CoursePage/>} />
          <Route
            path="/myCourses"
            element={
              <Protected
                role={null}
                AutorizedRole={null}
                isLoggedIn={toJS(store.isAuth)}
              >
                <MyCoursesPage />
              </Protected>
            }
          />
          <Route path="/chooseCourseEnv" element={<ChooseCourseEnviromentPage />} />
          <Route
            path="/groups"
            element={
              <Protected
                role={null}
                AutorizedRole={null}
                isLoggedIn={toJS(store.isAuth)}
              >
                <GroupPage user={toJS(store.user)} />
              </Protected>
            }
          />
          <Route path="/courseInfo" element={<AboutCoursePage />} />
          <Route path="/FAQT" element={<GettingStartedPageTeacher />} />
          <Route
            path="/taskManager"
            element={
              <Protected
                role={toJS(store.user.role)}
                AutorizedRole={"Teacher"}
                isLoggedIn={toJS(store.isAuth)}
              >
                <AddTaskToGroupPage />
              </Protected>
            }
          />
          <Route path="/Test" element={<TaskAnalysis studyTasks={[]} completedTasks={[]} />} />
        </Routes>
      </main>

      <Footer />
    </BrowserRouter>
  );
}

export default observer(App);
