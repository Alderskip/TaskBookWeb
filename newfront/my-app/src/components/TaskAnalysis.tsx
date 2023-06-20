import { useEffect, useState } from "react";
import { Table, ToggleButton, ToggleButtonGroup } from "react-bootstrap";
import { useForm } from "react-hook-form";
import IStudyTask from "../models/studyTaskRelatedInterfaces/IStudyTask";
import parse from "html-react-parser";
interface ITaskAnalysisProps {
  studyTasks: IStudyTask[];
  completedTasks: IStudyTask[];
}

const TaskAnalysis = (props: ITaskAnalysisProps) => {
  let taskAnalysis = new Map();


  const [checkedButtons, setcheckedButtons] = useState<number[]>([
    1, 2, 3, 4, 5, 6,
  ]);
  taskAnalysis.set(
    "PrimaryAllTaskCount",
    props.studyTasks.filter((x) => x.studyTaskType === "Primary").length)
  useEffect(() => {
     taskAnalysis.set(
      "PrimaryAllTaskCount",
      props.studyTasks.filter((x) => x.studyTaskType === "Primary").length
    );
    taskAnalysis.set(
      "PrimaryCompletedTaskCount",
      props.completedTasks.filter((x) => x.studyTaskType === "Primary").length
    );

    taskAnalysis.set(
      "SecondaryAllTaskCount",
      props.studyTasks.filter((x) => x.studyTaskType === "Secondary").length
    );
    taskAnalysis.set(
      "SecondaryCompletedTaskCount",
      props.completedTasks.filter((x) => x.studyTaskType === "Secondary").length
    );

    taskAnalysis.set(
      "HardAllTaskCount",
      props.studyTasks.filter((x) => x.taskDifficulty === "Hard").length
    );
    taskAnalysis.set(
      "HardCompletedTaskCount",
      props.completedTasks.filter((x) => x.taskDifficulty === "Hard").length
    );

    taskAnalysis.set(
      "EasyAllTaskCount",
      props.studyTasks.filter((x) => x.taskDifficulty === "Easy").length
    );
    taskAnalysis.set(
      "EasyCompletedTaskCount",
      props.completedTasks.filter((x) => x.taskDifficulty === "Easy").length
    );

    taskAnalysis.set(
      "VariativeAllTaskCount",
      props.studyTasks.filter((x) => x.studyTaskType === "Variant").length
    );
    taskAnalysis.set(
      "VariativeCompletedTaskCount",
      props.completedTasks.filter((x) => x.studyTaskType === "Variant").length
    );
  }, []);

  const changeButtonState = (toChangeState: number) => {
    if (checkedButtons.includes(toChangeState)) {
      setcheckedButtons(checkedButtons.filter((x) => x !== toChangeState));
    } else {
      const newCheckedButtons = checkedButtons;
      newCheckedButtons.push(toChangeState);
      setcheckedButtons(newCheckedButtons);
    }
  };

  const getRusEquvalent = (textToTranslate: string) => {
    switch (textToTranslate) {
      case "Primary": {
        return "Основное";
      }
      case "Secondary": {
        return "Дополнительное";
      }
      case "Hard":
      {
        return "Сложное"
      }
      case "Easy":
      {
        return "Простое"
      }
      case "Variant":
      {
        return "Вариативное"
      }
      default:
        return ""
    }
  };

  const filterTasks = (tasks: IStudyTask[]) => {
    if (!checkedButtons.includes(1))
      tasks = tasks.filter((x) => x.studyTaskType === "Primary");
    if (!checkedButtons.includes(2))
      tasks = tasks.filter((x) => x.studyTaskType === "Secondary");
    if (!checkedButtons.includes(3))
      tasks = tasks.filter((x) => x.taskDifficulty === "Hard");
    if (!checkedButtons.includes(4))
      tasks = tasks.filter((x) => x.taskDifficulty === "Easy");
    if (!checkedButtons.includes(5))
      tasks = tasks.filter((x) => x.studyTaskType === "Variant");
    if (!checkedButtons.includes(6))
      tasks.forEach((x) => {
        if (props.completedTasks.includes(x)) tasks.filter((y) => y === x);
      });
    return tasks;
  };
  console.log(taskAnalysis.get("PrimaryAllTaskCount"))
  const TaskTable = (): JSX.Element => {
    return (
      <Table className="ms-auto" size="sm">
        <thead>
          <tr>
            <th>Задание</th>
            <th>Описание задания</th>
            <th>Тип задания</th>
            <th>Сложность задания</th>
            <th>Стандартное количество баллов за задание</th>
          </tr>
        </thead>
        <tbody>
          {filterTasks(props.studyTasks).map((task) => (
            <tr>
              <td>{task.taskName}</td>
              <td>{parse(task.taskDesc)}</td>
              <td>{getRusEquvalent(task.studyTaskType)}</td>
              <td>{getRusEquvalent(task.taskDifficulty)}</td>
              <td>{task.taskPointValue}</td>
            </tr>
          ))}
        </tbody>
      </Table>
    );
  };

  return (
    <Table striped bordered className="ms-auto">
      <thead>
        <tr>
          <th className="col-3">Краткая сводка</th>
          <th className="col-9">Подробная инфоция о каждом задании</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td className="ms-auto">
            <p>
              Основных заданий: {taskAnalysis.get("PrimaryAllTaskCount")} <br />
              Второстепенных заданий: {taskAnalysis.get(
                "SecondaryAllTaskCount"
              )}{" "}
              <br />
              Сложных заданий: {taskAnalysis.get("HardAllTaskCount")} <br />
              Простых заданий: {taskAnalysis.get("EasyAllTaskCount")} <br />
              Вариативных заданий: {taskAnalysis.get("VariativeAllTaskCount")}{" "}
              <br />
            </p>
          </td>
          <td>
            <ToggleButtonGroup
              type="checkbox"
              className="mb-2 mr-2 ml-2"
              size="sm"
              value={checkedButtons}
            >
              {props.completedTasks.length === 0 ? (
                <></>
              ) : (
                <ToggleButton
                  id="tbg-check-1"
                  variant="outline-primary"
                  value={6}
                  onChange={() => changeButtonState(6)}
                >
                  Показывать выполненные задания
                </ToggleButton>
              )}

              <ToggleButton
                id="tbg-check-1"
                variant="outline-primary"
                value={1}
                onChange={() => changeButtonState(1)}
              >
                Показывать Основные задания
              </ToggleButton>
              <ToggleButton
                id="tbg-check-2"
                variant="outline-primary"
                value={2}
                onChange={() => changeButtonState(2)}
              >
                Показывать второстепенные задания
              </ToggleButton>

              <ToggleButton
                id="tbg-check-3"
                variant="outline-danger"
                value={3}
                onChange={() => changeButtonState(3)}
              >
                Показывать сложные задания
              </ToggleButton>

              <ToggleButton
                id="tbg-check-4"
                variant="outline-success"
                value={4}
                size="sm"
                onChange={() => changeButtonState(4)}
              >
                Показывать простые задания
              </ToggleButton>
              <ToggleButton
                id="tbg-check-5"
                variant="outline-secondary"
                value={5}
                onChange={() => changeButtonState(5)}
              >
                Показывать вариантивные задания
              </ToggleButton>
            </ToggleButtonGroup>
            {TaskTable()}
          </td>

        </tr>
      </tbody>
    </Table>
  );
};
export default TaskAnalysis;
