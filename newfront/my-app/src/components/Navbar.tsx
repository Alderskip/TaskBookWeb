import { observer } from "mobx-react-lite";
import { Container, Nav, Navbar, NavDropdown } from "react-bootstrap";
import Store from "../store/store";
interface INavbarProps {
  store: Store;
}

const NavbarSite: React.FC<INavbarProps> = (props) => {
  function ExitButtonClick(event: React.MouseEvent<HTMLButtonElement>) {
    props.store.logout();
  }
  return (
    <Navbar
      className="color sticky-top"
      collapseOnSelect
      expand="lg"
      variant="dark"
    >
      <Container>
        <Navbar.Brand href="/">TaskBookWeb</Navbar.Brand>
        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Collapse id="responsive-navbar-nav">
          <Nav className="me-auto">
            {props.store.isAuth ? (
              <NavDropdown title="Курсы" id="collasible-nav-dropdown">
                <NavDropdown.Item href="/chooseCourseEnv">
                  Записаться на курс
                </NavDropdown.Item>
                <NavDropdown.Item href="/myCourses">Мои курсы</NavDropdown.Item>
              </NavDropdown>
            ) : (
              <Nav.Link href="/chooseCourseEnv">Курсы</Nav.Link>
            )}
            {props.store.isAuth ? (
              <Nav.Link href="/groups">Группы</Nav.Link>
            ) : (
              <></>
            )}
          </Nav>
          <Nav>
            {props.store.isAuth ? (
              <Nav.Link href="/personalCabinet">Личный кабинет </Nav.Link>
            ) : (
              <Nav.Link href="/registration">Регистрация</Nav.Link>
            )}
            {props.store.isAuth ? (
              <Nav.Link href="/login" onClick={ExitButtonClick}>
                Выход
              </Nav.Link>
            ) : (
              <Nav.Link href="/login">Вход </Nav.Link>
            )}
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
};
export default observer(NavbarSite);
