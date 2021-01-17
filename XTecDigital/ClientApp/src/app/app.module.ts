import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { InitializeSemesterComponent } from './initialize-semester/initialize-semester.component'
import { LoginComponent } from './login/login.component';
import { GestionCursosComponent } from './gestion-cursos/gestion-cursos.component';
import { GestionDocumentosComponent } from './gestion-documentos/gestion-documentos.component';
import { GestionRubrosComponent } from './gestion-rubros/gestion-rubros.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { HomeGrupoComponent } from './home-grupo/home-grupo.component';
import { NewsManagementComponent } from './news-management/news-management.component';
import { SeeGroupNewsComponent } from './see-group-news/see-group-news.component';
import { EvaluationPageComponent } from './evaluation-page/evaluation-page.component';
import { EvaluateTaskComponent } from './evaluate-task/evaluate-task.component';
import { HomeAdminComponent } from './home-admin/home-admin.component';
import { HomeStudentComponent } from './home-student/home-student.component';
import { HomeTeacherComponent } from './home-teacher/home-teacher.component';
import { AsignarEvaluacionComponent } from './asignar-evaluacion/asignar-evaluacion.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    InitializeSemesterComponent,
    LoginComponent,
    GestionCursosComponent,
    GestionDocumentosComponent,
    GestionRubrosComponent,
    SidebarComponent,
    HomeGrupoComponent,
    NewsManagementComponent,
    SeeGroupNewsComponent,
    EvaluationPageComponent,
    EvaluateTaskComponent,
    HomeAdminComponent,
    HomeStudentComponent,
    HomeTeacherComponent,
    AsignarEvaluacionComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: LoginComponent, pathMatch: 'full' },
      { path: 'home', component: HomeComponent },
      { path: 'initialize-semester', component: InitializeSemesterComponent},
      { path: 'gestion-cursos', component: GestionCursosComponent },
      { path: 'gestion-documentos', component: GestionDocumentosComponent },
      { path: 'gestion-rubros/:id', component: GestionRubrosComponent},
      { path: 'sidebar', component: SidebarComponent},
      { path: 'documentos/:id', component: GestionDocumentosComponent },
      { path: 'grupo/:id', component: HomeGrupoComponent },
      { path: 'noticias/:id', component: NewsManagementComponent },
      { path: 'ver-noticias/:id', component: SeeGroupNewsComponent},
      { path: 'evaluation/:id', component: EvaluationPageComponent},
      { path: 'evaluate-task/:id', component: EvaluateTaskComponent},
      { path: 'home-admin', component: HomeAdminComponent},
      { path: 'home-student', component: HomeStudentComponent},
      { path: 'home-teacher', component: HomeTeacherComponent},
      { path: 'asignar-evaluacion/:id', component: AsignarEvaluacionComponent}
      
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
