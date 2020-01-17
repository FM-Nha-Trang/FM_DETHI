import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

//Import component for path
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CreateComponent } from './create/create.component';
import { HistoryComponent } from './history/history.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';

const routes: Routes = [
	{path: '', redirectTo: '/login', pathMatch: 'full'}, /*Default url, redirect to login path, */
	{path: 'login', component: LoginComponent},
	{path: 'register', component: RegisterComponent},
	{path: 'dashboard', component: DashboardComponent},
	{path: 'create', component: CreateComponent},
	{path: 'history', component: HistoryComponent},
	{path: "**", component: PageNotFoundComponent} /*This path must be the last*/
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

//Export module can use in app.module.ts 
export const routingComponents = [LoginComponent];
