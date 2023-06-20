import { toJS } from "mobx";
import { useContext, useEffect, useRef, useState } from "react";
import { Container, Row, Col, Button, Accordion } from "react-bootstrap";
import { Context } from "../..";
import IGroup from "../../models/groupRelatedInterfaces/IGroup";
import IGroupResult from "../../models/groupRelatedInterfaces/IGroupResult";
import IGroupTask from "../../models/groupRelatedInterfaces/IGroupTask";
import GroupService from "../../services/GroupService";
import StudentGradedTasksTable from "./StudentGradedTasksTable";
import StudentGroupTaskTable from "./StudentGroupTaskTable";

interface IGroupInfoStudentProps {
  group: IGroup;
}
const GroupInfoStudent = (props: IGroupInfoStudentProps) => {
  const { store } = useContext(Context);
  const [groupTasks, setGroupTasks] = useState<IGroupTask[]>([]);
  const [studentResults, setStudentResults] = useState<IGroupResult[]>([]);
  const [accessdatDownloadUrl, setAccessdatDownloadUrl] = useState<string>("");
  const refAccessDatDownload = useRef<HTMLAnchorElement | null>(null);
  const refInitialResDatDownload = useRef<HTMLAnchorElement | null>(null);
  const [InitialResDowlandUrl, setInitialResDowlandUrl] = useState<string>("");
  useEffect(() => {
    const getGroupTasks = async () => {
      var response = await GroupService.GetGroupTasks(props.group.Id);
      setGroupTasks(response);
      return response;
    };
    const getStudentGradedTasks = async () => {
      var response = await GroupService.GetStudentResults(
        toJS(store.user).Id,
        props.group.Id
      );
      setStudentResults(response);
      return response;
    };
    const getAccessDownloadUrl = async () => {
      var response = await GroupService.DowlandAccessDatStudent(props.group.Id);
      setAccessdatDownloadUrl(URL.createObjectURL(new Blob([response])));
    };
    const getResultsDatDownloadUrl = async () => {
      var response = await GroupService.DowlandInitialResultFile(
        props.group.Id
      );
      setInitialResDowlandUrl(URL.createObjectURL(new Blob([response])));
    };
    getGroupTasks();
    getStudentGradedTasks();
    getAccessDownloadUrl();
    getResultsDatDownloadUrl();
  }, []);

  function GetGradeSysRus(gradeSys: string): string {
    switch (gradeSys) {
      case "default": {
        return "По умолчанию";
      }
      case "flexible": {
        return "Гибкая";
      }
      case "credit": {
        return "Зачет";
      }
      default: {
        return "Гибкая";
      }
    }
  }

  async function DowlandAccessDatButtonClick() {
    refAccessDatDownload.current?.click();
  }
  async function DowlandResultsDatButtonClick() {
    refInitialResDatDownload.current?.click();
  }

  return (
    <Container className="">
      <Row>
        <Col className="col-md-4">
          <h1>Группа:"{props.group?.groupName}"</h1>
          <h2>Язык:"{props.group.groupEnv}"</h2>
          <p className="fs-5">
            Cистема оценивания: <b>{GetGradeSysRus(props.group.gradeSys)}</b>
          </p>
        </Col>
      </Row>
      <Row>
        <Col className="col-8">
          {" "}
          <Accordion alwaysOpen={true} className="mb-2">
            <Accordion.Item eventKey="1">
              <Accordion.Header>
                Как начать работать с задачником
              </Accordion.Header>
              <Accordion.Body>
                <Row className=" justify-content-center">
                <h3>
                  Здесь вы можете скачать необходимые для начала работы с задачником
                  файлы
                </h3>
                <p>
                  Данные файлы вам необходимо скачать в вашу рабочую папку
                  задачника(для каждой группы требуется своя рабочая папка). Для
                  более подробной информации перейдите по{" "}
                  <a href="/FAQ#configurate-taskbook">
                    "Как настроить задачник"
                  </a>
                  .
                  <br /> Или воспользуйтесь нашим локальным клиентом (ссылка)
                </p>
                
                  <Col className="col-4">
                    <Button
                      variant="primary"
                      onClick={() => DowlandAccessDatButtonClick()}
                    >
                      Cкачать access.dat
                    </Button>

                    <a
                      href={accessdatDownloadUrl}
                      download="access.dat"
                      ref={refAccessDatDownload}
                    ></a>
                  </Col>
                  <Col className="col-4">
                    <Button
                      variant="primary"
                      onClick={() => DowlandResultsDatButtonClick()}
                      
                    >
                      Cкачать results.dat
                    </Button>
                    <a
                      href={InitialResDowlandUrl}
                      download="results.dat"
                      ref={refInitialResDatDownload}
                    ></a>
                  </Col>
                  </Row>
              </Accordion.Body>
            </Accordion.Item>
          </Accordion>
        </Col>
      </Row>
      <Row className="mt-2">
        <Col>
          <h2>Описание группы:</h2>
          <p className="fs-5 text-break">{props.group.groupDesc}</p>
        </Col>
      </Row>

      <Row>
        <StudentGradedTasksTable group={props.group} res={studentResults} />
      </Row>
      <Row>
        <StudentGroupTaskTable tasks={groupTasks} />
      </Row>
    </Container>
  );
};

export default GroupInfoStudent;
