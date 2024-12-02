import { UsersModal } from "../components/Users/UsersModal";
import { UsersTable } from "../components/Users/UsersTable";
import { useAppDispatch, useAppSelector } from "../app/hooks";
import * as usersActions from '../features/users/usersSlice';
import { Loader } from "../components/Loader";

export const UsersPage = () => {
  const dispatch = useAppDispatch();
  const { users, selectedUser, loading, error } = useAppSelector(state => state.users);
  
  const handleCreateBtnClick = () => {
    dispatch(usersActions.addSelectedUser({
      id: '',
      firstName: '',
      lastName: '',
      email: '',
      department: ''
    }))
  }

  return (
    <>
      <div className="block">
        <div className="columns is-desktop is-flex is-justify-content-space-between is-align-items-center mt-4 mb-4">
          <h1 className="title">Users Page</h1>
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

      {!loading && !error && users.length === 0 && (
        <div className="notification is-warning">
          No users yet
        </div>
      )}

      {!loading && !error && users.length > 0 && (
        <div className="block">
          <div className="box table-container">
            <UsersTable users={users} />
          </div>
        </div>
      )}
      

      {selectedUser && <UsersModal user={selectedUser} />}
    </>
  )
}