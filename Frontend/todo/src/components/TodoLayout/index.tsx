import { Todo } from "@/models/todo";
import { todoService } from "@/pages/api/todo";
import { CreateTodoCommand, DeadlineCommand } from "@/services/commands/todo";
import { DateRangePicker } from "@mui/x-date-pickers-pro/DateRangePicker";
import dayjs from "dayjs";
import { FC, FormEvent, useEffect, useState } from "react";
import { Button, Col, Container, Form, InputGroup, Row } from "react-bootstrap";
import TodoContainer from "./TodoContainer";

function SortByDate(todos: Todo[]) {
  return todos.sort((a, b) => {
    const deadlineA = new Date(a.deadline.to).getTime();
    const deadlineB = new Date(b.deadline.to).getTime();
    return deadlineA - deadlineB;
  });
}

const TodoLayout: FC = () => {
  const today = dayjs();
  const tomorrow = dayjs().add(1, "day");
  const [todos, setTodos] = useState<Todo[]>([]);

  const [newTodo, setNewTodo] = useState<CreateTodoCommand>({
    title: "",
    deadline: {
      from: today.toDate(),
      to: tomorrow.toDate(),
    },
    description: "",
  });
  const [validated, setValidated] = useState(false);
  useEffect(() => {
    const fetchTodos = async () => {
      const result = await todoService.findAll();
      const orderedTodos = SortByDate(result);
      setTodos(orderedTodos);
    };

    fetchTodos();
  }, []);

  const onTodoUpdate = async (todo: Todo) => {
    const index = todos.findIndex((item) => item.id === todo.id);
    if (index === -1) return;

    todo.description = `Updated Description for: ${todo.title}`;
    const result = await todoService.update(todo);
    todos[index] = result;
    setTodos([...todos]);
  };

  const onTodoDelete = async (todo: Todo) => {
    const index = todos.findIndex((item) => item.id === todo.id);
    if (index === -1) return;
    const result = await todoService.delete({
      id: todo.id,
    });
    const newTodos = todos.filter((item) => item.id !== result.id);
    setTodos(newTodos);
  };

  const onTodoCreate = async () => {
    if (newTodo) {
      if (newTodo.title && newTodo.title.length < 11) return;
      newTodo.description = `Description for: ${newTodo.title}`;
      const result = await todoService.create(newTodo);

      setTodos([...todos, result]);
      setNewTodo({
        title: "",
        deadline: {
          from: today.toDate(),
          to: tomorrow.toDate(),
        },
        description: "",
      });
    }
  };

  const handleSubmit = (event: FormEvent<HTMLFormElement>) => {
    const form = event.currentTarget;
    event.preventDefault();
    event.stopPropagation();

    if (form.checkValidity()) {
      onTodoCreate();
    } else {
      setValidated(true);
    }
  };

  return (
    <Container fluid>
      <Container className="mt-5">
        <Form
          noValidate
          validated={validated}
          onSubmit={(event) => handleSubmit(event)}
        >
          <Row>
            <Col>
              <InputGroup className="mb-3">
                <Form.Control
                  type="text"
                  className="form-control me-2"
                  placeholder="Title"
                  required
                  minLength={11}
                  value={newTodo?.title}
                  onChange={(e) => {
                    const todoCreate: CreateTodoCommand = {
                      ...newTodo,
                      title: e.target.value,
                    };
                    setNewTodo(todoCreate);
                  }}
                />
              </InputGroup>
            </Col>
          </Row>
          <Row>
            <Col>
              <DateRangePicker
                onChange={(event) => {
                  const from = event[0]?.toDate();
                  const to = event[1]?.toDate();

                  const timeFrame: DeadlineCommand = {
                    from: new Date(from ?? today.toDate()),
                    to: new Date(to ?? tomorrow.toDate()),
                  };
                  setNewTodo({
                    ...newTodo,
                    deadline: timeFrame,
                  });
                }}
                defaultValue={[today, tomorrow]}
                disablePast
              />
            </Col>
            <Col>
              <Button variant="primary" type="submit" onClick={onTodoCreate}>
                Add
              </Button>
            </Col>
          </Row>
        </Form>
      </Container>

      <Container className="mt-5">
        <TodoContainer
          onTodoDelete={onTodoDelete}
          onTodoUpdate={onTodoUpdate}
          todos={todos}
        />
      </Container>
    </Container>
  );
};

export default TodoLayout;
