import React, { useEffect, useState } from "react";
import { Accordion, Button, Col, Form, Row, Table } from "react-bootstrap";
import IUser from "../../models/IUser";
import GroupService from "../../services/GroupService";
interface IStudentsInGroupTableProps {
  groupId: number;
}
const StudentsInGroupTable = (props: IStudentsInGroupTableProps) => {
  const [studentTable, setStudentTable] = useState<IUser[]>();
  const [usernameToAdd, setUsernameToAdd] = useState<string>("");

  useEffect(() => {
    const GetStudentsInGroup = async () => {
      var response = await GroupService.GetGroupStudentsTeacher(props.groupId);
      setStudentTable(response);
    };
    GetStudentsInGroup();
  }, []);

  async function AddStudentByUsername() {
      await GroupService.AddStudentToGroupByUsernameTeacher(
      props.groupId,
      usernameToAdd
    );
    
    var response = await GroupService.GetGroupStudentsTeacher(props.groupId);
    setStudentTable(response);
  }

  const changeHandlerUsername = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setUsernameToAdd(event.target.value);
  };

  async function deleteStudentFromGroupButtonClick(studentId:number) {
    await GroupService.RemoveFromGroupTeacher(studentId,props.groupId);
    var response = await GroupService.GetGroupStudentsTeacher(props.groupId);
      setStudentTable(response);
  }

  return (
    <Accordion defaultActiveKey={["1"]} alwaysOpen={true}>
      <Accordion.Item eventKey={"1"}>
        <Accordion.Header>
          <p className="fs-5 fw-semibold">Студенты группы</p>
        </Accordion.Header>

        <Accordion.Body>
          {studentTable?.length != 0 ? (
            <Table striped bordered hover>
              <thead>
                <tr>
                  <th>Ник</th>
                  <th>ФИО</th>
                  <th></th>
                </tr>
              </thead>

              <tbody>
                {studentTable?.map((student) => (
                  <tr>
                    <td>{student.username}</td>
                    <td>{`${student.lastName} ${student.firstName} ${student.secondOrFathersName}`}</td>
                    <td><Button variant="primary" onClick={()=>deleteStudentFromGroupButtonClick(student.Id)} >Удалить из группы </Button></td>
                  </tr>
                ))}
              </tbody>
            </Table>
          ) : (
            <p className="fw-semibold fs-5">
              В группе еще нет студентов, вы можете добавить их с помощью
              кода-приглашения или формы ниже
            </p>
          )}

          <Form className="">
            <Form.Group
              className="mb-3 border border-primary border-opacity-50"
              controlId="formUsername"
              as={Row}
            >
              <Form.Label column sm="2" className="mt-1">
                <p className="fw-semibold fs-6">Добавить в группу : </p>
              </Form.Label>

              <Col sm="7" className="mt-2">
                <Form.Control
                  type="text"
                  placeholder="Логин студента"
                  onChange={changeHandlerUsername}
                />
              </Col>

              <Col>
                <Button
                  variant="primary"
                  className="mt-2 "
                  onClick={() => AddStudentByUsername()}
                >
                  Добавить
                </Button>
              </Col>
            </Form.Group>
          </Form>
        </Accordion.Body>
      </Accordion.Item>
    </Accordion>
  );
};
export default StudentsInGroupTable;
