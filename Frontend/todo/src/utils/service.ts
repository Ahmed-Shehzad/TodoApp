import axios, { AxiosError } from "axios";
export interface IService {
  create<T>(url: string, model: object): Promise<T | null | undefined>;
  update<T>(url: string, id: string, model: any): Promise<T | null | undefined>;
  delete<T>(url: string, id: string): Promise<T | null | undefined>;
  findById<T>(url: string, id: string): Promise<T | null | undefined>;
  findAll<T>(url: string): Promise<T | null | undefined>;
}
export class Service implements IService {
  constructor() {}
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
  public async create<T>(url: string, model: object) {
    try {
      const response = await axios.post<T>(url, model);
      return response.data;
    } catch (error) {
      this.logError(error);
    }
  }
  public async update<T>(url: string, id: string, model: object) {
    try {
      const response = await axios.put<T>(`${url}/${id}`, model);
      return response.data;
    } catch (error) {
      this.logError(error);
    }
  }
  public async delete<T>(url: string, id: string) {
    try {
      const response = await axios.delete<T>(`${url}/${id}`);
      return response.data;
    } catch (error) {
      this.logError(error);
    }
  }
  public async findById<T>(url: string, id: string) {
    try {
      const response = await axios.get<T>(`${url}/${id}`);
      return response.data as T | null;
    } catch (error) {
      this.logError(error);
    }
  }
  public async findAll<T>(url: string) {
    try {
      const response = await axios.get<T>(url);
      return response.data;
    } catch (error) {
      this.logError(error);
    }
  }
}
