import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { HomeComponent } from './pages/home/home.component';
import { authGuard } from './core/guards/auth.guard';
import { ChildComponent } from './pages/child/child.component';

export const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
    title: 'Login Page',
  },
  {
    path: 'parent',
    component: HomeComponent,
    canActivate: [authGuard],
    title: 'Parent Todo Page',
  },
  {
    path: 'child',
    component: ChildComponent,
    canActivate: [authGuard],
    title: 'Child Todo Page',
  },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', redirectTo: 'login' },
];
