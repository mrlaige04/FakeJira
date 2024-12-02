import { useAppDispatch, useAppSelector } from "../app/hooks";
import * as tasksActions from "../features/tasks/tasksSlice";
import { Loader } from "../components/Loader";
import { TasksModal } from "../components/Tasks/TasksModal";
import { TimeModal } from "../components/Times/TimeModal";
import { TasksTable } from "../components/Tasks/TasksTable";

export const TasksPage = () => {
  const { tasks, selectedTask, loading, error } = useAppSelector(state => state.tasks);
  const { selectedTime } = useAppSelector(state => state.trackedTimes);
  const dispatch = useAppDispatch();

  const handleCreateBtnClick = () => {
    dispatch(tasksActions.addSelectedTask({
      id: 0,
      name: '',
      description: '',
      assigneeId: null,
      status: 0,
      priority: 0,
    }))
  };

  return (
    <>
    <div className="block">
      <div className="columns is-desktop is-flex is-justify-content-space-between is-align-items-center mt-4 mb-4">
        <h1 className="title">Tasks Page</h1>
        <button className="button is-link is-light" onClick={handleCreateBtnClick}>
          <i className="fa-solid fa-plus"></i> Create
        </button>
    </div>

    </div>

    {loading && <Loader />}

    {!loading && error && (
      <div className="notification is-danger">
        {error}
      </div>
    )}

    {!loading && !error && tasks.length === 0 && (
      <div className="notification is-warning">
        No tasks yet
      </div>
    )}

    {!loading && !error && tasks.length > 0 && (
      <div className="block">
        <div className="box table-container">
          <TasksTable tasks={tasks} />
        </div>
      </div>
    )}
    

    {selectedTask && <TasksModal task={selectedTask} />}

    {selectedTime && <TimeModal time={selectedTime} />}
  </>
  )
}