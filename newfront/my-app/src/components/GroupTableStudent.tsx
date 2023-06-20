import React, { useEffect, useState } from "react";
import { Pagination, Row, Table } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import IGroupTableRow from "../models/IGroupTableRowStudent";
import GroupService from "../services/GroupService";
import NoGroupStudentAlert from "./groupRelatedComp/NoGroupStudentAlert";
const GroupTableStudent = () => {
  const [groupTable, setGroupTable] = useState<IGroupTableRow[]>();
  const [groupCount, setGrouptCount] = useState<number>(0);
  const [currentPageItem, setcurrentPageItem] = useState<number>(1);
  const s = useNavigate();
  useEffect(() => {
    const GetTable = async () => {
      setGroupTable(await GroupService.GetGroupTableStudent(1));
    };
    const GetGroupCount = async () => {
      setGrouptCount(await GroupService.GetGroupCount());
    };
    GetTable();
    GetGroupCount();
  }, []);
  async function PaginationItemClick(
    event: React.MouseEvent<HTMLButtonElement>
  ) {
    setcurrentPageItem(Number(event.currentTarget.textContent));
    setGroupTable(
      await GroupService.GetGroupTableStudent(
        Number(event.currentTarget.textContent)
      )
    );
    console.log(event.target);
  }
  function getCount(): number {
    return groupCount;
  }
  function InfoButtonClick(id: number, groupName: string) {
    s(`/GroupInfoS?groupId=${id}&courseName=${groupName}`);
  }
  const CreatePagination = () => {
    let items = [];
    for (var i = 1; i <= groupCount / 5 + 1; i++) {
      items.push(
        <Pagination.Item
          key={i}
          active={i === currentPageItem}
          onClick={PaginationItemClick}
        >
          {i}
        </Pagination.Item>
      );
    }
    return items;
  };
  return (
    <Row>
      {getCount() === 0 ? (
        <NoGroupStudentAlert />
      ) : (
        <>
          <Table striped bordered hover>
            <thead>
              <tr>
                <th>Название группы</th>
                <th>ФИО преподавателя</th>
                <th>Язык программирования</th>
                <th>Доступны задания для выполнения</th>
              </tr>
            </thead>
            <tbody>
              {groupTable?.map((row) => (
                <tr onClick={() => InfoButtonClick(row.Id, row.groupName)}>
                  <td>{row.groupName}</td>
                  <td>{row.teacherFIO}</td>
                  <td>{row.enviroment}</td>
                  <td>{row.taskToDo ? <>Да</> : <>Нет</>}</td>
                </tr>
              ))}
            </tbody>
          </Table>
          <Pagination>{CreatePagination().map((row) => row)}</Pagination>{" "}
        </>
      )}
    </Row>
  );
};

export default GroupTableStudent;
