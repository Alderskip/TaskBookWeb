import { Accordion, Table } from "react-bootstrap";
import IGroupTask from "../../models/groupRelatedInterfaces/IGroupTask";

interface IStudentGroupTaskTableProps {
  tasks: IGroupTask[];
}
const StudentGroupTaskTable = (props: IStudentGroupTaskTableProps) => {
    return(
  <Accordion defaultActiveKey={["1"]} alwaysOpen={true}>
    <Accordion.Item eventKey={"1"}>
      <Accordion.Header>
        <p className="fs-5 fw-semibold">Задания</p>
      </Accordion.Header>

      <Accordion.Body>
        <Table>
          <thead>
            <tr>
              <th>Задание</th>
              <th>Описание задания</th>
              <th>Тип задание</th>
              <th>Автоматическое оценивание</th>
              <th>Максимальная оценка</th>
            </tr>
          </thead>
          <tbody>
            {props.tasks.map((task) => (
              <tr>
                <td>{task.studyTask.taskName}</td>
                <td>{task.studyTask.taskDesc}</td>
                <td>{task.isMandatory ? (<b>Обязательное</b>) : "Дополнительное"}</td>
                <td>{task.isAutoGraded ? "Да" : "Нет"}</td>

                <td>{task.maxTaskPointValueInGroup}</td>
              </tr>
            ))}
          </tbody>
        </Table>
      </Accordion.Body>
    </Accordion.Item>
  </Accordion>
    )
};
export default StudentGroupTaskTable;