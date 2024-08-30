import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class CrudService {
  private http = inject(HttpClient);
  private API_BASE_URL = environment.API_BASE_URL + 'todo';

  constructor() {}

  getAllTodos() {
    return this.http.get(this.API_BASE_URL);
  }

  createTodo(name: string, isComplete: boolean) {
    return this.http.post(
      this.API_BASE_URL,
      {
        name: name,
        isComplete: isComplete,
      }
    );
  }

  updateTodo(id: number, name: string, isComplete: boolean) {
    return this.http.put(
      this.API_BASE_URL + '/' + id,
      {
        name: name,
        isComplete: isComplete,
      }
    );
  }

  deleteTodo(id: number) {
    return this.http.delete(this.API_BASE_URL + '/' + id);
  }
}
