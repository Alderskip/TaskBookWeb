import { observer } from "mobx-react-lite";
import { useContext, useEffect, useState } from "react";
import {
  Button,
  Card,
  Container,
  Form,
  ListGroup,
  ListGroupItem,
  Modal,
  Row,
} from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { Context } from "..";
import IUser from "../models/IUser";
import UserService from "../services/UserService";
interface IProps
{
  user1:IUser
}

const PersonalCabinet = (props:IProps) => {
  const [show, setShow] = useState(false);
  const { store } = useContext(Context);
  const [oldPassword, setOldPassword] = useState<string>("");
  const [newPassword, setNewPassword] = useState<string>("");
  const s = useNavigate();
  const [user, setUser] = useState<IUser>();


  useEffect(() => {
    const GetMe = async () => {
      setUser(await UserService.GetMe());
    };
    GetMe();
  }, []);

  const changeHandlersetOldPassword = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setOldPassword(event.target.value);
  };

  const changeHandlersetNewPassword = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setNewPassword(event.target.value);
  };

  const handleClose = () => {
    setShow(false);
  };

  const handleResetPassword = () => {
    store.ChangePassword(oldPassword, newPassword);
    setShow(false);
  };

  console.log("userId:", props.user1?.Id);
  const handleShow = () => setShow(true);
  

  return (
    <Container>
    <Row className="justify-content-md-center" >
    <Card style={{ width: "30rem" }} bg="Light" >
      <Card.Img variant="top" src={require("../resourses/avatar"+props.user1?.gender+".png")} alt="das" className="mb-2 mt-3 img-fluid" />
      <Card.Body>
        <Card.Title>Персональная информация</Card.Title>
      </Card.Body>
      <ListGroup className="list-group-flush">
        {<ListGroupItem>Логин: {user?.username} </ListGroupItem>}
        <ListGroupItem>
          ФИО: {user?.lastName} {user?.firstName}{" "}
          {user?.secondOrFathersName}
        </ListGroupItem>
        <ListGroupItem>Email: {user?.email}</ListGroupItem>
      </ListGroup>
      <Card.Body>
        <Button variant="primary" onClick={handleShow}>
          Изменить пароль
        </Button>
        <Modal show={show} onHide={handleClose}>
          <Modal.Header closeButton>
            <Modal.Title>Изменить пароль</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Form>
              <Form.Group className="mb-3" controlId="formOldPassword">
                <Form.Label>Старый пароль</Form.Label>
                <Form.Control
                  type="password"
                  placeholder="cтарый пароль "
                  onChange={changeHandlersetOldPassword}
                />
              </Form.Group>
              <Form.Group className="mb-3" controlId="formNewPassword">
                <Form.Label>Новый пароль</Form.Label>
                <Form.Control
                  type="password"
                  placeholder="новый пароль"
                  onChange={changeHandlersetNewPassword}
                />
              </Form.Group>
            </Form>
          </Modal.Body>
          <Modal.Footer>
            <Button variant="secondary" onClick={handleClose}>
              Закрыть
            </Button>
            <Button variant="primary" onClick={handleResetPassword}>
              Изменить пароль
            </Button>
          </Modal.Footer>
        </Modal>
        
      </Card.Body>
    </Card>
    </Row>
    </Container>
  );
};
export default observer(PersonalCabinet);
