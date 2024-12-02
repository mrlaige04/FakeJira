import React from 'react';
import { useAppDispatch } from '../../app/hooks';
import { User } from '../../types/User';
import { deleteUser } from '../../api/users';
import * as actionsUsers from '../../features/users/usersSlice';

type Props = {
  user: User;
};

export const UserLink: React.FC<Props> = ({ user }) => {
  const {
    firstName,
    lastName,
    email,
    department,
  } = user;

  const dispatch = useAppDispatch();

  const handleDeleteUser = () => {
    deleteUser(user.id).then(() => {
      dispatch(actionsUsers.deleteUser(user))
    })
  };

  return (
    <tr>
      <td>{firstName}</td>
      <td>{lastName}</td>
      <td>{email}</td>
      <td>{department}</td>
      <td onClick={() => dispatch(actionsUsers.addSelectedUser(user))}>
        <i className="fa-solid fa-user-pen"></i>
      </td>
      <td onClick={handleDeleteUser}><i className="fa-solid fa-trash"></i></td>
    </tr>
  );
};
