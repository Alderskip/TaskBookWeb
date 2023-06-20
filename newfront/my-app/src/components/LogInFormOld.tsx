import React, { useContext, useState } from "react";
import { Context } from "..";
import { Alert, Button, Container, Form, Row } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
export const LogInFormOld: React.FC = () => {
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

  async function AutorizeButtonClick(
    event: React.MouseEvent<HTMLButtonElement>
  ) {
    const res = await store.login(login, password);
    if (res) {
      s("/");
    } else setLoginSuccess(false);
  }

  return (
    <Container>
      <Row className="justify-content-md-center">
        <Form className="">
          <Form.Group className="mb-3" controlId="formUsername">
            <Form.Label>Логин</Form.Label>
            <Form.Control
              type="username"
              placeholder="Логин "
              onChange={changeHandlerLogin}
            />
          </Form.Group>
          <Form.Group className="mb-3" controlId="formPassword">
            <Form.Label>Пароль</Form.Label>
            <Form.Control
              type="password"
              placeholder="Пароль "
              onChange={changeHandlerPassword}
            />
          </Form.Group>
          {logInSuccess ? (
            <></>
          ) : (
            <Alert key={"danger"} variant={"danger"}>
              Неверный логин или пароль
            </Alert>
          )}
          <Button variant="primary" onClick={AutorizeButtonClick}>
            Войти
          </Button>
        </Form>
      </Row>
    </Container>
  );
};

/*
 <div className="InputAutoData LogInForm ">
      <label htmlFor="login" className="loginLabel ">
        Введите логин или email
      </label>
      <input
        onChange={changeHandlerLogin}
        value={login}
        type="text"
        id="login"
        placeholder="Логин или email"
        //onKeyPress={keyPressHandler}
      />
      <label htmlFor="password" className="passwordLabel ">
        Введите пароль
      </label>
      <input
        onChange={changeHandlerPassword}
        type="text"
        value={password}
        id="password"
        placeholder="Пароль"
      //  onKeyPress={keyPressHandler}
      />
      <button type="button" onClick={AutorizeButtonClick}>
        Вход
      </button>
    </div>
*/
