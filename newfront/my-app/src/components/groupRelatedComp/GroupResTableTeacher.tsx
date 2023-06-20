import { useEffect, useState } from "react";
import {
  Accordion,
  Col,
  Form,
  Row,
  InputGroup,
  Table,
  Button,
} from "react-bootstrap";
import IGroup from "../../models/groupRelatedInterfaces/IGroup";
import IUser from "../../models/IUser";
import IGroupResult from "../../models/groupRelatedInterfaces/IGroupResult";
import GroupService from "../../services/GroupService";
import UtilityService from "../../services/UtilityService";
import QuestionMarkTooltip from "../QuestionMarkTooltip";
interface IGroupResTableTProps {
  group: IGroup;
}
const GroupResTableT = (props: IGroupResTableTProps) => {
  const [viewResBy, setViewResBy] = useState<string>("students");
  const [viewOnlyNewRes, setViewOnlyNewRes] = useState<boolean>(true);
  const [viewOnlyNewSuccesfulRes, setviewOnlyNewSuccesfulRes] =
    useState<boolean>(true);
  const [groupRes, setGroupRes] = useState<IGroupResult[]>([]);
  //const [sortedGroupRes, setsortedGroupRes] = useState<JSX.Element>(<></>);

  useEffect(() => {
    const getGroupRes = async () => {
      var response = await GroupService.GetGroupResTeacher(props.group.Id);
      setGroupRes(response);
      //sortGroupGrades(response);
      return response;
    };
    getGroupRes();
  }, []);

  function changeViewResBy() {
    sortGroupGrades();
    viewResBy === "students" ? setViewResBy("time") : setViewResBy("students");
  }

  function changeViewOnlyNewRes() {
    sortGroupGrades();
    setViewOnlyNewRes(!viewOnlyNewRes);
  }

  function changeViewOnlyNewSuccesfulRes() {
    sortGroupGrades();
    setviewOnlyNewSuccesfulRes(!viewOnlyNewSuccesfulRes);
  }

  function GetStudents(groupResult: IGroupResult[] = groupRes) {
    let a: IUser[] = [];
    groupRes.forEach((x) => {
      if (!a.some((y) => y.Id == x.studentId)) {
        a.push(x.student);
      }
    });
    a.sort((a, b) => (a.lastName > b.lastName ? -1 : 1));
    return a;
  }

  async function GradeTask(res: IGroupResult) {
    if (res.resType != "Задание выполнено") {
      await GroupService.GradeTaskTeacher(res);
      var response = await GroupService.GetGroupResTeacher(props.group.Id);
      setGroupRes(response);
    } else {
      Number(
        (document.getElementById(`${res.Id}-pointvalue`) as HTMLInputElement)
          .value
      ) === null
        ? (res.pointGradeValue = res.groupTask.maxTaskPointValueInGroup)
        : (res.pointGradeValue = Number(
            (
              document.getElementById(
                `${res.Id}-pointvalue`
              ) as HTMLInputElement
            ).value
          ));

      await GroupService.GradeTaskTeacher(res);
      var response = await GroupService.GetGroupResTeacher(props.group.Id);
      setGroupRes(response);
    }
  }

  function sortGroupGrades(groupResult: IGroupResult[] = groupRes) {
    if (viewResBy === "students") {
      return (
        <>
          {GetStudents(groupResult).map((student) => (
            <Accordion>
              <Accordion.Item eventKey={student.Id.toString()}>
                <Accordion.Header>
                  <h1 className="fs-6 semi-b">
                    {student.lastName +
                      " " +
                      student.firstName +
                      " " +
                      student.secondOrFathersName}{" "}
                  </h1>
                </Accordion.Header>

                <Accordion.Body>
                  <Table className="text-break table-sm  align-middle">
                    <thead>
                      <tr>
                        <th>Задание</th>
                        <th>Тип задание</th>
                        <th>Тип результата</th>
                        <th>Время результата</th>
                        {props.group.gradeSys === "credit" ? (
                          <></>
                        ) : (
                          <th>Оценка</th>
                        )}
                      </tr>
                    </thead>

                    <tbody>
                      {groupResult
                        .filter(
                          (x) =>
                            x.studentId === student.Id &&
                            x.seen == !viewOnlyNewRes &&
                            (viewOnlyNewSuccesfulRes
                              ? x.resType === "Задание выполнено"
                              : true)
                        )
                        .map((res) => (
                          <tr>
                            <td>{res.groupTask.studyTask.taskName}</td>
                            <td>
                              {res.groupTask.isMandatory
                                ? "Обязательное"
                                : "Дополнительное"}
                            </td>
                            <td>{res.resType}</td>
                            <td>
                              {`${new Date(res.resTime).toLocaleDateString()} 
                                ${new Date(res.resTime).toLocaleTimeString([], {timeStyle: 'short'})} `}
                            </td>
                            <td>
                              {props.group.gradeSys === "credit" ? (
                                <></>
                              ) : res.resType === "Задание выполнено" ? (
                                <td>
                                  <Form.Control
                                    type="number"
                                    id={`${res.Id}-pointvalue`}
                                    placeholder={
                                      res.IsGraded != true
                                        ? `Максимум: ${res.groupTask.maxTaskPointValueInGroup.toString()}`
                                        : `Текущая оценка: ${res.pointGradeValue.toString()}`
                                    }
                                  />
                                </td>
                              ) : (
                                <>Оценка не выставляется</>
                              )}
                            </td>
                            <td>
                              <Button
                                variant="primary"
                                onClick={() => GradeTask(res)}
                              >
                                {res.resType === "Задание выполнено" ? (
                                  <>Оценить</>
                                ) : (
                                  <>Прочитано</>
                                )}
                              </Button>
                            </td>
                          </tr>
                        ))}
                    </tbody>
                  </Table>
                </Accordion.Body>
              </Accordion.Item>
            </Accordion>
          ))}
        </>
      );
    }

    if (viewResBy === "time") {
      return (
        <>
          <Table className="text-break table-sm  align-middle">
            <thead>
              <tr>
                <th>Студент</th>
                <th>Задание</th>
                <th>Тип Задания <QuestionMarkTooltip text={"Обязательные-влияет на макс оценку, необязательные - не влияют"}/></th>
                <th>Тип результата</th>
                <th>Время результата</th>
                {props.group.gradeSys === "credit" ? <></> : <th>Оценка</th>}
              </tr>
            </thead>
            <tbody>
              {groupResult
                .filter(
                  (x) =>
                    x.seen == !viewOnlyNewRes &&
                    (viewOnlyNewSuccesfulRes
                      ? x.resType === "Задание выполнено"
                      : true)
                )
                .sort((a, b) => (a.resTime > b.resTime ? -1 : 1))
                .map((res) => (
                  <tr>
                    <td>{`${res.student.firstName.at(
                      0
                    )}.${res.student.secondOrFathersName.at(0)}. ${
                      res.student.lastName
                    }`}</td>
                    <td>{res.groupTask.studyTask.taskName}</td>
                    <td>
                      {res.groupTask.isMandatory
                        ? "Обязательное"
                        : "Дополнительное"}
                    </td>
                    <td>{res.resType}</td>
                    <td>
                      {new Date(res.resTime)
                        .toLocaleString()
                        .slice(
                          0,
                          new Date(res.resTime).toLocaleString().length - 3
                        )}
                    </td>
                    <td>
                      {props.group.gradeSys === "credit" ? (
                        <></>
                      ) : res.resType === "Задание выполнено" ? (
                        <td>
                          <Form.Control
                            type="number"
                            id={`${res.Id}-pointvalue`}
                            placeholder={
                              res.IsGraded != true
                                ? `Максимум: ${res.groupTask.maxTaskPointValueInGroup.toString()}`
                                : `Текущая оценка: ${res.pointGradeValue.toString()}`
                            }
                          />
                        </td>
                      ) : (
                        <>Оценка не выставляется</>
                      )}
                    </td>
                    <td>
                      <Button variant="primary" onClick={() => GradeTask(res)}>
                        {res.resType === "Задание выполнено" ? (
                          <>Оценить</>
                        ) : (
                          <>Прочитано</>
                        )}
                      </Button>
                    </td>
                  </tr>
                ))}
            </tbody>
          </Table>
        </>
      );
    }
  }

  return (
    <Accordion defaultActiveKey={["1"]} alwaysOpen={true}>
      <Accordion.Item eventKey={"1"}>
        <Accordion.Header>
          <p className="fs-5 fw-semibold">Результаты группы</p>
        </Accordion.Header>

        <Accordion.Body>
          <Row>
            <Col>
              <InputGroup className="mb-3" size="sm">
                <InputGroup.Text>Сортировать результаты по:</InputGroup.Text>
                <Form.Select
                  aria-label="Default select"
                  onChange={changeViewResBy}
                >
                  <option value="students">Студентам</option>
                  <option value="time"> Времени</option>
                </Form.Select>
              </InputGroup>
            </Col>
            <Col>
              <InputGroup className="mb-3" size="sm">
                <InputGroup.Text>Только успешные результаты?</InputGroup.Text>
                <Form.Select
                  aria-label="Default select"
                  onChange={changeViewOnlyNewSuccesfulRes}
                >
                  <option value="1">Да</option>
                  <option value="0">Все результаты</option>
                </Form.Select>
              </InputGroup>
            </Col>
            <Col>
              <InputGroup className="mb-3" size="sm">
                <InputGroup.Text>Только новые?</InputGroup.Text>
                <Form.Select
                  aria-label="Default select"
                  onChange={changeViewOnlyNewRes}
                >
                  <option value="1">Да</option>
                  <option value="0">Уже виденные</option>
                </Form.Select>
              </InputGroup>
            </Col>
          </Row>
          <Row>{sortGroupGrades()}</Row>
        </Accordion.Body>
      </Accordion.Item>
    </Accordion>
  );
};

export default GroupResTableT;
