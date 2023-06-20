import React from "react";
import { Container, Row, Col } from "react-bootstrap";
import LogInForm from "../components/LogInForm";



export const LoginPage: React.FC = () => {
  return (
    <Container className="mt-5">
      <Row className="justify-content-md-center ms-auto">
        <Col className="col-8"> 
          <LogInForm />
        </Col>
      </Row>
    </Container>
  );
};
