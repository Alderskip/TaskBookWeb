import { useEffect, useState } from "react";
import { Col, Container, Row, Tabs, Tab, Form } from "react-bootstrap";
import IStudyTaskGroup from "../../models/studyTaskRelatedInterfaces/IStudyTaskGroup";
import GroupService from "../../services/GroupService";

import IGroup from "../../models/groupRelatedInterfaces/IGroup";

import TaskAdditinModelTable from "../../components/groupRelatedComp/TaskAdditingModelTable";

const AddTaskToGroupPage = () => {
  const [studyTaskGroups, setStudyTaskGroups] = useState<IStudyTaskGroup[]>([]);
  const [groups, setGroups] = useState<IGroup[]>([]);
  useEffect(() => {
    const getstudyTaskGroups = async () => {
      setStudyTaskGroups(await GroupService.GetStudyTaskGroups());
    };
    const getGroups = async () => {
      var response = await GroupService.GetGroupsTeacher();
      setGroups(response);
      return response;
    };

    getstudyTaskGroups();
    getGroups();
  }, []);

  return (
    <Container>
      <Row className="row justify-content-start">
        <Col>
          <Tabs
            defaultActiveKey={groups.at(0)?.Id}
            id="group-tabs"
            className="mb-3 mt-1"
          >
            {groups.map((group) => (
              <Tab eventKey={group.Id} title={group.groupName}>
                <Tabs
                  defaultActiveKey="Begin"
                  id="task-tabs"
                  className="mb-3 mt-1"
                >
                  {studyTaskGroups.map((studyTaskGroup) => (
                    <Tab
                      eventKey={studyTaskGroup.studyTaskGroupName}
                      title={studyTaskGroup.studyTaskGroupName}
                    >
                      <TaskAdditinModelTable
                        group={group}
                        studyTaskGroupId={Number(studyTaskGroup.Id)}
                      />
                    </Tab>
                  ))}
                </Tabs>
              </Tab>
            ))}
          </Tabs>
        </Col>
      </Row>
    </Container>
  );
};
export default AddTaskToGroupPage;
