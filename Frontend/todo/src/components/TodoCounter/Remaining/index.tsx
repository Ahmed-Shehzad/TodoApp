import { FC } from "react";
import { Button } from "react-bootstrap";
import { TodoCounterProps } from "../types";

const TodoRemainingCounter: FC<TodoCounterProps> = ({ todos }) => {
  const completedTodos = todos.filter((item) => item.isDone && !item.isOverdue);
  return (
    <Button disabled variant="light">
      Remaining: {todos.length - completedTodos.length}
    </Button>
  );
};

export default TodoRemainingCounter;
