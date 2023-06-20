import React from "react";
import { Container, Row,Col } from "react-bootstrap";
import StudentRatingTable from "../components/tableComp/StudentRatingTable";

export const HomePage: React.FC = () => {
  return (
    <Container className="mt-2">
      <Row className="justify-content-md-center">
        <Col className="col-10">
        <StudentRatingTable />
        </Col>
      </Row>
    </Container>
  );
};
