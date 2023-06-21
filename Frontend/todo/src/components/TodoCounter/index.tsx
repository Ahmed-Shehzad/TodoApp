import { FC } from "react";
import { Col, Row } from "react-bootstrap";
import TodoActiveCounter from "./Active";
import TodoAllCounter from "./All";
import TodoCompletedCounter from "./Completed";
import TodoOverDueCounter from "./OverDue";
import TodoRemainingCounter from "./Remaining";
import { TodoCurrentCounterProps } from "./types";

const TodoCounter: FC<TodoCurrentCounterProps> = ({
  todos,
  onCurrentTodoListChanged,
}) => {
  return (
    <Row className="border rounded-3 border-3">
      <Col>
        <TodoRemainingCounter todos={todos} />
      </Col>
      <Col>
        <TodoAllCounter
          todos={todos}
          onCurrentTodoListChanged={onCurrentTodoListChanged}
        />
      </Col>
      <Col>
        <TodoActiveCounter
          todos={todos}
          onCurrentTodoListChanged={onCurrentTodoListChanged}
        />
      </Col>
      <Col>
        <TodoOverDueCounter
          todos={todos}
          onCurrentTodoListChanged={onCurrentTodoListChanged}
        />
      </Col>
      <Col>
        <TodoCompletedCounter
          todos={todos}
          onCurrentTodoListChanged={onCurrentTodoListChanged}
        />
      </Col>
    </Row>
  );
};

export default TodoCounter;
