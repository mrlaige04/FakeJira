import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit';
import { Task } from '../../types/Task';
import { getTasks } from '../../api/tasks';

type TasksState = {
  tasks: Task[];
  selectedTask: Task | null;
  loading: boolean;
  error: string;
};

const initialState: TasksState = {
  tasks: [],
  selectedTask: null,
  loading: false,
  error: '',
};

const tasksSlice = createSlice({
  name: 'tasks',
  initialState,
  reducers: {
    addTask: (state, action: PayloadAction<Task>) => {
      state.tasks.push(action.payload);
    },
    updateTask: (state, action: PayloadAction<Task>) => {
      const indexTask = state.tasks.findIndex(task => task.id === action.payload.id);

      state.tasks.splice(indexTask, 1, action.payload);
    },
    deleteTask: (state, action: PayloadAction<Task>) => {
      const indexTask = state.tasks.findIndex(task => task.id === action.payload.id);

      state.tasks.splice(indexTask, 1);
    },
    addSelectedTask: (state, action: PayloadAction<Task | null>) => {
      state.selectedTask = action.payload;
    },
  },
  extraReducers: builder => {
    builder.addCase(init.pending, state => {
      state.loading = true;
    });

    builder.addCase(init.fulfilled, (state, action) => {
      state.tasks = action.payload;
      state.loading = false;
    });

    builder.addCase(init.rejected, state => {
      state.error = 'Something went wrong!';
      state.loading = false;
    });
  },
});

export default tasksSlice;
export const { addTask, updateTask, deleteTask, addSelectedTask } = tasksSlice.actions;

export const init = createAsyncThunk('tasks/fetch', () => {
  return getTasks();
});
