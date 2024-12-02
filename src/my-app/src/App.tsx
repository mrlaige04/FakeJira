import { Navbar } from './components/Navbar';

import './App.css';
import { Outlet } from 'react-router-dom';
import { useEffect } from 'react';
import { useAppDispatch } from './app/hooks';
import * as usersActions from './features/users/usersSlice';
import * as tasksActions from "./features/tasks/tasksSlice";
import * as timeActions from "./features/times/timesSlice";

export const App = () => {
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(usersActions.init());
    dispatch(tasksActions.init());
    dispatch(timeActions.init());
  }, [dispatch]);

  return (
    <div data-cy="app">
      <Navbar />

      <div className="section">
        <div className="container">
          <Outlet />
        </div>
      </div>
    </div>
  );
};
