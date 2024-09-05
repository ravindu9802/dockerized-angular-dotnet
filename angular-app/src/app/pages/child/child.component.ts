import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CrudService } from '../../core/services/crud.service';

@Component({
  selector: 'app-child',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterModule],
  templateUrl: './child.component.html',
  styleUrl: './child.component.css',
})
export class ChildComponent {
  private todoService = inject(CrudService);

  todo: string = '';
  editTodo: any = {};
  todoList: any = [];
  parentTodoList: any = [];
  isEdit: boolean = false;
  parentId: number = 0;
  parentTodoKeyValue: any = {};

  ngOnInit() {
    this.fetchTodos();
    this.fetchParentTodos();
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

  fetchParentTodos() {
    this.todoService.getAllParentTodos().subscribe({
      next: (res: any) => {
        this.parentTodoList = res;
        this.parentTodoList.forEach((p: any) => {
          this.parentTodoKeyValue[p.id] = p.parentName;
        });
      },
      error: (e) => console.error(e),
      complete: () => console.info('getAllParentTodos complete'),
    });
  }

  onAdd() {
    if (this.todo) {
      this.todoService.createTodo(this.todo, false, this.parentId).subscribe({
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
    this.parentId = todo.todoParentId;
  }

  toggleStatus(todo: any) {
    this.todoService
      .updateTodo(todo.id, todo.name, !todo.isComplete, this.parentId)
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
      .updateTodo(
        this.editTodo.id,
        this.todo,
        this.editTodo.isComplete,
        this.parentId
      )
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

  onParentChange() {
    console.log(this.parentId);
  }
}
