import React, { useContext, useState } from "react";
import { Button, Form } from "react-bootstrap";
import { useForm, SubmitHandler } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { Context } from "..";

interface IRegistationFormInput {
  username: string;
  password: string;
  email: string;
  firstName: string;
  lastName: string;
  secondOrFathersName: string;
  gender:string;
}

export const RegistrationForm = () => {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<IRegistationFormInput>();
  let s = useNavigate();
  const { store } = useContext(Context);
  const onSubmit: SubmitHandler<IRegistationFormInput> = async (data) => {
    setValidated(true);

    const res = await store.registration(
      data.username,
      data.password,
      data.firstName,
      data.lastName,
      data.email,
      data.secondOrFathersName
    );
    if (res) s("/FAQ");
  };

  const [validated, setValidated] = useState(false);

  return (
    <Form noValidate onSubmit={handleSubmit(onSubmit)} className="mb-2 mt-5">
      <Form.Group className="mb-2" controlId="formUsername">
        <Form.Label>Логин</Form.Label>
        <Form.Control
          type="username"
          placeholder="Логин"
          defaultValue={""}
          {...register("username", {
            required: "Это поле обязательно",
            minLength: {
              value: 4,
              message: "Минимальная длинна логина 4 символа",
            },
            maxLength: {
              value: 50,
              message: "Максимальная длинна логина 50 символов",
            },
          })}
          isValid={validated && errors.username?.message === undefined}
          isInvalid={errors.username?.message !== undefined}
        />
        <Form.Control.Feedback type="invalid">
          {errors.username?.message}
        </Form.Control.Feedback>
      </Form.Group>
      <Form.Group className="mb-2" controlId="formPasswordd">
        <Form.Label>Пароль</Form.Label>
        <Form.Control
          type="password"
          placeholder="Пароль"
          defaultValue={""}
          {...register("password", {
            required: "Это поле обязательно",
            minLength: {
              value: 8,
              message: "Минимальная длинна пароля 8 символа",
            },
            maxLength: {
              value: 50,
              message: "Максимальная длинна пароля 50 символов",
            },
          })}
          isValid={validated && errors.password?.message === undefined}
          isInvalid={errors.password?.message !== undefined}
        />
        <Form.Control.Feedback type="invalid">
          {errors.password?.message}
        </Form.Control.Feedback>
      </Form.Group>
      <Form.Group className="mb-2" controlId="formEmail">
        <Form.Label>Email</Form.Label>
        <Form.Control
          type="email"
          placeholder="Email"
          defaultValue={""}
          {...register("email", {
            required: "Это поле обязательно",
            pattern: {
              value: /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/,
              message: "Неверный формат email",
            },
            maxLength: {
              value: 500,
              message: "Максимальная длинна email 500 символов",
            },
          })}
          isValid={validated && errors.email?.message === undefined}
          isInvalid={errors.email?.message !== undefined}
        />
        <Form.Control.Feedback type="invalid">
          {errors.email?.message}
        </Form.Control.Feedback>
      </Form.Group>
      <Form.Group className="mb-2" controlId="formFirstName">
        <Form.Label>Имя</Form.Label>
        <Form.Control
          type="text"
          placeholder="Имя"
          defaultValue={""}
          {...register("firstName", {
            required: "Это поле обязательно",
            minLength: {
              value: 2,
              message: "Минимальная длинна имени 2 символа",
            },
            maxLength: {
              value: 50,
              message: "Максимальная длинна имени 50 символов",
            },
          })}
          isValid={validated && errors.firstName?.message === undefined}
          isInvalid={errors.firstName?.message !== undefined}
        />
        <Form.Control.Feedback type="invalid">
          {errors.firstName?.message}
        </Form.Control.Feedback>
      </Form.Group>
      <Form.Group className="mb-2" controlId="formLastName">
        <Form.Label>Фамилия</Form.Label>
        <Form.Control
          type="text"
          placeholder="Фамилия"
          defaultValue={""}
          {...register("lastName", {
            required: "Это поле обязательно",
            minLength: {
              value: 2,
              message: "Минимальная длинна фамилии 2 символа",
            },
            maxLength: {
              value: 50,
              message: "Максимальная длинна фамилии 50 символов",
            },
          })}
          isValid={validated && errors.lastName?.message === undefined}
          isInvalid={errors.lastName?.message !== undefined}
        />
        <Form.Control.Feedback type="invalid">
          {errors.firstName?.message}
        </Form.Control.Feedback>
      </Form.Group>
      <Form.Group className="mb-2" controlId="formSecondOrFathersName">
        <Form.Label>Отчество</Form.Label>
        <Form.Control
          type="text"
          placeholder="Отчество(необязательно)"
          defaultValue={""}
          {...register("secondOrFathersName", {})}
          isValid={validated}
        />
        <Form.Control.Feedback type="invalid">
          {errors.secondOrFathersName?.message}
        </Form.Control.Feedback>
      </Form.Group>

      <Form.Group className="mb-2" controlId="formGender">
        <Form.Label>Пол</Form.Label>
        <Form.Select aria-label="Default select example"
        {...register("gender" )}>
          <option value="Undefined">Не указан</option>
          <option value="Male">Мужской</option>
          <option value="Female">Женский</option>
          
        </Form.Select>
       
      </Form.Group>

      <Button type="submit">Зарегистрироваться</Button>
    </Form>
  );
};

export default RegistrationForm;
