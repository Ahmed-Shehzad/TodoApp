import { Todo } from "@/models/todo";
import {
  CreateTodoCommand,
  DeleteTodoCommand,
  UpdateTodoCommand,
} from "@/services/commands/todo";
import { GetTodoByIdQuery } from "@/services/queries/todo";
import { IService, Service } from "@/utils";

class TodoService {
  private readonly service: IService;
  private readonly baseUrl: string;
  constructor(service: IService) {
    this.service = service;
    this.baseUrl = `${process.env.apiUrl}/todo`;
  }
  public async create(command: CreateTodoCommand) {
    return await this.service.create<Todo>(this.baseUrl, command);
  }
  public async update(command: UpdateTodoCommand) {
    return await this.service.update<Todo>(this.baseUrl, command.id, command);
  }
  public async delete(command: DeleteTodoCommand) {
    return await this.service.delete<Todo>(this.baseUrl, command.id);
  }
  public async findById(query: GetTodoByIdQuery) {
    return await this.service.findById<Todo>(this.baseUrl, query.id);
  }
  public async findAll() {
    return await this.service.findAll<Todo[]>(this.baseUrl);
  }
}

const todoService = new TodoService(new Service());
export { todoService };
