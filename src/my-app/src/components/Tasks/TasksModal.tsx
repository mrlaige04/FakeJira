import React, { useState } from "react";
import * as tasksActions from '../../features/tasks/tasksSlice';
import { useAppDispatch, useAppSelector } from "../../app/hooks";
import { Task } from "../../types/Task";
import classNames from "classnames";
import { postTask, updateTask } from "../../api/tasks";

type Props = {
  task: Task;
};

type FormValues = Omit<Task, 'id' | 'assigneeId'> & { 
  assigneeId: string;
};

type FormErrors = Partial<Record<keyof FormValues, string>>;

function validate({ name, priority, status, assigneeId }: FormValues): FormErrors {
  const errors: FormErrors = {};

  if (!name) {
    errors.name = 'Title is required';
  }

  if (!errors.name && name.length < 2) {
    errors.name = 'Title must be at least 2 characters';
  }

  if (+priority < 1 || +priority > 4) {
    errors.priority = 'Priority must by from 1 to 4';
  }

  if (+status < 1 || +status > 5) {
    errors.priority = 'Status must by from 1 to 5';
  }

  if (!assigneeId) {
    errors.assigneeId = 'Assignee is required';
  }

  return errors;
};

export const TasksModal: React.FC<Props> = ({ task }) => {
  const { users } = useAppSelector(state => state.users);
  const dispatch = useAppDispatch();

  const [values, setValues] = useState<FormValues>({
    ...task, 
    assigneeId: task.assigneeId === null ? '' : task.assigneeId, 
    status: task.status + 1, 
    priority: task.priority + 1,
  });
  const [errors, setErrors] = useState<FormErrors>({});

  function handleChange(
    event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>
  ) {
    const { name, value } = event.target;
  
    setValues(currentValues => ({
      ...currentValues,
      [name]: value,
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

    if (task.id === 0) {
      postTask({
        name: values.name,
        description: values.description,
        assigneeId: !values.assigneeId ? null : values.assigneeId,
        priority: +values.priority - 1,
      })
        .then((task) => {
          dispatch(tasksActions.addTask(task));
        })
    }else {
      updateTask(task.id, {
        name: values.name,
        description: values.description,
        assigneeId: !values.assigneeId ? null : values.assigneeId,
        priority: +values.priority - 1,
        status: +values.status - 1,
      })
        .then((task) => {   
          dispatch(tasksActions.updateTask(task));
        })
        .catch((error) => {
          console.error('Update failed:', error);
        });
    }

    handleReset();
  }

  const handleReset = () => {
    dispatch(tasksActions.addSelectedTask(null));
  }

  return (
    <div className="modal is-active">
      <div className="modal-background" />
        <div className="modal-card">
          <header className="modal-card-head">
            <div className="modal-card-title has-text-weight-medium">
              {task.id !== 0 ? 'Update' : 'Create'} task
            </div>
          </header>

          <div className="modal-card-body">
            <form onSubmit={handleSubmit} onReset={handleReset} noValidate>
              <div className="field">
                <label className="label">Title</label>
                <div className="control has-icons-right">
                  <input 
                    name="name"
                    className={classNames('input', { 'is-danger': errors.name })} 
                    type="text" 
                    placeholder="Title" 
                    value={values.name} 
                    onChange={handleChange}
                  />

                  {errors.name && (
                    <span className="icon is-small is-right">
                      <i className="fas fa-exclamation-triangle"></i>
                    </span>
                  )}
                </div>

                {errors.name && (
                  <p className="help is-danger">{errors.name}</p>
                )}
              </div>

              <div className="field">
                <label className="label">Description</label>
                <div className="control">
                  <textarea 
                    name="description"
                    className="textarea"
                    placeholder="Description"
                    value={values.description} 
                    onChange={handleChange}
                  />
                </div>
              </div>

              <div className="field">
                <label className="label">Assignee</label>
                <div className="control">
                  <div className={classNames('select', { 'is-danger': errors.assigneeId })} >
                    <select name="assigneeId" value={values.assigneeId} onChange={handleChange}>
                      <option value={''} disabled>Without Assignee</option>
                      {users.map(user => (
                        <option key={user.id} value={user.id}>
                          {`${user.firstName} ${user.lastName}`}
                        </option>
                      ))}
                    </select>
                  </div>

                  {errors.assigneeId && (
                    <p className="help is-danger">{errors.assigneeId}</p>
                  )}
                </div>
              </div>

              <div className="field">
                <label className="label">Priority</label>
                <div className="control">
                  <input 
                    name="priority"
                    className={classNames('input', { 'is-danger': errors.priority })}
                    type="number" 
                    placeholder="Priority" 
                    value={values.priority} 
                    min="1"
                    max='4'
                    onChange={handleChange}
                  />
                </div>

                {errors.priority && (
                  <p className="help is-danger">{errors.priority}</p>
                )}
              </div>

              {task.id !== 0 && (<div className="field">
                <label className="label">Status</label>
                <div className="control">
                  <input 
                    name="status"
                    className={classNames('input', { 'is-danger': errors.status })}
                    type="number" 
                    placeholder="Status" 
                    value={values.status} 
                    min="1"
                    max='5'
                    onChange={handleChange}
                  />
                </div>

                {errors.status && (
                  <p className="help is-danger">{errors.status}</p>
                )}
              </div>)}

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
}