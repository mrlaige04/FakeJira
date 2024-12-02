/* eslint-disable no-useless-escape */
import React, { useState } from "react";
import { User } from "../../types/User";
import * as usersActions from '../../features/users/usersSlice';
import { useAppDispatch } from "../../app/hooks";
import classNames from "classnames";
import { postUser, updateUser } from "../../api/users";

type Props = {
  user: User;
};

type FormValues = Omit<User, 'id'>;
type FormErrors = Partial<Record<keyof FormValues, string>>;

function validate({ firstName, lastName, email, department }: FormValues): FormErrors {
  const errors: FormErrors = {};
  const emailRegex = /^((([0-9A-Za-z]{1}[-0-9A-z\.]{1,}[0-9A-Za-z]{1})|([0-9А-Яа-я]{1}[-0-9А-я\.]{1,}[0-9А-Яа-я]{1}))@([-A-Za-z]{1,}\.){1,2}[-A-Za-z]{2,})$/;

  if (!firstName) {
    errors.firstName = 'Name is required';
  }

  if (!errors.firstName && firstName.length < 2) {
    errors.firstName = 'Name must be at least 2 characters';
  }

  if (!lastName) {
    errors.lastName = 'Last name is required';
  }

  if (!errors.lastName && lastName.length < 2) {
    errors.lastName = 'Last name must be at least 2 characters';
  }

  if (!email) {
    errors.email = 'Email is required';
  }

  if (!errors.email && !emailRegex.test(email)) {
    errors.email = 'Email is no valid';
  }
  
  if (!department) {
    errors.department = 'Department is required';
  }

  return errors;
};

export const UsersModal: React.FC<Props> = ({ user }) => {
  const dispatch = useAppDispatch();

  const [values, setValues] = useState<FormValues>(user);
  const [errors, setErrors] = useState<FormErrors>({});
  
  function handleChange(event: React.ChangeEvent<HTMLInputElement>) {
    const { name, type, value, checked } = event.target;

    setValues(currentValues => ({
      ...currentValues,
      [name]: type === 'checkbox' ? checked : value,
    }));

    setErrors(currentErrors => {
      const copy = { ...currentErrors };
      delete copy[name as keyof FormValues];

      return copy;
    });
  }

  function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();

    const newErrors = validate(values);
    setErrors(newErrors);
    if (Object.keys(newErrors).length > 0) return;

    if (user.id === '') {
      postUser({
        firstName: values.firstName,
        lastName: values.lastName,
        email: values.email,
        department: values.department,
      })
        .then((user) => {
          dispatch(usersActions.addUser(user));
        })
    } else {
      updateUser(user.id, {
        firstName: values.firstName,
        lastName: values.lastName,
        email: values.email,
        department: values.department,
      })
        .then(() => {   
          dispatch(usersActions.updateUser({id: user.id, ...values}));
        })
        .catch((error) => {
          console.error('Update failed:', error);
        });
    }

    handleReset();
  }

  const handleReset = () => {
    dispatch(usersActions.addSelectedUser(null));
  }

  return (
    <div className="modal is-active">
      <div className="modal-background" />
        <div className="modal-card">
          <header className="modal-card-head">
            <div className="modal-card-title has-text-weight-medium">
              {user.id !== '' ? 'Update' : 'Create'} user
            </div>
          </header>

          <div className="modal-card-body">
            <form onSubmit={handleSubmit} onReset={handleReset} noValidate>
              <div className="field">
                <label className="label">First Name</label>
                <div className="control has-icons-left has-icons-right">
                  <input 
                    name="firstName"
                    className={classNames('input', { 'is-danger': errors.firstName })}
                    type="text" 
                    placeholder="First Name" 
                    value={values.firstName} 
                    onChange={handleChange}
                  />

                  <span className="icon is-small is-left">
                    <i className="fas fa-user"></i>
                  </span>

                  {errors.firstName && (
                    <span className="icon is-small is-right">
                      <i className="fas fa-exclamation-triangle"></i>
                    </span>
                  )}
                </div>

                {errors.firstName && (
                  <p className="help is-danger">{errors.firstName}</p>
                )}
              </div>

              <div className="field">
                <label className="label">Last Name</label>
                <div className="control has-icons-left has-icons-right">
                  <input 
                    name="lastName"
                    className={classNames('input', { 'is-danger': errors.lastName })}
                    type="text" 
                    placeholder="Last Name" 
                    value={values.lastName} 
                    onChange={handleChange}
                  />

                  <span className="icon is-small is-left">
                    <i className="fas fa-user"></i>
                  </span>

                  {errors.lastName && (
                    <span className="icon is-small is-right">
                      <i className="fas fa-exclamation-triangle"></i>
                    </span>
                  )}
                </div>

                {errors.lastName && (
                  <p className="help is-danger">{errors.lastName}</p>
                )}
              </div>

              <div className="field">
                <label className="label">Email</label>
                <div className="control has-icons-left has-icons-right">
                  <input 
                    name="email"
                    className={classNames('input', { 'is-danger': errors.email })}
                    type="email" 
                    placeholder="Email input" 
                    value={values.email} 
                    onChange={handleChange} 
                  />

                  <span className="icon is-small is-left">
                    <i className="fas fa-envelope"></i>
                  </span>

                  {errors.email && (
                    <span className="icon is-small is-right">
                      <i className="fas fa-exclamation-triangle"></i>
                    </span>
                  )}
                </div>

                {errors.email && (
                  <p className="help is-danger">{errors.email}</p>
                )}
              </div> 

              <div className="field">
                <label className="label">Department</label>
                <div className="control has-icons-left has-icons-right">
                  <input 
                    name="department"
                    className={classNames('input', { 'is-danger': errors.department })}
                    type="text" 
                    placeholder="Department input" 
                    value={values.department} 
                    onChange={handleChange} 
                  />

                  <span className="icon is-small is-left">
                    <i className="fas fa-envelope"></i>
                  </span>

                  {errors.department && (
                    <span className="icon is-small is-right">
                      <i className="fas fa-exclamation-triangle"></i>
                    </span>
                  )}
                </div>

                {errors.department && (
                  <p className="help is-danger">{errors.department}</p>
                )}
              </div>     

              <div className="field is-grouped is-grouped-right">
                <div className="control">
                  <button className="button is-link" type="submit">Save</button>
                </div>
                <div className="control">
                  <button className="button is-link is-light" type="reset">Cancel</button>
                </div>
              </div>
            </form>
          </div>
        </div>
    </div>
  );
};