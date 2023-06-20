import userEvent from "@testing-library/user-event";
import { useEffect, useState } from "react";
import { Container, Row, Tab, Tabs } from "react-bootstrap";
import CreateGroupForm from "../../components/groupRelatedComp/CreateGroupForm";
import GroupInfoStudent from "../../components/groupRelatedComp/GroupInfoStudent";
import GroupInfoTeacher from "../../components/groupRelatedComp/GroupInfoTeacher";
import JoinToGroupByTokenStudent from "../../components/groupRelatedComp/JoinToGroupByTokenStudent";
import NoGroupStudentAlert from "../../components/groupRelatedComp/NoGroupStudentAlert";
import IGroup from "../../models/groupRelatedInterfaces/IGroup";
import IUser from "../../models/IUser";
import GroupService from "../../services/GroupService";
interface IGroupPageProps {
  user: IUser | undefined;
}

export const GroupPage = (props: IGroupPageProps) => {
  const [groups, setGroups] = useState<IGroup[]>([]);
  useEffect(() => {
    const getGroupsTeacher = async () => {
      var response = await GroupService.GetGroupsTeacher();
      setGroups(response);
      return response;
    };
    const getGroupsStudent = async()=>{
      var response = await GroupService.GetGroupsStudent();
      setGroups(response);
      return response;
    }
    getGroupsTeacher()
    getGroupsStudent()
    
    
  }, []);
  console.log(props.user);

  return (
    <Container>
      
      <Row className="justify-content-md-center">
        {groups.length === 0 && props.user?.role==="Teacher" ? (
          <CreateGroupForm />
        ) : (
          <Tabs
            defaultActiveKey={groups.at(0)?.Id}
            id="group-tab"
            className="mb-3 fill"
            
          >
            {groups.map((group) => (
              <Tab eventKey={String(group.Id)} title={group.groupName}>
                {props.user?.role === "Teacher" ? (
                  <GroupInfoTeacher group={group} />
                ) : (
                  <GroupInfoStudent group={group} />
                )}
              </Tab>
            ))}
            {props.user?.role === "Teacher" ? (
              <Tab eventKey={String("createGroup")} title="Создать группу">
                <CreateGroupForm />
              </Tab>
            ) : (  groups.length===0?(<></>):(<Tab eventKey={String("joinGroup")} title="Вступить по коду">
            <JoinToGroupByTokenStudent />
          </Tab>)

            )}
            
          </Tabs>
          
        )}
        {props.user?.role === "Student" && groups.length===0 ?(<NoGroupStudentAlert />):(<></>)}
      </Row>
    </Container>
  );
};
