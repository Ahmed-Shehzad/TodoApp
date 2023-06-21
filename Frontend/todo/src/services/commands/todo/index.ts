export interface DeadlineCommand {
  from?: Date;
  to?: Date;
}
export interface CreateTodoCommand {
  title: string;
  description?: string;
  deadline: DeadlineCommand;
}

export interface UpdateTodoCommand {
  id: string;
  title?: string;
  description?: string;
  deadline?: DeadlineCommand;
  isDone?: boolean;
}

export interface DeleteTodoCommand {
  id: string;
}
