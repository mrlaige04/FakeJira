import { configureStore, ThunkAction, Action, combineSlices } from '@reduxjs/toolkit';
import usersSlice from '../features/users/usersSlice';
import tasksSlice from '../features/tasks/tasksSlice';
import trackedTimesSlice from '../features/times/timesSlice';

const rootReducer = combineSlices(usersSlice, tasksSlice, trackedTimesSlice);

export const store = configureStore({
  reducer: rootReducer,
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<ReturnType, RootState, unknown, Action<string>>;
