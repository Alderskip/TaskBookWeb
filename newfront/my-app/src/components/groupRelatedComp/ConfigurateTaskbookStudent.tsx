
import { group } from "console"
import { Container, Row } from "react-bootstrap"
import IGroup from "../../models/groupRelatedInterfaces/IGroup"

interface IConfigurateTaskbookProps
{
    group:IGroup
    children:JSX.Element[]
}
const ConfigurateTaskbook=(props: IConfigurateTaskbookProps)=>
{
    return(
        <Container>
            <Row>
                <h1>Настройка локального задачника для группы: {props.group.groupName}</h1>
            </Row>
            <Row>
                <p>Здесь вы можете скачать необходимые для работы с задачником файлы</p>
            </Row>
            {props.children[0]}
        </Container>
    )
}
export default ConfigurateTaskbook;