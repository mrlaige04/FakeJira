import { TaskWithTimeLogs } from '../types/Task';
import { Time } from '../types/Time';
import { client } from '../utils/fetchClient';

const baseUrl = process.env.REACT_APP_BASE_URL_TASKS;

export const getTasksWithTimeLogs = () => {
  return client.get<TaskWithTimeLogs[]>(`${baseUrl}/tasks/times`);
};

export const postTime = (taskId: number, data: Omit<Time, 'taskId' | 'id'>) => {
  return client.post<Time>(`${baseUrl}/tasks/${taskId}/times`, data);
}

export const updateTime = (taskId: number, timeId: number,  data: Omit<Time, 'taskId' | 'id' | 'userId'>) => {
  return client.patch<Time>(`${baseUrl}/tasks/${taskId}/times/${timeId}`, data)
};

export const deleteTime = (taskId: number, timeId: number) => {
  return client.delete(`${baseUrl}/tasks/${taskId}/times/${timeId}`);
};