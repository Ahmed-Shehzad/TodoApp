import { FC } from "react";
import { Button } from "react-bootstrap";
import { TodoCurrentCounterProps } from "../types";

const TodoCompletedCounter: FC<TodoCurrentCounterProps> = ({
  todos,
  onCurrentTodoListChanged,
}) => {
  const completedTodos = todos.filter((item) => item.isDone && !item.isOverdue);
  return (
    <Button
      variant="success"
      onClick={() => {
        onCurrentTodoListChanged(completedTodos);
      }}
    >
      Completed: {completedTodos.length}
    </Button>
  );
};

export default TodoCompletedCounter;
