import { Card } from "react-bootstrap";
import {  useNavigate } from "react-router-dom";
interface IEnviromentCourseCardProps {
  backgroundImageName: string;
  enviroment: string;
  enviromentDesc: string;
}
const EnviromentCourseCard = (props: IEnviromentCourseCardProps) => {
  const s=useNavigate();
  function onCardClick() {
    s(`/courses?env=${props.enviroment}`)
  }
  return (
    <Card
      className="bg-dark text-white h-100 text-center hovercard md-2 "
      style={{ width: "15rem" }}
      onClick={() => onCardClick()}
    >
      <Card.Img
        src={require(`../../resourses/${props.backgroundImageName}`)}
        height={"270px"}
        alt="Card image"
      />
      
      <Card.ImgOverlay>
      <Card.Header><p className="fs-5 "> <b>{props.enviroment}</b> </p></Card.Header>
        <Card.Text>
          <p className="fs-6 text-wrap ">{props.enviromentDesc}</p>
        </Card.Text>
      </Card.ImgOverlay>
    </Card>
  );
};

export default EnviromentCourseCard;
