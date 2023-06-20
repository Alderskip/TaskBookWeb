import { Container } from "react-bootstrap";
import { useLocation, useNavigate } from "react-router-dom";
import StudyTaskTableGroup from "../../components/groupRelatedComp/StudyTaskTableGroup";

export const AboutGroupPageStudent = () => {
  const location = useLocation();
  const s = useNavigate();
  const id = !isNaN(
    Number(location.search.split("?")[1].split("&")[0].split("=")[1])
  )
    ? Number(location.search.split("?")[1].split("&")[0].split("=")[1])
    : s("/error");
  const groupName = location.search.split("?")[1].split("&")[1].split("=")[1];
  console.log(id);
  console.log(groupName);

  return (
    <Container>
      <StudyTaskTableGroup groupId={Number(id)} />
    </Container>
  );
};
