import { client } from '../utils/fetchClient';
import { User } from '../types/User';

const baseUrl = process.env.REACT_APP_BASE_URL_USERS;

export const getUsers = () => {
  return client.get<User[]>(`${baseUrl}/users`);
};

export const getUser = (id: string) => {
  return client.get<User>(`${baseUrl}/users/${id}`);
};

export const postUser = (data: Omit<User, 'id'>) => {
  return client.post<User>(`${baseUrl}/users`, data);
};

export const updateUser = (id: string, data: Omit<User, 'id'>) => {
  return client.put(`${baseUrl}/users/${id}`, data)
};

export const deleteUser = (id: string) => {
  return client.delete(`${baseUrl}/users/${id}`);
};

