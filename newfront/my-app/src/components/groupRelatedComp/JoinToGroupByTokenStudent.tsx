import { useState } from "react";
import { Alert, Button, Container, Form, Row } from "react-bootstrap";
import GroupService from "../../services/GroupService";

const JoinToGroupByTokenStudent = () => {
  const [code, setCode] = useState<string>("");
  const changeHandlerCode = (event: React.ChangeEvent<HTMLInputElement>) => {
    setCode(event.target.value);
  };
  const[statusAlert,setStatusAlert]= useState(<></>)
  async function SendInvToken() {
    var response = await GroupService.AddByTokenS(code);
    if(response.status===200)
    {
      setStatusAlert(
      <Alert key="success" variant="success">
        Вы были успешно добавлены в группу, пожалуйста перезагрузите страницу, чтобы продолжить
      </Alert>
      )
    }
    else
    {
      setStatusAlert(
        <Alert key="danger" variant="danger">
          Что-то пошло не так, пожалуйста проверьте введенный код!
        </Alert>
        )
    }
  }
  return (
    <>
    <Row className="mt-2">
    <Alert key="info" variant="info">
      <Alert.Heading>Вступить по коду</Alert.Heading>
      <p>
        Вы можете вступить в группу по коду-приглашения, который вам отправил преподаватель
      </p>
      <hr />
      <p className="mb-0">
        Введите здесь свой код-приглашение, который вы получили от преподавателя
        и нажмите кнопку вступить:{" "}
        <Form.Control
          placeholder="Код-приглашение"
          onChange={changeHandlerCode}
        />
        <Button variant="primary" onClick={SendInvToken}>
          Вступить
        </Button>
      </p>
    </Alert>
    </Row>
    <Row>
    {statusAlert}
    </Row>
    </>
  );
};
export default JoinToGroupByTokenStudent;