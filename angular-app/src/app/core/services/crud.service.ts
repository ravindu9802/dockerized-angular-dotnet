import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class CrudService {
  private http = inject(HttpClient);
  private API_BASE_URL = environment.API_BASE_URL;

  constructor() {}

  getAllTodos() {
    return this.http.get(this.API_BASE_URL + 'todo');
  }

  createTodo(name: string, isComplete: boolean, parentId: number) {
    return this.http.post(this.API_BASE_URL + 'todo', {
      name: name,
      isComplete: isComplete,
      todoParentId: parentId,
    });
  }

  updateTodo(id: number, name: string, isComplete: boolean, parentId: number) {
    return this.http.put(this.API_BASE_URL + 'todo/' + id, {
      name: name,
      isComplete: isComplete,
      todoParentId: parentId,
    });
  }

  deleteTodo(id: number) {
    return this.http.delete(this.API_BASE_URL + 'todo/' + id);
  }

  getAllParentTodos() {
    return this.http.get(this.API_BASE_URL + 'todoparent');
  }
}
