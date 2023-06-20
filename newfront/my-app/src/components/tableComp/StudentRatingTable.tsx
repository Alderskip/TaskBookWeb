import React, { useEffect, useState } from "react";
import { Pagination, Row, Table } from "react-bootstrap";
import IStudentRatingTableRow from "../../models/IStudentRatingTable";
import IUser from "../../models/IUser";
import UserService from "../../services/UserService";
const StudentRatingTable = () => {
  const [ratingTable, setRatingTable] = useState<IUser[]>([]);
  useEffect(() => {
    const GetTable = async () => {
      setRatingTable(await UserService.GetStudentRatingTable());
    };
    GetTable();
  }, []);
  const GiveMedals =(index:number):JSX.Element=>
  {
    switch(index)
    {
      case 1:
      {
        return(<img src={require("../../resourses/medal.png")} className="img-fluid"/>)
      }
      case 2:
      {
        return(<img src={require("../../resourses/silver-medal.png")} className="img-fluid"/>)
      }
      case 3:
      {
        return(<img src={require("../../resourses/bronze-medal.png")} className="img-fluid"/>)
      }
      default:
      {
        return (<></>)
      }
    }
    
  }
  return (
    <Row>
      <Table bordered className="caption-top ">
        <caption className="fs-5"><b>Топ самых активных пользователей</b></caption>
        <thead>
          <tr>
            <th>
              <b>№</b>{" "}
            </th>
            <th>Ник</th>
            
            <th>Общее количество очков</th>
            <th>Последний визит</th>
          </tr>
        </thead>
        <tbody>
          {ratingTable?.map((row, index) => (
            <tr>
              <td className="text-center">
                {(index + 1)<=3?(GiveMedals(index+1)):(<b>{(index+1).toString()}</b>)}
              </td>
              <td>{row.username}</td>
              
              <td >{row.totalStudyTaskPoints.toString()}</td>
              <td>{new Date(row.lastVisit).toLocaleDateString()}</td>
            </tr>
          ))}
        </tbody>
      </Table>
    </Row>
  );
};

export default StudentRatingTable;
