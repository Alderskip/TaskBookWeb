import { useEffect, useState } from "react";
import { Col } from "react-bootstrap";

import Row from "react-bootstrap/esm/Row";
import ICourse from "../../models/CourseRelatedInterfaces/ICourse";
import IStudyTask from "../../models/studyTaskRelatedInterfaces/IStudyTask";
import CourseService from "../../services/CourseService";
import TaskAnalysis from "../TaskAnalysis";

interface ICourseInfoProps {
  course: ICourse;
}
const CourseInfo = (props: ICourseInfoProps) => {
    const [studyTasks,setStudyTasks]= useState<IStudyTask[]>([])
    useEffect(() => {
        const GetCourseTasks = async () => {
          var response = await CourseService.GetCourseTasksFull(props.course.Id);
         
          setStudyTasks(response);
        };
        const GetCompletedTasks = async ()=>
        {

        }
        GetCourseTasks();
      }, []);
  return (
    <>
      <Row className="mb-2 mt-2">
        <Col>
          <h2>Название курса: <b>{props.course.courseName}</b></h2>
          <h3>Язык программирования: <b>{props.course.courseEnv}</b> </h3>
          <h3>Уровень сложности: <b>{props.course.courseDifficulty}</b> </h3>
        </Col>
      </Row>
      <Row>
        <h4>Описание курса:</h4>
        <p>{props.course.courseDesc}</p>  
      </Row>
      <Row>
        <TaskAnalysis studyTasks={studyTasks} completedTasks={[]}/>
      </Row>
      <Row>

      </Row>
    </>
  );
};
export default CourseInfo;
