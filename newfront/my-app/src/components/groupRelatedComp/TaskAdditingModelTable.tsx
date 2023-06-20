
import { useEffect, useState } from "react";
import { Button, Form, Table, ToggleButton } from "react-bootstrap";
import IGroup from "../../models/groupRelatedInterfaces/IGroup";
import IGroupTask from "../../models/groupRelatedInterfaces/IGroupTask";
import IStudyTask from "../../models/studyTaskRelatedInterfaces/IStudyTask";
import GroupService from "../../services/GroupService";
import parse from "html-react-parser";
interface ITaskAdditinModelTableProps {
  group: IGroup;
  studyTaskGroupId: number;
}
const TaskAdditinModelTable = (props: ITaskAdditinModelTableProps) => {
  const [studyTasks, setStudyTasks] = useState<IGroupTask[]>([]);
  let buttonState: boolean[];

  useEffect(() => {
    const getGroupTasksAddingModel = async () => {
      setStudyTasks(
        await GroupService.GetGroupTasksAddingModel(
          Number(props.group.Id),
          Number(props.studyTaskGroupId)
        )
      );
    };

    getGroupTasksAddingModel();
  }, []);

  const getGroupTasksAddingModel = async () => {
    setStudyTasks(
      await GroupService.GetGroupTasksAddingModel(
        Number(props.group.Id),
        Number(props.studyTaskGroupId)
      )
    );
  };

  async function AddTask(
    studyTask: IStudyTask,
    isMandatory: string,
    isAutoGraded: string,
    taskPointValue: string
  ) {
    console.log(Number(
        (document.getElementById(isMandatory) as HTMLSelectElement).selectedIndex
      ))
    await GroupService.AddTaskToGroupByTaskAddingModel(
      studyTask.Id,
      

      Boolean(
        Number(
          (document.getElementById(isMandatory) as HTMLSelectElement).value
        )
      ),
      Boolean(
        Number(
          (document.getElementById(isAutoGraded) as HTMLSelectElement).value
        )
      ),
      Number(
        (document.getElementById(taskPointValue) as HTMLInputElement).value
      ),
      props.group.Id
    );
    await getGroupTasksAddingModel();
  }

  return (
    <Table  striped bordered hover className="align-middle">
      <thead className="fs-6 text-wrap lh-1 align-middle">
        <tr>
          <th >Задание</th>
          <th>Описание задания</th>
          <th>Обязательно к выполнению</th>
          <th>Ставить оценку автоматически</th>
          <th >Максимальное кол-во баллов за задание</th>
        </tr>
      </thead>

      <tbody>
        {studyTasks?.map((task, key = task.Id) => (
          <tr>
            <td className="fw-semibold">{task.studyTask.taskName}</td>

            <td className="text-break">{ parse(task.studyTask.taskDesc)}</td>
            <td>
              <Form.Select
                aria-label="Default select example"
                defaultValue={task.isMandatory ? "1" : "0"}
                id={`${props.group.Id} ${task.studyTask.Id}-mand`}
              >
                <option value={"1"}>Да</option>
                <option value={"0"}>Нет</option>
              </Form.Select>
            </td>

            <td>
              <Form.Select
                aria-label="Default select example"
                defaultValue={task.isAutoGraded ? "1" : "0"}
                id={`${props.group.Id} ${task.studyTask.Id}-autogr`}
              >
                <option value={"1"}>Да</option>
                <option value={"0"}>Нет</option>
              </Form.Select>
            </td>

            {props.group.gradeSys === "credit" ? (
              <></>
            ) : (
              <td>
                <Form.Control
                  type="number"
                  id={`${props.group.Id} ${task.studyTask.Id}-pointvalue`}
                  placeholder={task.maxTaskPointValueInGroup.toString()}
                />
              </td>
            )}

            <td>
              <Button
                id={`${props.group.Id} ${task.studyTask.Id}-button`}
                onClick={() =>
                  AddTask(
                    task.studyTask,
                    `${props.group.Id} ${task.studyTask.Id}-mand`,
                    `${props.group.Id} ${task.studyTask.Id}-autogr`,
                    `${props.group.Id} ${task.studyTask.Id}-pointvalue`
                  )
                }
                variant={task.active===false?("primary"):("danger")}
              >
                {task.active === false ? <>Добавить</> : <>Удалить</>}
              </Button>
            </td>
          </tr>
        ))}
      </tbody>
    </Table>
  );
};
export default TaskAdditinModelTable;
