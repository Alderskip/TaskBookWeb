import { useState, useEffect } from "react";
import { Accordion, Table, Col, Form, Tabs, Tab } from "react-bootstrap";
import IGroupTask from "../../models/groupRelatedInterfaces/IGroupTask";
import GroupService from "../../services/GroupService";
interface StudyTaskTableTeacherProps {
  groupId: number;
}

const StudyTaskTableTeacher = (props: StudyTaskTableTeacherProps) => {
  const [taskTableGroup, setTaskTableGroup] = useState<IGroupTask[]>();
  const [taskCount, setTaskCount] = useState<number>(0);
  const [currentPageItem, setcurrentPageItem] = useState<number>(1);

  useEffect(() => {
    const GetTable = async () => {
      setTaskTableGroup(await GroupService.GetGroupTasks(props.groupId, 1));
    };
    const GetTaskCount = async () => {
      setTaskCount(await GroupService.GetGroupTaskCount(props.groupId));
    };
    GetTable();
    GetTaskCount();
  }, []);

  async function PaginationItemClick(
    event: React.MouseEvent<HTMLButtonElement>
  ) {
    setcurrentPageItem(Number(event.currentTarget.textContent));
    setTaskTableGroup(
      await GroupService.GetGroupTasks(
        props.groupId,
        Number(event.currentTarget.textContent)
      )
    );
    console.log(event.target);
  }

  async function ChangeTaskParams(groupTask: IGroupTask) {
    groupTask.isMandatory = Boolean(
      Number(
        (
          document.getElementById(
            `${groupTask.groupId}${groupTask.studyTask.Id}-mand`
          ) as HTMLSelectElement
        ).value
      )
    );
    groupTask.isAutoGraded = Boolean(
      Number(
        (
          document.getElementById(
            `${groupTask.groupId}${groupTask.studyTask.Id}-auto`
          ) as HTMLSelectElement
        ).value
      )
    );

    const newPointValue = Number(
      (
        document.getElementById(
          `${groupTask.groupId}${groupTask.studyTask.Id}-point`
        ) as HTMLInputElement
      ).value
    );
    if (newPointValue > 0) groupTask.maxTaskPointValueInGroup = newPointValue;
    await GroupService.ChangeGroupTaskParams(groupTask);
    setTaskTableGroup(await GroupService.GetGroupTasks(props.groupId));
  }

  function getMandTask() {
    return taskTableGroup?.filter((x) => x.isMandatory === true);
  }

  function getUnMandTask() {
    return taskTableGroup?.filter((x) => x.isMandatory === false);
  }

  return (
    <>
      {taskTableGroup?.length === 0 ? (
        <p className="fw-semibold fs-5">
          Вы еще не добавили ни одного задания в группу!
        </p>
      ) : (
        <Accordion key={"taskTable"}
          defaultActiveKey={"1"}
          alwaysOpen={true}
          className="mt-2 mb-2"
        >
          <Accordion.Item eventKey="1">
            <Accordion.Header>
              <p className="fs-5 fw-semibold">Задания в группе</p>
            </Accordion.Header>

            <Accordion.Body>
              <Tabs
                defaultActiveKey={"mandatory"}
                id="group-tabs"
                className="mb-3 mt-1"
              >
                <Tab eventKey="mandatory" title="Обязательные">
                  <Table striped bordered hover>
                    <thead className="fs-6 text-wrap lh-1 align-middle">
                      <tr>
                        <th>Задание</th>
                        <th>Описание задания</th>
                        <th>Обязательно к выполнению</th>
                        <th>Ставить оценку автоматически</th>
                        <th>Максимальное кол-во баллов за задание</th>
                      </tr>
                    </thead>

                    <tbody>
                      {getMandTask()?.map((task) => (
                        <tr>
                          <td>{task.studyTask.taskName}</td>
                          <td>{task.studyTask.taskDesc}</td>
                          <td>
                            <Form.Select
                              aria-label="Default select example"
                              id={`${task.groupId}${task.studyTask.Id}-mand`}
                              defaultValue={task.isMandatory ? "1" : "0"}
                              onChange={() => ChangeTaskParams(task)}
                            >
                              <option value={"1"}>Да</option>
                              <option value={"0"}>Нет</option>
                            </Form.Select>
                          </td>
                          <td>
                            <Form.Select
                              aria-label="Default select example"
                              defaultValue={task.isAutoGraded ? "1" : "0"}
                              id={`${task.groupId}${task.studyTask.Id} -auto`}
                              onChange={() => ChangeTaskParams(task)}
                            >
                              <option value={"1"}>Да</option>
                              <option value={"0"}>Нет</option>
                            </Form.Select>
                          </td>
                          <td>
                            <Form.Control
                              type="number"
                              placeholder={task.maxTaskPointValueInGroup.toString()}
                              id={`${task.groupId}${task.studyTask.Id}-point`}
                              onChange={() => ChangeTaskParams(task)}
                            />
                          </td>
                        </tr>
                      ))}
                    </tbody>
                  </Table>
                </Tab>
                {getUnMandTask()?.length === 0 ? (
                  <></>
                ) : (
                  <Tab eventKey="unMandatory" title="Дополнительные">
                    <Table striped bordered hover>
                      <thead className="fs-6 text-wrap lh-1 align-middle">
                        <tr>
                          <th>Задание</th>
                          <th>Описание задания</th>
                          <th>Обязательно к выполнению</th>
                          <th>Ставить оценку автоматически</th>
                          <th>Максимальное кол-во баллов за задание</th>
                        </tr>
                      </thead>

                      <tbody>
                        {getUnMandTask()?.map((task) => (
                          <tr>
                            <td>{task.studyTask.taskName}</td>
                            <td>{task.studyTask.taskDesc}</td>
                            <td>
                              <Form.Select
                                aria-label="Default select example"
                                id={`${task.studyTask.Id}-mand`}
                                defaultValue={task.isMandatory ? "1" : "0"}
                                onChange={() => ChangeTaskParams(task)}
                              >
                                <option value={"1"}>Да</option>
                                <option value={"0"}>Нет</option>
                              </Form.Select>
                            </td>
                            <td>
                              <Form.Select
                                aria-label="Default select example"
                                id={`${task.studyTask.Id}-auto`}
                                defaultValue={task.isAutoGraded ? "1" : "0"}
                                onChange={() => ChangeTaskParams(task)}
                              >
                                <option value={"1"}>Да</option>
                                <option value={"0"}>Нет</option>
                              </Form.Select>
                            </td>
                            <td>
                              <Form.Control
                                type="Number"
                                id={`${task.studyTask.Id}-point`}
                                onChange={() => ChangeTaskParams(task)}
                                placeholder={task.maxTaskPointValueInGroup.toString()}
                              />
                            </td>
                          </tr>
                        ))}
                      </tbody>
                    </Table>
                  </Tab>
                )}
              </Tabs>
            </Accordion.Body>
          </Accordion.Item>
        </Accordion>
      )}
    </>
  );
};

export default StudyTaskTableTeacher;
/*
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
<Row >
      <Accordion defaultActiveKey={["1"]} alwaysOpen={true}>
        {taskTableGroup?.map((row) => (
          <Col >
            <Accordion.Item eventKey={row.task_name}>
              <Accordion.Header>{row.task_name}</Accordion.Header>
              <Accordion.Body>
                {row.task_description}
                {"\n"} Кол-во баллов за задание {row.taskPointValue.toString()}
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
      {taskTableGroup?.map((row) => (
        <tr>
          
          <td>{row.task_name}</td>
          <td>{row.task_description}</td>
          <td>{row.taskPointValue.toString()}</td>
        </tr>
      ))}
    </tbody>
    </Table>
    <Pagination>{CreatePagination().map((row) => row)}</Pagination>
    */
