import { Task } from '../types/Task';
import { client } from '../utils/fetchClient';

const baseUrl = process.env.REACT_APP_BASE_URL_TASKS;

export const getTasks = () => {
  return client.get<Task[]>(`${baseUrl}/tasks`);
};

export const getTask = (id: number) => {
  return client.get<Task>(`${baseUrl}/tasks/${id}`);
};

export const postTask = (data: Omit<Task, 'id' | 'status'>) => {
  return client.post<Task>(`${baseUrl}/tasks`, data);
};

export const updateTask = (id: number, data: Omit<Task, 'id'>) => {
  return client.patch<Task>(`${baseUrl}/tasks/${id}`, data)
};

export const deleteTask = (id: number) => {
  return client.delete(`${baseUrl}/tasks/${id}`);
};

