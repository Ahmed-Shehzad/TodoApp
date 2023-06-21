export interface Todo {
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
