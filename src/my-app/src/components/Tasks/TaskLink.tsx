import React, { useEffect, useState } from 'react';
import { Task } from '../../types/Task';
import { User } from '../../types/User';
import { deleteTask } from '../../api/tasks';
import { useAppDispatch, useAppSelector } from '../../app/hooks';
import * as tasksActions from '../../features/tasks/tasksSlice';
import * as actionsTimes from '../../features/times/timesSlice';

type Props = {
  task: Task;
};

export const TaskLink: React.FC<Props> = ({ task }) => {
  const { name, description, priority, status, assigneeId } = task;
  const { users } = useAppSelector(state => state.users);
  const dispatch = useAppDispatch();

  const [assignee, setAssignee] = useState<User | null>(null);

  useEffect(() => {
    if (!assigneeId) {
      return;
    }

    setAssignee(users.find(user => user.id === assigneeId) || null);

    // getUser(assigneeId).then(user => setAssignee(user));
  }, [users, assigneeId]);

  const handleCreateTrackedTime = () => {
    dispatch(actionsTimes.addSelectedTime({
      id: 0,
      userId: task.assigneeId || '',
      taskId: task.id,
      date: '',
      start: '',
      end: '',
    }))
  };

  
  const handleDeleteTask = () => {
    deleteTask(task.id).then(() => { 
      dispatch(tasksActions.deleteTask(task));
      dispatch(actionsTimes.init());
    })
  };

  return (
    <tr>
      <td>{name}</td>
      <td>{`${description.length > 100 ? description.slice(0, 100) + '...' : description}`}</td>
      <td>{status + 1}</td>
      <td>{priority + 1}</td>
      <td>{assignee ? `${assignee.firstName} ${assignee.lastName}` : '-'}</td>
      <td onClick={() => dispatch(tasksActions.addSelectedTask(task))}>
        <i className="fa-solid fa-pen-to-square"></i>
      </td>
      <td onClick={handleDeleteTask}><i className="fa-solid fa-trash"></i></td>
      <td onClick={handleCreateTrackedTime}><i className="fa-solid fa-plus"></i></td>
    </tr>
  );
};
