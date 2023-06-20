import { Alert, Col, Container, Row, Image } from "react-bootstrap";

export const ErrorPage: React.FC = () => {
  const logo = require("../resourses/error.png");
  return (
    <Container>
      <Row className="justify-content-md-center">
        <Alert variant="danger">
          <Alert.Heading className="ErrorText">Ошибка</Alert.Heading>
        </Alert>
      </Row>
      <Row className="justify-content-md-center">
        <Col>
          <Image src={logo} fluid thumbnail />
        </Col>
      </Row>
      <Row className="justify-content-md-center">
        <Alert variant="danger">
          <Alert.Heading>Возникла ошибка</Alert.Heading>
          <p>
            Пожалуйста попробуйте повторить запрос еще раз или свяжитесь с
            поддержкой
          </p>
        </Alert>
      </Row>
    </Container>
  );
};
