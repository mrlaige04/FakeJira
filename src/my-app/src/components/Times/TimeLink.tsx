import React, { useEffect, useState } from 'react';
import { Time } from '../../types/Time';
import { User } from '../../types/User';
import { Task } from '../../types/Task';
import { useAppDispatch, useAppSelector } from '../../app/hooks';
import { deleteTime } from '../../api/times';
import * as timesActions from '../../features/times/timesSlice';

type Props = {
  time: Time;
};

export const TimeLink: React.FC<Props> = ({ time }) => {
  const { userId, taskId, date, start, end } = time;

  const { users } = useAppSelector(state => state.users);
  const { tasks } = useAppSelector(state => state.tasks);
  const dispatch = useAppDispatch();

  const [assignee, setAssignee] = useState<User | null>(null);
  const [task, setTask] = useState<Task | null>(null);

  useEffect(() => {
    if (userId) {
      setAssignee(users.find(user => user.id === userId) || null);
      // getUser(userId).then(user => setAssignee(user));
    }

    if (taskId) {
      setTask(tasks.find(task => task.id === taskId) || null);
    }

 
    // getTask(taskId).then(task => setTask(task));
  }, [users, userId, taskId, tasks]);

  const handleDeleteTime = () => {
    deleteTime(time.taskId, time.id).then(() => dispatch(timesActions.deleteTime(time)))
  };

  return (
    <tr>
      <td>{date}</td>
      <td>{`${start.slice(0,5)} - ${end.slice(0,5)}`}</td>
      <td>{task ? task.name : '-'}</td>
      <td>{assignee ? `${assignee?.firstName} ${assignee?.lastName}` : '-'}</td>
      <td onClick={() => dispatch(timesActions.addSelectedTime(time))}>
        <i className="fa-solid fa-pen-to-square"></i>
      </td>
      <td onClick={handleDeleteTime}><i className="fa-solid fa-trash"></i></td>
    </tr>
  );
};
