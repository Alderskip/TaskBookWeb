import { Container, Row, Col,Stack } from "react-bootstrap";
import EnviromentCourseCard from "../../components/coursesRelatedComp/EnviromentCourseCard";
import Enviroments from "../../models/Enviroments";

const ChooseCourseEnviromentPage = () => {


  return (
    <>
    
    <Container className="">
        
      <Row className="mt-2 mb-2">
        <h2 ><p className="text-center">Чтобы начать работать с курсами, выберите интересующий вас язык программирования из представленных ниже </p> </h2>
        <hr/>
      </Row>
      <Row
        md={2}
        lg={3}
        xs={1}
        className="justify-content-center "
      >
         <Col className="mb-2">
          <EnviromentCourseCard
            enviroment={Enviroments.Python}
            enviromentDesc={
              "В основном используеться для создания веб-сайтов и программного обеспечения, автоматизации задач и проведения анализа данных."
            }
            backgroundImageName={"envBackgr1.jpg"}
          />
        </Col>
        <Col className="mb-2">
          <EnviromentCourseCard
            enviroment={Enviroments.Cplus}
            enviromentDesc={
              "Применяется для создания высоконагруженных сервисов, где важна скорость работы: поисковые, рекламные системы, драйверы, операционные системы, игры и приложения."
            }
            backgroundImageName={"envBackgr2.jpg"}
          />
        </Col>
        <Col className="mb-2">
          <EnviromentCourseCard
            enviroment={Enviroments.CSharp}
            enviromentDesc={
              "Универсальный язык,на котором  пишут игры, десктопные приложения, веб-сервисы, нейросети и даже графику для метавселенных"
            }
            backgroundImageName={"envBackgr3.jpg"}
          />
        </Col>
          
     
      </Row>
    </Container>
    </>
  );
};
  
export default ChooseCourseEnviromentPage;
