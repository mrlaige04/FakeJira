import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit';
import { Time } from '../../types/Time';
import { getTasksWithTimeLogs } from '../../api/times';
import { deleteTask } from '../tasks/tasksSlice';
import { Task } from '../../types/Task';

type TrackedTimesState = {
  times: Time[];
  selectedTime: Time | null;
  loading: boolean;
  error: string;
};

const initialState: TrackedTimesState = {
  times: [],
  selectedTime: null,
  loading: false,
  error: '',
};

const trackedTimesSlice = createSlice({
  name: 'trackedTimes',
  initialState,
  reducers: {
    addTime: (state, action: PayloadAction<Time>) => {
      state.times.push(action.payload);
    },
    updateTime: (state, action: PayloadAction<Time>) => {
      const indexTime = state.times.findIndex(time => time.id === action.payload.id);

      state.times.splice(indexTime, 1, action.payload);
    },
    deleteTime: (state, action: PayloadAction<Time>) => {
      const indexTime = state.times.findIndex(time => time.id === action.payload.id);

      state.times.splice(indexTime, 1);
    },
    addSelectedTime: (state, action: PayloadAction<Time | null>) => {
      state.selectedTime = action.payload;
    },
  },
  extraReducers: builder => {
    builder.addCase(init.pending, state => {
      state.loading = true;
    });

    builder.addCase(init.fulfilled, (state, action) => {
      const timesFromTasks:Time[] = [];

      for (const task of action.payload) {
        for (const time of task.timeLogs) {
          timesFromTasks.push({
            ...time,
            taskId: task.taskId,
          });
        }
      }

      state.times = timesFromTasks;
      state.loading = false;
    });

    builder.addCase(init.rejected, state => {
      state.error = 'Something went wrong!';
      state.loading = false;
    });

    builder.addCase(deleteTask, (state, action: PayloadAction<Task>) => {
      state.times = state.times.filter(time => time.taskId !== action.payload.id);
    });
  },
});

export default trackedTimesSlice;
export const { addTime, updateTime, deleteTime, addSelectedTime } = trackedTimesSlice.actions;

export const init = createAsyncThunk('trackedTimes/fetch', () => {
  return getTasksWithTimeLogs();
});
