import { Routes } from '@angular/router';
import { HomeComponent } from './components/home-component/home.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];
