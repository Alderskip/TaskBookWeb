
import { useEffect, useRef, useState } from "react";
import {
  Row,
  Col,
  Container,
  
  Tooltip,
  
  Overlay,
  Button,
} from "react-bootstrap";

import $api from "../../http";
import IGroup from "../../models/groupRelatedInterfaces/IGroup";
import IInvitationToken from "../../models/groupRelatedInterfaces/IInvitationToken";
import GroupService from "../../services/GroupService";
import GroupResTableT from "./GroupResTableTeacher";
import StudentsInGroupTable from "./StudentsInGroupTable";
import StudyTaskTableGroup from "./StudyTaskTableGroup";

interface IGroupInfoTeacherProps {
  group: IGroup;
}

const GroupInfoTeacher = (props: IGroupInfoTeacherProps) => {
  const [invitationToken, setInvitationToken] = useState<IInvitationToken>();
  const [showKeyTooltip, setShowKeyTooltip] = useState(false);
  const target = useRef(null);





  useEffect(() => {
    const GetToken = async () => {
      var response = await GroupService.GetGroupInvitationToken(props.group.Id);
      setInvitationToken(response);
      return response;
    };
    GetToken();
  }, []);


  function GetGradeSysRus(gradeSys:string):string
  {
      switch(gradeSys)
      {
        case "default":{
          return "По умолчанию"
        }
        case "flexible":
        {
          return "Гибкая"
        }
        case "credit":
        {
          return "Зачет"
        }
        default:{
          return "Гибкая"
        }
      }
  }


  const keyClick = () => {
    
    navigator.clipboard.writeText(
      String(`${$api}/AddByTokenS?token=${invitationToken?.invitationToken}`)
    );
    setShowKeyTooltip(!showKeyTooltip);
  };

  async function UpdateGroupResultsButtonClick()
  {
     var response = await GroupService.UpdateGroupResults(props.group.Id)
     return (response)
  }

  return (
    <Container className="">
      <Row>
        <Col className="col-md-6.6">
          <h1>Группа:"{props.group?.groupName}"</h1>
          <h2>Язык:"{props.group.groupEnv}"</h2>
          <p className="fs-5">Cистема оценивания: <b>{GetGradeSysRus(props.group.gradeSys)}</b></p>
        </Col>

        <Col className="text-break ">
          <h3>Код приглашения: </h3>

          <p
            className="fs-6 text-break text-primary "
            ref={target}
            onClick={keyClick}
          >
            {" "}
            {invitationToken?.invitationToken}
          </p>

          <Overlay
            target={target.current}
            show={showKeyTooltip}
            placement="bottom"
          >
            {(props) => (
              <Tooltip id="overlay-example" {...props}>
                Ссылка для приглашения скопирована в буфер обмена
              </Tooltip>
            )}
          </Overlay>
        </Col>
      </Row>

      <Row className="mt-2">
        <Col>
          <h2>Описание группы:</h2>
          <p className="fs-5 text-break">{props.group.groupDesc}</p>
        </Col>
      </Row>

      <Row className="mb-2">
        <Button variant="primary" onClick={()=>UpdateGroupResultsButtonClick()} >Обновить результаты группы</Button>
        <GroupResTableT group={props.group}/>
      </Row>

      <Row>
       
          {props.group === undefined ? (
            <></>
          ) : (
            <StudentsInGroupTable groupId={props.group?.Id} />
          )}
        
      </Row>

      

      <Row>
       
          <StudyTaskTableGroup groupId={props.group?.Id} />
       
      </Row>
    </Container>
  );
};
export default GroupInfoTeacher;
