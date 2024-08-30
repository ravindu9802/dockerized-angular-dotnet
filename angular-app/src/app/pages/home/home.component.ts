import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CrudService } from '../../core/services/crud.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {
  private todoService = inject(CrudService);

  todo: string = '';
  editTodo: any = {};
  todoList: any = [];
  isEdit: boolean = false;

  ngOnInit() {
    this.fetchTodos();
  }

  fetchTodos() {
    this.todoService.getAllTodos().subscribe({
      next: (res: any) => {
        this.todoList = res;
      },
      error: (e) => console.error(e),
      complete: () => console.info('getAllTodos complete'),
    });
  }

  onAdd() {
    if (this.todo) {
      this.todoService.createTodo(this.todo, false).subscribe({
        next: (res: any) => {
          this.todo = '';
          this.fetchTodos();
        },
        error: (e) => console.error(e),
        complete: () => console.info('createTodo complete'),
      });
    }
  }

  onEdit(todo: any) {
    this.todo = todo.name;
    this.editTodo = todo;
    this.isEdit = true;
  }

  toggleStatus(todo: any) {
    this.todoService
      .updateTodo(todo.id, todo.name, !todo.isComplete)
      .subscribe({
        next: (res: any) => {
          for (let i = 0; i < this.todoList.length; i++) {
            if (this.todoList[i].id == todo.id) {
              this.todoList[i].isComplete = !todo.isComplete;
              return false;
            }
          }
          return true;
        },
        error: (e) => console.error(e),
        complete: () => console.info('updateTodo complete'),
      });
  }

  onSave() {
    this.todoService
      .updateTodo(this.editTodo.id, this.todo, this.editTodo.isComplete)
      .subscribe({
        next: (res: any) => {
          this.todoList = this.todoList.filter(
            (t: { id: any; name: string }) => {
              if (t.id == this.editTodo.id) {
                t.name = this.todo;
              }
              return true;
            }
          );
          this.isEdit = false;
          this.todo = '';
        },
        error: (e) => console.error(e),
        complete: () => console.info('updateTodo complete'),
      });
  }

  onDelete(id: number) {
    this.todoService.deleteTodo(id).subscribe({
      next: (res: any) => {
        this.todoList = this.todoList.filter((t: { id: number }) => {
          return t.id !== id;
        });
      },
      error: (e) => console.error(e),
      complete: () => console.info('updateTodo complete'),
    });
  }
}
