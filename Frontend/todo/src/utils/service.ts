import { IRoot } from "@/models/todo";
import axios, { AxiosError } from "axios";

type ID = string | number;

type TReturnType<T> = T | null | undefined;

export interface IService {
  create<T extends IRoot>(url: string, model: object): Promise<TReturnType<T>>;
  update<T extends IRoot>(
    url: string,
    id: ID,
    model: object
  ): Promise<TReturnType<T>>;
  delete<T extends IRoot>(url: string, id: ID): Promise<TReturnType<T>>;
  findById<T extends IRoot>(url: string, id: ID): Promise<TReturnType<T>>;
  findAll<T extends IRoot>(url: string): Promise<TReturnType<T>>;
}
export class Service implements IService {
  private logError(error: unknown) {
    if (axios.isAxiosError(error)) {
      const axiosError = error as AxiosError;
      if (axiosError.response?.status === 404) {
        // Handle 404 error
        console.log("Resource not found.");
      } else {
        // Handle other error codes
        console.error("An error occurred:", error);
      }
    } else {
      // Handle non-Axios errors
      console.error("An error occurred:", error);
    }
  }
  public async create<T extends IRoot>(url: string, model: object) {
    try {
      const response = await axios.post<T>(url, model);
      return response.data as TReturnType<T>;
    } catch (error) {
      this.logError(error);
    }
  }
  public async update<T extends IRoot>(url: string, id: ID, model: object) {
    try {
      const response = await axios.put<T>(`${url}/${id}`, model);
      return response.data as TReturnType<T>;
    } catch (error) {
      this.logError(error);
    }
  }
  public async delete<T extends IRoot>(url: string, id: ID) {
    try {
      const response = await axios.delete<T>(`${url}/${id}`);
      return response.data as TReturnType<T>;
    } catch (error) {
      this.logError(error);
    }
  }
  public async findById<T extends IRoot>(url: string, id: ID) {
    try {
      const response = await axios.get<T>(`${url}/${id}`);
      return response.data as TReturnType<T>;
    } catch (error) {
      this.logError(error);
    }
  }
  public async findAll<T extends IRoot>(url: string) {
    try {
      const response = await axios.get<T>(url);
      return response.data as TReturnType<T>;
    } catch (error) {
      this.logError(error);
    }
  }
}
