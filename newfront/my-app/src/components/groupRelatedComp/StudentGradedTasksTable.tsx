import { group } from "console";
import { Accordion, Table } from "react-bootstrap";
import IGroup from "../../models/groupRelatedInterfaces/IGroup";
import IGroupResult from "../../models/groupRelatedInterfaces/IGroupResult";
import IUser from "../../models/IUser";
import UtilityService from "../../services/UtilityService";

interface IStudentGradedTasksTableProps {
  res: IGroupResult[];
  group: IGroup;
}
const StudentGradedTasksTable = (props: IStudentGradedTasksTableProps) => {
  return (
    <Accordion defaultActiveKey={["1"]} alwaysOpen={true}>
      <Accordion.Item eventKey={"1"}>
        <Accordion.Header>
          <p className="fs-5 fw-semibold">Оценки</p>
        </Accordion.Header>

        <Accordion.Body>
          <Table>
            <thead>
              <tr>
                <th>Задание</th>
                <th>Тип задание</th>
                <th>Время результата</th>
                <th>Оценка</th>
                <th>Максимальная оценка</th>
              </tr>
            </thead>
            <tbody>
              {props.res
                .filter((x) => x.resType === "Задание выполнено")
                .map((res) => (
                  <tr>
                    <td>{res.groupTask.studyTask.taskName}</td>
                    <td>
                      {res.groupTask.isMandatory
                        ? (<b>Обязательное</b>)
                        : "Дополнительное"}
                    </td>
                    <td>
                      {`${new Date(
                        res.resTime
                      ).getDate()} ${UtilityService.getMonthName(
                        new Date(res.resTime).getMonth()
                      )} 
                                 ${new Date(res.resTime).getHours()}:${new Date(
                        res.resTime
                      ).getMinutes()}
                                `}
                    </td>
                    <td>
                      {!res.IsGraded
                        ? "Не оценено"
                        : props.group.gradeSys === "credit"
                        ? "Зачтено"
                        : res.pointGradeValue}
                    </td>
                    <td>{res.groupTask.maxTaskPointValueInGroup}</td>
                  </tr>
                ))}
            </tbody>
          </Table>
        </Accordion.Body>
      </Accordion.Item>
    </Accordion>
  );
};
export default StudentGradedTasksTable;
