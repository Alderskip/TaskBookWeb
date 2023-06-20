import { useEffect, useState } from "react";
import { Col, Container, Row } from "react-bootstrap";
import { useLocation } from "react-router-dom";
import CourseCard from "../../components/coursesRelatedComp/CourseCard";
import ICourse from "../../models/CourseRelatedInterfaces/ICourse";
import CourseService from "../../services/CourseService";

export const CoursePage = () => {
  const [courses, setCourses] = useState<ICourse[]>([]);
  const l = useLocation();
  const specialCourses = ["BeginCourse", "IfCourse"];
  const courseDifficulties = [0, 1];
  useEffect(() => {
    const GetCouses = async () => {
      console.log(l)
      setCourses(await CourseService.GetCourses(l.search.split("=")[1]));
    };
    GetCouses();
  }, []);
  console.log(courses[0]?.courseName);

  return (
    <Container>
      {courseDifficulties.map((courseDifficultie) => (
        <Row className="justify-content-md-center md-2 mt-2">
          {courses
            .filter((x) => x.courseDifficulty === courseDifficultie)
            .sort(
              (a, b) =>
                specialCourses.indexOf(b.courseName) -
                specialCourses.indexOf(a.courseName)
            )
            ?.map((course, index) => (
              <>
                {index === 0 ? (
                  <>
                    <h2>
                      <p className="text-center">
                        Курсы уровня сложности {course.courseDifficulty}
                      </p>
                    </h2>
                    <hr />
                  </>
                ) : (
                  <></>
                )}
                <Col className=" mb-2 mt-2 col-4 text-center justify-content-center">
                  <CourseCard
                    course={course}
                    backgroundImageName={`courseCardBackgr${
                      course.courseDifficulty + 1
                    }.jpg`}
                  />
                </Col>
              </>
            ))}
        </Row>
      ))}
    </Container>
  );
};
