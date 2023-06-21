import { FC } from "react";
import { Button } from "react-bootstrap";
import { TodoCurrentCounterProps } from "../types";

const TodoAllCounter: FC<TodoCurrentCounterProps> = ({
  todos,
  onCurrentTodoListChanged,
}) => {
  return (
    <Button
      variant="dark"
      onClick={() => {
        onCurrentTodoListChanged(todos);
      }}
    >
      All: {todos.length}
    </Button>
  );
};

export default TodoAllCounter;
