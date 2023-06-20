import { Container, Row, Col, Accordion } from "react-bootstrap";
import { useLocation, useNavigate } from "react-router-dom";
import CourseCard from "../../components/coursesRelatedComp/CourseCard";
import StudyTaskTableCourse from "../../components/StudyTaskTableCourse";

export const AboutCoursePage = () => {
  const location = useLocation();
  const s = useNavigate();
  const id = !isNaN(
    Number(location.search.split("?")[1].split("&")[0].split("=")[1])
  )
    ? Number(location.search.split("?")[1].split("&")[0].split("=")[1])
    : s("/error");
  const courseName = location.search.split("?")[1].split("&")[1].split("=")[1];
  console.log(id);
  console.log(courseName);
  return (
    <Container>
      <Row>
        <Accordion>
          <Accordion.Item eventKey="0">
            <Accordion.Header>Задания</Accordion.Header>
            <Accordion.Body>
              <StudyTaskTableCourse courseId={Number(id)} />
            </Accordion.Body>
          </Accordion.Item>
        </Accordion>
      </Row>
    </Container>
  );
};
