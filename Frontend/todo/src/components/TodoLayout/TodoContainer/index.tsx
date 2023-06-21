import TodoCounter from "@/components/TodoCounter";
import TodoTable from "@/components/TodoTable";
import { Todo } from "@/models/todo";
import { FC, useEffect, useState } from "react";

interface TodoContainerProps {
  onTodoDelete: (todo: Todo) => Promise<void>;
  onTodoUpdate: (todo: Todo) => Promise<void>;
  todos: Todo[];
}

const TodoContainer: FC<TodoContainerProps> = ({
  todos,
  onTodoUpdate,
  onTodoDelete,
}) => {
  const [currentTodos, setCurrentTodos] = useState<Todo[]>([]);

  useEffect(() => {
    setCurrentTodos(todos);
  }, [todos]);

  const onCurrentTodoListChanged = (todos: Todo[]) => {
    setCurrentTodos(todos);
  };

  return (
    <>
      <TodoTable
        todos={currentTodos}
        onTodoDeleted={onTodoDelete}
        onTodoUpdated={onTodoUpdate}
      />

      <TodoCounter
        todos={todos}
        onCurrentTodoListChanged={onCurrentTodoListChanged}
      />
    </>
  );
};

export default TodoContainer;
