import React, { useEffect, useState } from "react";
import { Pagination, Row, Table } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import IGroupTableRow from "../models/IGroupTableRowStudent";
import GroupService from "../services/GroupService";
import StudentsInGroupTable from "./groupRelatedComp/StudentsInGroupTable";
const GroupTableTeacher = () => {
  const [groupTable, setGroupTable] = useState<IGroupTableRow[]>();
  const [groupCount, setGrouptCount] = useState<number>(0);
  const [currentPageItem, setcurrentPageItem] = useState<number>(1);

  const s = useNavigate();
  useEffect(() => {
    const GetTable = async () => {
      setGroupTable(await GroupService.GetGroupTableTeacher(1));
    };
    const GetGroupCount = async () => {
      setGrouptCount(await GroupService.GetGroupCountTeacher());
    };
    GetTable();
    GetGroupCount();
  }, []);
  async function PaginationItemClick(
    event: React.MouseEvent<HTMLButtonElement>
  ) {
    setcurrentPageItem(Number(event.currentTarget.textContent));
    setGroupTable(
      await GroupService.GetGroupTableTeacher(
        Number(event.currentTarget.textContent)
      )
    );
    console.log(event.target);
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
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>Название группы</th>
            <th>Язык программирования</th>
            <th>Появились новые задания для проверки</th>
          </tr>
        </thead>
        <tbody>
          {groupTable?.map((row) => (
            <>
              <tr>
                <td>{row.groupName}</td>
                <td>{row.enviroment}</td>
                <td>{row.taskToDo ? <>Да</> : <>Нет</>}</td>
              </tr>
              <StudentsInGroupTable groupId={row.Id} />
            </>
          ))}
        </tbody>
      </Table>
      <Pagination>{CreatePagination().map((row) => row)}</Pagination>{" "}
    </Row>
  );
};

export default GroupTableTeacher;
