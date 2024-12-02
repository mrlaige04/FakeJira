import React from 'react';
import { TimeLink } from './TimeLink';
import { Time } from '../../types/Time';

type Props = {
  times: Time[];
};

export const TimeTable: React.FC<Props> = ({ times }) => {
  return (
    <table className="table is-striped is-hoverable is-narrow is-fullwidth">
      <thead>
        <tr>
          <th>Date</th>
          <th>Time</th>
          <th>Task Name</th>
          <th>Assignee</th>
          <th>Edit</th>
          <th>Delete</th>
        </tr>
      </thead>

      <tbody>
        {times.map(time => (
          <TimeLink key={time.id} time={time} />
        ))}
      </tbody>
    </table>
  );
};
