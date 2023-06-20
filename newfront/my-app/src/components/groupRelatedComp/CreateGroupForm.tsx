
import { useState } from "react";
import {
  Button,
  Col,
  Form,
  OverlayTrigger,
  Row,
  Tooltip,
} from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import GroupService from "../../services/GroupService";

const CreateGroupForm = () => {
  const [groupName, setGroupName] = useState<string>("");
  const [groupDesc, setgroupDesc] = useState<string>("");
  const [gradeSys, setgroupGradeSys] = useState<string>("flexible");
  const [groupEnv, setgroupEnv] = useState<string>("Python3");

  const s = useNavigate();

  async function CreateGroup() {
    console.log(groupName);
    await GroupService.CreateGroup({
      Id: 0,
      groupName: groupName,
      groupDesc: groupDesc,
      gradeSys: gradeSys,
      groupEnv: groupEnv,
      lastUpdateTime: null,
    });
   
  }

  return (
    <Form>
      <Form.Group className="mb-3 mt-2" controlId="GroupName" as={Row}>
        <Col md="auto" className="mt-1">
          <Form.Label>Название группы:</Form.Label>
        </Col>
        <Col>
          <Form.Control
            required
            type="text"
            placeholder="Название группы"
            onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
              setGroupName(event.target.value);
            }}
          />
        </Col>
      </Form.Group>

      <Form.Group className="mb-3" controlId="GroupDesc" as={Row}>
        <Form.Label>Описание группы:</Form.Label>
        <Col>
          <Form.Control
            as="textarea"
            rows={3}
            onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
              setgroupDesc(event.target.value);
            }}
          />
        </Col>
      </Form.Group>

      <Row>
        <Col>
          <Form.Group className="mb-3" controlId="GradeSys">
            <Form.Label>Система оценивания</Form.Label>
            <Col>
              <Form.Select aria-label="Default select example">
                <OverlayTrigger
                  overlay={
                    <Tooltip id="tooltip-1">
                      Вы сами выставляете число баллов за задание
                    </Tooltip>
                  }
                >
                  <option
                    onSelect={() => setgroupGradeSys("flexible")}
                    value="flexible"
                  >
                    Гибкая
                  </option>
                </OverlayTrigger>
                <OverlayTrigger
                  overlay={
                    <Tooltip id="tooltip-2">
                      Баллы за задания рассчитаны нами
                    </Tooltip>
                  }
                >
                  <option
                    onSelect={() => setgroupGradeSys("default")}
                    value="default"
                  >
                    По умолчанию
                  </option>
                </OverlayTrigger>
                <OverlayTrigger
                  overlay={
                    <Tooltip id="tooltip-3">
                      За каждое задание ставиться 1 или 0 баллов (выполено/не
                      выполнено)
                    </Tooltip>
                  }
                >
                  <option
                    onSelect={() => setgroupGradeSys("credit")}
                    value="credit"
                  >
                    Зачет
                  </option>
                </OverlayTrigger>
              </Form.Select>
            </Col>
          </Form.Group>
        </Col>

        <Col>
          <Form.Group className="mb-3" controlId="GradeSys">
            <Form.Label>Язык программирования</Form.Label>
            <Col>
              <Form.Select aria-label="Default select example">
                <option onSelect={() => setgroupEnv("Python3")} value="Python3">
                  Python3
                </option>
                <option onSelect={() => setgroupEnv("C++")} value="C++">
                  C++
                </option>
                <option onSelect={() => setgroupEnv("C#")} value="C#">
                  C#
                </option>
              </Form.Select>
            </Col>
          </Form.Group>
        </Col>
      </Row>

      <Button onClick={CreateGroup} >Создать</Button>
    </Form>
  );
};
export default CreateGroupForm;
