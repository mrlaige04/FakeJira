import React, { useState } from "react";
import { useAppDispatch } from "../../app/hooks";
import { Time } from "../../types/Time";
import { addSelectedTime } from "../../features/times/timesSlice";
import classNames from "classnames";
import { postTime, updateTime } from "../../api/times";
import * as timesActions from '../../features/times/timesSlice';

type Props = {
  time: Time;
};

type FormValues = Pick<Time, 'date' | 'start' | 'end'>;
type FormErrors = Partial<Record<keyof FormValues, string>>;

function validate({ date, start, end }: FormValues): FormErrors {
  const errors: FormErrors = {};

  if (!date) {
    errors.date = 'Date is required';
  }

  if (!start) {
    errors.start = 'Start time is required';
  }

  if (!end) {
    errors.end = 'End time is required';
  }

  if (+start.split(':').join('').slice(0,4) >= +end.split(':').join('').slice(0,4)) {
    errors.end = 'End time must by bigger than start time';
  }

  return errors;
};

export const TimeModal: React.FC<Props> = ({ time }) => {
  const dispatch = useAppDispatch();
  const [values, setValues] = useState<FormValues>({
    date: time.date,
    start: time.start,
    end: time.end,
  });
  const [errors, setErrors] = useState<FormErrors>({});
  
  function handleChange(
    event: React.ChangeEvent<HTMLInputElement>
  ) {
    const { name, value } = event.target;
  
    setValues(currentValues => ({
      ...currentValues,
      [name]: value,
    }));
  
    setErrors(currentErrors => {
      const copy = { ...currentErrors };
      
      if(name === 'start') {
        delete copy.end;
      } else if(name === 'end') {
        delete copy.start;
      }

      delete copy[name as keyof FormValues];
      return copy;
    });
  }

  function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();

    const newErrors = validate(values);
    setErrors(newErrors);
    if (Object.keys(newErrors).length > 0) return;

    if (time.id === 0) {
      postTime(time.taskId, {
        date: values.date,
        start: values.start,
        end: values.end,
        userId: time.userId,
      })
        .then((timeFromServer) => {
          dispatch(timesActions.addTime({...timeFromServer, userId: time.userId}));
        })
    }else {
      updateTime(time.taskId, time.id, values)
        .then((time) => {   
          dispatch(timesActions.updateTime(time));
        })
        .catch((error) => {
          console.error('Update failed:', error);
        });
    }

    handleReset();
  }

  const handleReset = () => {
    dispatch(addSelectedTime(null));
  }

  return (
    <div className="modal is-active">
      <div className="modal-background" />
        <div className="modal-card">
          <header className="modal-card-head">
            <div className="modal-card-title has-text-weight-medium">
              {time.id !== 0 ? 'Update' : 'Create'} Tracked Time
            </div>
          </header>

          <div className="modal-card-body">
            <form onSubmit={handleSubmit} onReset={handleReset} noValidate>
              <div className="field">
                <label className="label">Date</label>
                <div className="control has-icons-right">
                  <input
                    name="date"
                    className={classNames('input', { 'is-danger': errors.date })} 
                    type="date"
                    value={values.date}
                    onChange={handleChange}
                  />

                  {errors.date && (
                    <span className="icon is-small is-right">
                      <i className="fas fa-exclamation-triangle"></i>
                    </span>
                  )}
                </div>

                {errors.date && (
                  <p className="help is-danger">{errors.date}</p>
                )}
              </div>

              <div className="field">
                <label className="label">Start Time</label>
                <div className="control has-icons-right">
                  <input
                    name="start"
                    className={classNames('input', { 'is-danger': errors.start })} 
                    type="time"
                    value={values.start}
                    onChange={handleChange}
                  />

                  {errors.start && (
                    <span className="icon is-small is-right">
                      <i className="fas fa-exclamation-triangle"></i>
                    </span>
                  )}
                </div>

                {errors.start && (
                  <p className="help is-danger">{errors.start}</p>
                )}
              </div>

              <div className="field">
                <label className="label">End Time</label>
                <div className="control has-icons-right">
                  <input
                    name="end"
                    className={classNames('input', { 'is-danger': errors.end })} 
                    type="time"
                    value={values.end}
                    onChange={handleChange}
                  />

                  {errors.end && (
                    <span className="icon is-small is-right">
                      <i className="fas fa-exclamation-triangle"></i>
                    </span>
                  )}
                </div>

                {errors.end && (
                  <p className="help is-danger">{errors.end}</p>
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
}