import { Todo } from "@/models/todo";

export interface TodoCounterProps {
  todos: Todo[];
}

export interface TodoCurrentCounterProps extends TodoCounterProps {
  onCurrentTodoListChanged: (todos: Todo[]) => void;
}
