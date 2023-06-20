import { useEffect, useState } from "react";
import { Container, Row, Tab, Col, Nav, TabContent } from "react-bootstrap";
import CourseInfo from "../../components/coursesRelatedComp/CourseInfo";
import ICourse from "../../models/CourseRelatedInterfaces/ICourse";
import Enviroments from "../../models/Enviroments";
import CourseService from "../../services/CourseService";

const MyCoursesPage = () => {
  const [courses, setCourses] = useState<ICourse[]>([]);
  useEffect(() => {
    const GetCouses = async () => {
      setCourses(await CourseService.GetUserCourses());
    };
    GetCouses();
  }, []);
  const enviroments = Object.values(Enviroments) as unknown as Array<
    keyof typeof Enviroments
  >;
  return (
    <Container>
      <Tab.Container
        id="enviroments"
        defaultActiveKey={`event${enviroments[0]}`}
      >
        <Row className="mb-2 mt-2">
          <Col>
            <Nav variant="pills" className="">
              {enviroments.map((key) => (
                <Nav.Item>
                  <Nav.Link eventKey={`event${key}`}>{key}</Nav.Link>
                </Nav.Item>
              ))}
            </Nav>
          </Col>
        </Row>

        <Row>
          <Tab.Content>
            {enviroments.map((key) => (
              <Tab.Pane eventKey={`event${key}`}>
                <Tab.Container>
                  <Nav variant="tabs">
                    {courses
                      .filter((x) => x.courseEnv == key)
                      ?.map((course) => (
                        <>
                          <Nav.Item>
                            <Nav.Link
                              eventKey={`selectedCourse${course.courseName}`}
                            >
                              {course.courseName}
                            </Nav.Link>
                          </Nav.Item>
                        </>
                      ))}
                  </Nav>
                  <TabContent>
                    {courses
                      .filter((x) => x.courseEnv == key)
                      ?.map((course) => (
                        <>
                          <Tab.Pane
                            eventKey={`selectedCourse${course.courseName}`}
                          >
                            <CourseInfo course={course}/>
                          </Tab.Pane>
                        </>
                      ))}
                  </TabContent>
                </Tab.Container>
              </Tab.Pane>
            ))}
          </Tab.Content>
        </Row>
      </Tab.Container>
    </Container>
  );
};
export default MyCoursesPage;
