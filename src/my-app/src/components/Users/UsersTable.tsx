import React from 'react';
import { User } from '../../types/User';
import { UserLink } from './UserLink';

type Props = {
  users: User[];
};

export const UsersTable: React.FC<Props> = ({ users }) => {
  return (
    <table className="table is-striped is-hoverable is-narrow is-fullwidth">
      <thead>
        <tr>
          <th>First Name</th>
          <th>Last Name</th>
          <th>Email</th>
          <th>Department</th>
          <th>Edit</th>
          <th>Delete</th>
        </tr>
      </thead>

      <tbody>
        {users.map(user => (
          <UserLink key={user.id} user={user} />
        ))}
      </tbody>
    </table>
  );
};
