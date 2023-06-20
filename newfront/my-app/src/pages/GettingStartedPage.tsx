import { Row, Col, Accordion as h1, Card } from "react-bootstrap";
import Container from "react-bootstrap/esm/Container";

export const GettingStartedPage: React.FC = () => {
  return (
    <Container>
      <Row>
        <Col>
          <Card className="bg-dark text-white">
            <Card.Img
              src={require("../resourses/FAQBackground.jpg")}
              alt="Card image"
              height="200px"
            />
            <Card.ImgOverlay>
              <Card.Title className="FaqText">
                Начало работы с задачником!
              </Card.Title>
            </Card.ImgOverlay>
          </Card>
        </Col>
      </Row>
      <Row>
        <Col>
          <h1>Установка электронного задачника</h1>
          <p>
            Для начала работы с задачником, вам необходимо скачать его с сайта{" "}
            <a
              href="http://ptaskbook.com/ru/download.php"
              className="link-primary"
            >
              http://ptaskbook.com/ru/download.php
            </a>{" "}
            Перейдите на страницу скачивание дистрибутивов и выберите последнюю
            Complete версию в формате exe или zip. Запустите exe файл и следуйте
            инструкции установщика. Если вы скачали zip файл то предварительно
            распакуйте его в удобную для вас папку. После корректной установки у
            вас должны появится программы: PT4Load, PT4Teach, PT4Results.
          </p>

          <h1 id="configurate-taskbook" >Настройка задачника<a className="anchor-link" href="#configurate-taskbook"></a></h1>
          <p>
            После установки перейдите в папку с задачником или создайте новую
            папку и добавьте туда ярлык PT4Load. Перейдите на страницу с
            информацией о группе или курсе и нажмите на "Настройка локального задачника" скачайте оттуда файл access.dat и results.dat.
            Далее скопируйте их в папку, в которой вы планируете работать (но не более одного файла access.dat в
            одной папке). Запустите PT4Load.exe и если программа автоматически
            определила ваш ник, значит настройка прошла успешно. Также вы можете
            воспользоваться программой для автоматической настройки всех ваших
            групп и курсов (инструкция ниже).
          </p>

          <h1 id="work-with-taskbook" >Работа с задачником <a className="anchor-link" href="#work-with-taskbook"></a></h1>
          <p>
            После установки и первичной настройки запустите Pt4Load.exe. Здесь
            вы можете выбрать интересующее вас задание, выбрать каталог, в
            котором будут храниться тексты ваших программ, а также увидеть имя
            пользователя, под которым вы записаны в задачнике. При запуске
            задания вы увидите шаблон созданный задачником при компиляции
            которого вам откроется окно с заданием и текущей стадией выполнения,
            а также пример верного решения. Чтобы узнать, как взаимодействовать
            с задачников на выброном языке нажмите на кнопку "вопроса" в окне
            задачника и перейдите на вкладку «ввод-вывод», либо прочитайте
            "взаимодействие с задачником" в информации о курсе или группе на
            сайте.
          </p>

          <h1>О PT4Results</h1>
          <p>
            Данная программа устанавливается на ваш компьютер вместе с
            электронным задачником. В ней вы можете просмотреть подробную
            информацию о вашем выполнении заданий.
          </p>

          <h1>Программа автоматической настройки "Taskbook Helper"</h1>
          <p>
            Данную программа вы можете скачать по ссылке: "ссылка". С её помощью
            вы сможете автоматически настроить (скачать необходимые access.dat
            файлы) все ваши группы и курсы сразу после установки электронного
            задачника.
          </p>
        </Col>
      </Row>
    </Container>
  );
};
