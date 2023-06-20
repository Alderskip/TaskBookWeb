import React from "react";

export const Footer: React.FC = () => {
  return (
    <footer className="footer mt-auto py-3 bg-light">
      <div className="container">
      <ul className="nav justify-content-center border-bottom pb-3 mb-3">
        <li className="nav-item">
          <a href="/" className="nav-link px-2 text-muted">
            Главная страница
          </a>
        </li>
        <li className="nav-item">
          <a  href="http://ptaskbook.com/ru/" className="nav-link px-2 text-muted">
            Задачник
          </a>
        </li>
        <li className="nav-item">
          <a href="/FAQ" className="nav-link px-2 text-muted">
            Инструкция
          </a>
        </li>
        <li className="nav-item">
          <a href="https://simplecodesoftware.com/" className="nav-link px-2 text-muted">
            Разработчики
          </a>
        </li>
      </ul>
      <p className="text-center text-muted">© 2022 SimpleCode, Inc</p>
      </div>
    </footer>
  );
};

/*
<footer className="footer fixed-bottom ">
      <div className="container">
        <div className="row">
          <div className="col l6 s12">
            <h5 className="">О разработчиках</h5>
            <p className=" grey-text text-lighten-4">
              <h1 className="footerLogo">
                SimpleCode
                <br />
              </h1>
              Наша миссия состоит в том, чтобы предоставлять нашим клиентам
              наилучшие услуги, принимая во внимание будущее их проекта.
              <br />
              Our mission is to provide the best services to our clients by
              taking into account the future of their project.
            </p>
          </div>
          <div className="col l4 offset-l2 s12">
            <h5 className="white-text">Cсылки/Links</h5>
            <ul>
              <li>
                <a
                  className="grey-text text-lighten-3"
                  href="https://simplecodesoftware.com/"
                >
                  Разработичики
                </a>
              </li>
              <li>
                <a
                  className="grey-text text-lighten-3"
                  href="http://ptaskbook.com/ru/"
                >
                  Официальный сайт задачника
                </a>
              </li>
            </ul>
          </div>
        </div>
      </div>
      <div className="footer-copyright">
        <div className="container">© 2022 SimpleCode</div>
      </div>
    </footer>
*/
