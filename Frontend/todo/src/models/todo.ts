export interface IRoot {}

export interface Todo extends IRoot {
  id: string;
  title: string;
  description?: string;
  deadline: Deadline;
  isDone: boolean;
  isOverdue: boolean;
  updatedAt?: Date;
}

interface Deadline {
  from: Date;
  to: Date;
}
