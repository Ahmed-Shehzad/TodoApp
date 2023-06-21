import { FC } from "react";
import { Button } from "react-bootstrap";
import { TodoCurrentCounterProps } from "../types";

const TodoActiveCounter: FC<TodoCurrentCounterProps> = ({
  todos,
  onCurrentTodoListChanged,
}) => {
  const activeTodos = todos.filter((item) => !item.isDone);
  return (
    <Button
      variant="info"
      onClick={() => {
        onCurrentTodoListChanged(activeTodos);
      }}
    >
      Active: {activeTodos.length}
    </Button>
  );
};

export default TodoActiveCounter;
