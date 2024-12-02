import classNames from 'classnames';
import { NavLink } from 'react-router-dom';

const getClass = ({ isActive }: { isActive: boolean }) => {
  return classNames('navbar-item', {
    'has-background-grey-lighter': isActive,
  });
};

export const Navbar = () => {
  return (
    <nav
      data-cy="nav"
      className="navbar is-fixed-top has-shadow"
      role="navigation"
      aria-label="main navigation"
    >
      <div className="container">
        <div className="navbar-brand">
          <NavLink to="/users" className={getClass}>
            Users
          </NavLink>

          <NavLink className={getClass} to="/tasks">
            Tasks
          </NavLink>

          <NavLink className={getClass} to="/times">
            Times
          </NavLink>
        </div>
      </div>
    </nav>
  );
};
