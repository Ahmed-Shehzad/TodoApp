import { FC, useState } from "react";
import { Button, Col, Form, Modal, Row } from "react-bootstrap";
import { InfoCircleFill } from "react-bootstrap-icons";

interface ToggleButtonProps {
  isDone: boolean;
  onCompleted: (isDone: boolean) => void;
}

const ToggleButton: FC<ToggleButtonProps> = ({ onCompleted, isDone }) => {
  const [isDisplay, setIsDisplay] = useState(false);

  const toggleDisplay = () => setIsDisplay(!isDisplay);

  const toggleCheck = () => {
    const checked = !isDone;
    onCompleted(checked);
  };

  return (
    <Row>
      <Col>
        <Button variant="light" onClick={toggleDisplay}>
          <InfoCircleFill />
          <Modal
            show={isDisplay}
            onBackdropClick={toggleDisplay}
            onHide={toggleDisplay}
          >
            <Modal.Header closeButton>
              <Modal.Title>Info: </Modal.Title>
            </Modal.Header>
            <Modal.Body>
              <Form>
                Click on
                <Button
                  className="mx-2"
                  style={{ minWidth: "150px" }}
                  variant={isDone ? "success" : "info"}
                  onClick={toggleCheck}
                >
                  {isDone ? "Done" : "In Progress"}
                </Button>
                to change the status of the task.
              </Form>
            </Modal.Body>
          </Modal>
        </Button>

        <Button
          className="mx-2"
          style={{ minWidth: "150px" }}
          variant={isDone ? "success" : "info"}
          onClick={toggleCheck}
        >
          {isDone ? "Done" : "In Progress"}
        </Button>
      </Col>
    </Row>
  );
};

export default ToggleButton;
