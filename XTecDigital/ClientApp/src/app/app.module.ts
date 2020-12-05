import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { InitializeSemesterComponent } from './initialize-semester/initialize-semester.component'
import { LoginComponent } from './login/login.component';
import { GestionCursosComponent } from './gestion-cursos/gestion-cursos.component';
import { GestionDocumentosComponent } from './gestion-documentos/gestion-documentos.component';
import { GestionRubrosComponent } from './gestion-rubros/gestion-rubros.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    InitializeSemesterComponent,
    LoginComponent,
    GestionCursosComponent,
    GestionDocumentosComponent,
    GestionRubrosComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: LoginComponent, pathMatch: 'full' },
      { path: 'home', component: HomeComponent },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'initialize-semester', component: InitializeSemesterComponent},
      { path: 'gestion-cursos', component: GestionCursosComponent },
      { path: 'gestion-documentos', component: GestionDocumentosComponent },
      { path: 'gestion-rubros', component: GestionRubrosComponent},
      
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
