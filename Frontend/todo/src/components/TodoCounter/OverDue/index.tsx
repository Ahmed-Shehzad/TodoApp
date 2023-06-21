import { FC } from "react";
import { Button } from "react-bootstrap";
import { TodoCurrentCounterProps } from "../types";

const TodoOverDueCounter: FC<TodoCurrentCounterProps> = ({
  todos,
  onCurrentTodoListChanged,
}) => {
  const overDueTodos = todos.filter((item) => item.isOverdue && !item.isDone);

  return (
    <Button
      variant="warning"
      onClick={() => {
        onCurrentTodoListChanged(overDueTodos);
      }}
    >
      Over Due: {overDueTodos.length}
    </Button>
  );
};

export default TodoOverDueCounter;
