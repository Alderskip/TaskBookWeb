import { useContext, useState } from "react";
import { Button, ButtonGroup, Card } from "react-bootstrap";
import CardHeader from "react-bootstrap/esm/CardHeader";
import { useNavigate } from "react-router-dom";
import { Context } from "../..";
import ICourse from "../../models/CourseRelatedInterfaces/ICourse";
import CouseService from "../../services/CourseService";

interface ICouseCardProps {
  course: ICourse;
  backgroundImageName: string;
}

const CourseCard = (props: ICouseCardProps) => {
  const { store } = useContext(Context);
  const [cursButtonText, setCursButtonText] = useState<string>("Записаться");
  async function PrimaryButtonClick() {
    var response = await CouseService.AddUserToCouse(
      props.course.Id,
      store.user.Id
    );
    setCursButtonText(response);
  }

  return (
    <Card style={{ width: "18rem" }} className="text-white h-100 text-center ">
      <Card.Img
        src={require(`../../resourses/${props.backgroundImageName}`)}
        height={"270px"}
      />
      <Card.ImgOverlay className="p-0">
        <Card.Body>
          <Card.Header>
            <p className="fs-5 ">
              {" "}
              <b>{props.course.courseName}</b>{" "}
            </p>
          </Card.Header>
          <Card.Text className="fs-5"> {props.course.courseDesc}</Card.Text>
          <ButtonGroup aria-label="Basic example">
            <Button
              variant="primary"
              active={store.isAuth}
              onClick={PrimaryButtonClick}
              className="mr-2 ml-2"
            >
              {cursButtonText}
            </Button>

            <Button
              variant="info"
              active={store.isAuth}
              onClick={PrimaryButtonClick}
              className="mr-2 ml-2"
            >
              Подробнее
            </Button>
          </ButtonGroup>
        </Card.Body>
      </Card.ImgOverlay>
    </Card>
  );
};

export default CourseCard;
