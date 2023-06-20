import React, { useContext, useState } from "react";
import { Form, Button } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { Context } from "..";
import { useForm } from "react-hook-form";
export const RegistrationForm = () => {
  const [login, setLogin] = useState<string>("");

  const [password, setPassword] = useState<string>("");

  const [email, setEmail] = useState<string>("");

  const [firstName, setFirstName] = useState<string>("");

  const [lastName, setLastName] = useState<string>("");

  const [secondOrFathersName, setSecondOrFathersName] = useState<string>(""); 

  const { store } = useContext(Context);
  const s = useNavigate();

  const [loginisValid, setLoginValid] = useState<boolean>(false);

  const { register, handleSubmit } = useForm();

  const changeHandlerLogin = async (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setLogin(event.target.value);
    if (await store.checkUsernameValid(event.target.value)) setLoginValid(true);
    else setLoginValid(false);
  };

  const changeHandlerPassword = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setPassword(event.target.value);
  };

  const changeHandlerEmail = (event: React.ChangeEvent<HTMLInputElement>) => {
    setEmail(event.target.value);
  };

  const changeHandlerFirstName = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setFirstName(event.target.value);
  };

  const changeHandlerLastName = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setLastName(event.target.value);
  };

  const changeHandlerSecondOrFathersName = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setSecondOrFathersName(event?.target.value);
  };

    





  async function RegistrateButtonClick(
    event: React.MouseEvent<HTMLButtonElement>
  ) {
    const res = await store.registration(
      login,
      password,
      firstName,
      lastName,
      email,
      secondOrFathersName
    );
    if (res) s("/FAQ");
  }

  
  return (
    <Form>
      <Form.Group className="mb-3" controlId="formUsername">
        <Form.Label>Логин</Form.Label>
        <Form.Control
          type="username"
          placeholder="Логин"

          onChange={changeHandlerLogin}
        />
      </Form.Group>
      {loginisValid && (
        <Form.Text className="text-muted">
          Данный логин можно использовать
        </Form.Text>
      )}
      {login.length <= 4 && (
        <Form.Text className="text-muted">
          Логин должен быть длиннее 4 символов
        </Form.Text>
      )}
      {!loginisValid && login.length > 4 && (
        <Form.Text className="text-muted">Данный логин уже занят</Form.Text>
      )}
      <Form.Group className="mb-3" controlId="formPasswordd">
        <Form.Label>Пароль</Form.Label>
        <Form.Control
          type="passwordd"
          placeholder="Пароль "
          onChange={changeHandlerPassword}
        />
      </Form.Group>
      <Form.Group className="mb-3" controlId="formName">
        <Form.Label>Имя</Form.Label>
        <Form.Control
          type="Name"
          placeholder="Имя"
          onChange={changeHandlerFirstName}
        />
      </Form.Group>
      <Form.Group className="mb-3" controlId="formLastName">
        <Form.Label>Пароль</Form.Label>
        <Form.Control
          type="lastName"
          placeholder="Фамилия "
          onChange={changeHandlerLastName}
        />
      </Form.Group>
      <Form.Group className="mb-3" controlId="formFathersName">
        <Form.Label>Пароль</Form.Label>
        <Form.Control
          type="FathersName"
          placeholder="Отчество "
          onChange={changeHandlerSecondOrFathersName}
        />
      </Form.Group>
      <Form.Group className="mb-3" controlId="formEmail">
        <Form.Label>Email</Form.Label>
        <Form.Control
          type="Email;"
          placeholder="Email "
          onChange={changeHandlerEmail}
        />
      </Form.Group>
      <Button variant="primary" onClick={RegistrateButtonClick}>
        Зарегистрироваться
      </Button>
    </Form>
  );
};
/*
<div className="InputAutoData RegInForm ">
      <label htmlFor="login" className="loginLabel ">
        Логин{" "}
      </label>
      <input
        required={true}
        onChange={changeHandlerLogin}
        value={login}
        type="text"
        id="login"
        placeholder="Логин"
        //onKeyPress={keyPressHandler}
      />
      <label htmlFor="password" className="passwordLabel ">
        Пароль
      </label>
      <input
        onChange={changeHandlerPassword}
        type="text"
        required={true}
        value={password}
        id="password"
        placeholder="Пароль"
       // onKeyPress={keyPressHandler}
      />
      <label htmlFor="email" className="emailLabel ">
        Email
      </label>
      <input
        onChange={changeHandlerEmail}
        type="text"
        required={true}
        value={email}
        id="email"
        placeholder="Email"
      //  onKeyPress={keyPressHandler}
      />
      <label htmlFor="firstName" className="firstNameLabel ">
        Имя
      </label>
      <input
        onChange={changeHandlerFirstName}
        type="text"
        required={true}
        value={firstName}
        id="firstName"
        placeholder="Имя"
        //onKeyPress={keyPressHandler}
      />
      <label htmlFor="lastName" className="lastNameLabel ">
        Фамилия
      </label>
      <input
        onChange={changeHandlerLastName}
        type="text"
        required={true}
        value={lastName}
        id="lastName"
        placeholder="Фамилия"
       // onKeyPress={keyPressHandler}
      />
      <label htmlFor="secondOrFathersName" className="secondOrFathersName ">
        Отчество
      </label>
      <input
        onChange={changeHandlerSecondOrFathersName}
        type="text"
        value={secondOrFathersName}
        id="secondOrFathersName"
        placeholder="Отчество"
       // onKeyPress={keyPressHandler}
      />
      <button type="button" onClick={RegistrateButtonClick}>
        Зарегистрироваться
      </button>
    </div>
*/
