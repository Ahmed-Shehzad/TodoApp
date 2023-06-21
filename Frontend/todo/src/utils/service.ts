import axios from "axios";
export interface IService {
  create<T>(url: string, model: object): Promise<T>;
  update<T>(url: string, id: string, model: any): Promise<T>;
  delete<T>(url: string, id: string): Promise<T>;
  findById<T>(url: string, id: string): Promise<T | null>;
  findAll<T>(url: string): Promise<T>;
}
export class Service implements IService {
  constructor() {}
  public async create<T>(url: string, model: object) {
    const response = await axios.post<T>(url, model);
    if (response.status === 200) {
      return response.data;
    }
    throw new Error(`${response.status} ${response.statusText}`);
  }
  public async update<T>(url: string, id: string, model: object) {
    const response = await axios.put<T>(`${url}/${id}`, model);
    if (response.status === 200) {
      return response.data;
    }
    throw new Error(`${response.status} ${response.statusText}`);
  }
  public async delete<T>(url: string, id: string) {
    const response = await axios.delete<T>(`${url}/${id}`);
    if (response.status === 200) {
      return response.data;
    }
    throw new Error(`${response.status} ${response.statusText}`);
  }
  public async findById<T>(url: string, id: string) {
    const response = await axios.get<T>(`${url}/${id}`);
    if (response.status === 200) {
      return response.data as T | null;
    }
    throw new Error(`${response.status} ${response.statusText}`);
  }
  public async findAll<T>(url: string) {
    const response = await axios.get<T>(url);
    if (response.status === 200) {
      return response.data;
    }
    throw new Error(`${response.status} ${response.statusText}`);
  }
}
