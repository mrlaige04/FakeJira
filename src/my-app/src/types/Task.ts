import { Time } from "./Time";

export interface Task {
  id: number;
  name: string;
  description: string;
  assigneeId: string | null;
  priority: number;
  status: number;
};

export interface TaskWithTimeLogs extends Omit<Task, 'id'> {
  taskId: number;
  timeLogs: Omit<Time, 'taskId'>[];
};