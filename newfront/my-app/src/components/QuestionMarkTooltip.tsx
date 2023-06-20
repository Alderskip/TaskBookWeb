import Button from "react-bootstrap/Button";
import OverlayTrigger from "react-bootstrap/OverlayTrigger";
import Tooltip from "react-bootstrap/Tooltip";
interface IQuestionMarkTooltipProps
{
    text:string
}
const QuestionMarkTooltip = (props: IQuestionMarkTooltipProps) => {
  return (
    <OverlayTrigger
      key={"right"}
      placement={"right"}
      overlay={
        <Tooltip id={`tooltip-${"right"}`} color="blue">
          <strong>{props.text}</strong>.
        </Tooltip>
      }
    >
      <Button variant=" btn-outline-dark btn-sm"  >
       <b>?</b>
      </Button>
    </OverlayTrigger>
  );
};
export default QuestionMarkTooltip;
