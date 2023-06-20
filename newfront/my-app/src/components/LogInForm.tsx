import React, { useContext, useState } from "react";
import { Alert, Container } from "react-bootstrap";
import Button from "react-bootstrap/Button";
import Form from "react-bootstrap/Form";
import Row from "react-bootstrap/Row";
import { useNavigate } from "react-router-dom";
import { Context } from "..";

const LogInForm = () => {
  const [validated, setValidated] = useState(false);
  const [login, setLogin] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [logInSuccess, setLoginSuccess] = useState<boolean>(true);
  const { store } = useContext(Context);
  const s = useNavigate();

  const changeHandlerLogin = (event: React.ChangeEvent<HTMLInputElement>) => {
    setLogin(event.target.value);
  };
  const changeHandlerPassword = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setPassword(event.target.value);
  };

  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    const form = event.currentTarget;
    event.preventDefault();

    const res = await store.login(login, password);
    if (res) {
      s("/");
    } else {
      setLoginSuccess(false);
      setValidated(false);
    }

    if (form.checkValidity() === false) {
      event.stopPropagation();
    }
    if (login === "" || password === "") setValidated(true);
  }

  return (
    <Form noValidate validated={validated} onSubmit={handleSubmit}>
      <Row className="mb-3  ">
        <Form.Group controlId="validationCustom01">
          <Form.Label>Логин</Form.Label>
          <Form.Control
            required
            type="text"
            placeholder="Логин"
            onChange={changeHandlerLogin}
          />
          <Form.Control.Feedback type="invalid">
            Пожалуйста введите логин
          </Form.Control.Feedback>
        </Form.Group>
        <Form.Group controlId="validationCustom02">
          <Form.Label>Пароль</Form.Label>
          <Form.Control
            required
            type="password"
            placeholder="Пароль"
            onChange={changeHandlerPassword}
          />

          <Form.Control.Feedback type="invalid">
            Пожалуйста введите пароль
          </Form.Control.Feedback>
        </Form.Group>
      </Row>
      <Button type="submit" className="mb-2">
        Войти
      </Button>
      {logInSuccess ? (
        <></>
      ) : (
        <Alert key={"danger"} variant={"danger"}>
          <p className="text-start text-wrap fs-5 lh-2">
            Неверный логин или пароль <br/> Еще нет аккаунта?{" "}
            <a href="/registration">Зарегистрируйтесь</a>
          </p>
        </Alert>
      )}
    </Form>
  );
};
export default LogInForm;
