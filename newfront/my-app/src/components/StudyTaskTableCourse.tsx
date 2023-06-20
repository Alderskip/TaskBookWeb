import { useState, useEffect } from "react";
import { Accordion, Pagination, Row, Table, Col } from "react-bootstrap";
import IStudyTask from "../models/studyTaskRelatedInterfaces/IStudyTask";
import CourseService from "../services/CourseService";
interface props {
  courseId: number;
}
const StudyTaskTableCourse: React.FC<props> = (props) => {
  const [taskTableCourse, setTaskTableCourse] = useState<IStudyTask[]>();
  const [taskCount, setTaskCount] = useState<number>(0);
  const [currentPageItem, setcurrentPageItem] = useState<number>(1);

  useEffect(() => {
    const GetTable = async () => {
      setTaskTableCourse(
        await CourseService.GetCourseTasksFull(props.courseId)
      );
    };
    const GetTaskCount = async () => {
      setTaskCount(await CourseService.GetCourseTaskCount(props.courseId));
    };
    GetTable();
    GetTaskCount();
  }, []);
  async function PaginationItemClick(
    event: React.MouseEvent<HTMLButtonElement>
  ) {
    setcurrentPageItem(Number(event.currentTarget.textContent));
    setTaskTableCourse(
      await CourseService.GetCourseTasksPag(
        props.courseId,
        Number(event.currentTarget.textContent)
      )
    );
    console.log(event.target);
  }
  const CreatePagination = () => {
    let items = [];
    for (var i = 1; i <= taskCount / 5 + 1; i++) {
      items.push(
        <Pagination.Item
          key={i}
          active={i === currentPageItem}
          onClick={PaginationItemClick}
        >
          {i}
        </Pagination.Item>
      );
    }
    return items;
  };
  return (
    <>
      <Row>
        <Accordion>
          {taskTableCourse?.map((row) => (
            <Col>
              <Accordion.Item eventKey={row.taskName}>
                <Accordion.Header>{row.taskName}</Accordion.Header>
                <Accordion.Body>
                  {row.taskDesc}
                  {"\n"} Кол-во баллов за задание{" "}
                  {row.taskPointValue.toString()}
                </Accordion.Body>
              </Accordion.Item>
            </Col>
          ))}
        </Accordion>
      </Row>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>Задание</th>
            <th>Описание задания</th>
            <th>Кол-во очков за выполнения задания</th>
          </tr>
        </thead>
        <tbody>
          {taskTableCourse?.map((row) => (
            <tr>
              <td>{row.taskName}</td>
              <td>{row.taskDesc}</td>
              <td>{row.taskPointValue.toString()}</td>
            </tr>
          ))}
        </tbody>
      </Table>
      <Pagination>{CreatePagination().map((row) => row)}</Pagination>
    </>
  );
};

export default StudyTaskTableCourse;
/*
<Table striped bordered hover>
<thead>
  <tr>
    <th></th>
    <th>Задание</th>
    <th>Описание задания</th>
    <th>Кол-во очков за выполнения задания</th>
  </tr>
</thead>
<tbody>
  {taskTableCourse?.map((row) => (
    <tr>
      <td></td>
      <td>{row.task_name}</td>
      <td>{row.task_description}</td>
      <td>{row.taskPointValue.toString()}</td>
    </tr>
  ))}
</tbody>
</Table>
<Pagination>{CreatePagination().map((row) => row)}</Pagination>
*/
