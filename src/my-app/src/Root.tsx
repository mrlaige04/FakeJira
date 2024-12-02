import { Navigate, Route, HashRouter as Router, Routes } from "react-router-dom";
import { App } from "./App";
import { UsersPage } from "./pages/UsersPage";
import { TasksPage } from "./pages/TasksPage";
import { TimePage } from "./pages/TimePage";

export const Root = () => (
  <Router>
    <Routes>
      <Route path="/" element={<App />}>
        <Route path="users" element={<UsersPage />}/>
        <Route path="tasks" element={<TasksPage />}/>
        <Route path="times" element={<TimePage />}/>
      </Route>
      
      <Route path="*" element={<Navigate to="/users" replace />} />
    </Routes>
  </Router>
);