import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

//Import modules
import { AppRoutingModule, routingComponents } from './app-routing.module';
import { AppComponent } from './app.component';

//Import services
import { UsersService } from './users.service';
import { TestService } from './test.service';
import { LoginComponent } from './login/login.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { RegisterComponent } from './register/register.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { CreateComponent } from './create/create.component';
import { HistoryComponent } from './history/history.component';

import { ValueArrayPipe } from './pipes/valuearra.pipe';

@NgModule({
  declarations: [
    AppComponent,
    routingComponents,
    PageNotFoundComponent,
    RegisterComponent,
    DashboardComponent,
    SidebarComponent,
    CreateComponent,
    HistoryComponent,
    ValueArrayPipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    UsersService,
    TestService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
