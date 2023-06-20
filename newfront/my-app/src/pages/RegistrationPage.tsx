import React from "react";
import { Container, Row, Col } from "react-bootstrap";
import { RegistrationForm } from "../components/RegistationForm";

export const RegistrationPage: React.FC = () => {
  return (
    <Container >
      <Row className="justify-content-md-center">
        <Col className="col-8 ">
          <RegistrationForm />
        </Col>
      </Row>
    </Container>
  );
};
