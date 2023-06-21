import { Todo } from "@/models/todo";
import { DateTimeToFormattedString } from "@/utils";
import { FC } from "react";
import { Button, Col, Container, Form, Row } from "react-bootstrap";
import ToggleButton from "../ToggleButton";

interface TodoTableProps {
  todos: Todo[];
  onTodoDeleted: (todo: Todo) => Promise<void>;
  onTodoUpdated: (todo: Todo) => Promise<void>;
}

const TodoTable: FC<TodoTableProps> = ({
  todos,
  onTodoUpdated,
  onTodoDeleted,
}) => {
  return (
    <Row>
      <Col>
        {todos.length === 0 ? (
          <Container>
            <Row className="mb-3">
              <Col>
                <Form.Label>No Todos Available</Form.Label>
              </Col>
            </Row>
          </Container>
        ) : (
          todos.map((todo) => {
            return (
              <Container key={`card_${todo.id}`}>
                <Row className="mb-3">
                  <Col>
                    <ToggleButton
                      onCompleted={(isDone: boolean) => {
                        todo.isDone = isDone;
                        onTodoUpdated(todo);
                      }}
                      isDone={todo.isDone}
                    />
                  </Col>
                  <Col>
                    <Form.Label
                      style={{
                        color: todo.isOverdue ? "red" : "black",
                        textDecoration: todo.isDone ? "line-through" : "none",
                      }}
                    >
                      {todo.title}
                    </Form.Label>
                  </Col>
                  <Col>
                    <Form.Label
                      style={{
                        color: todo.isOverdue ? "red" : "black",
                        textDecoration: todo.isDone ? "line-through" : "none",
                      }}
                    >
                      Deadline: {DateTimeToFormattedString(todo.deadline.to)}
                    </Form.Label>
                  </Col>
                  <Col>
                    <Button
                      variant="danger"
                      onClick={() => onTodoDeleted(todo)}
                    >
                      Delete
                    </Button>
                  </Col>
                </Row>
              </Container>
            );
          })
        )}
      </Col>
    </Row>
  );
};

export default TodoTable;
