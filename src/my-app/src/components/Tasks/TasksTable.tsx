import React from 'react';
import { Task } from '../../types/Task';
import { TaskLink } from './TaskLink';

type Props = {
  tasks: Task[];
};

export const TasksTable: React.FC<Props> = ({ tasks }) => {
  return (
    <table className="table is-striped is-hoverable is-narrow is-fullwidth">
      <thead>
        <tr>
          <th>Title</th>
          <th>Description</th>
          <th>Status</th>
          <th>Priority</th>
          <th>Assignee</th>
          <th>Edit</th>
          <th>Delete</th>
          <th>Add time</th>
        </tr>
      </thead>

      <tbody>
        {tasks.map(task => (
          <TaskLink key={task.id} task={task} />
        ))}
      </tbody>
    </table>
  );
};
