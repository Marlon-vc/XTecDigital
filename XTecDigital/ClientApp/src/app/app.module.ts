import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { InitializeSemesterComponent } from './initialize-semester/initialize-semester.component'
import { LoginComponent } from './login/login.component';
import { GestionCursosComponent } from './gestion-cursos/gestion-cursos.component';
import { GestionDocumentosComponent } from './gestion-documentos/gestion-documentos.component';
import { GestionRubrosComponent } from './gestion-rubros/gestion-rubros.component';
import { HomeGrupoComponent } from './home-grupo/home-grupo.component';
import { NewsManagementComponent } from './news-management/news-management.component';
import { SeeGroupNewsComponent } from './see-group-news/see-group-news.component';
import { EvaluationPageComponent } from './evaluation-page/evaluation-page.component';
import { EvaluateTaskComponent } from './evaluate-task/evaluate-task.component';
import { HomeStudentComponent } from './home-student/home-student.component';
import { HomeTeacherComponent } from './home-teacher/home-teacher.component';
import { AsignarEvaluacionComponent } from './asignar-evaluacion/asignar-evaluacion.component';
import { InitializeExcelComponent } from './initialize-excel/initialize-excel.component';
import { GroupInfoComponent } from './group-info/group-info.component';

@NgModule({
  declarations: [
    AppComponent,
    InitializeSemesterComponent,
    LoginComponent,
    GestionCursosComponent,
    GestionDocumentosComponent,
    GestionRubrosComponent,
    HomeGrupoComponent,
    NewsManagementComponent,
    SeeGroupNewsComponent,
    EvaluationPageComponent,
    EvaluateTaskComponent,
    HomeStudentComponent,
    HomeTeacherComponent,
    AsignarEvaluacionComponent,
    InitializeExcelComponent,
    GroupInfoComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: LoginComponent, pathMatch: 'full' },
      { path: 'initialize-semester', component: InitializeSemesterComponent},
      { path: 'gestion-cursos', component: GestionCursosComponent },
      { path: 'gestion-documentos', component: GestionDocumentosComponent },
      { path: 'gestion-rubros', component: GestionRubrosComponent},
      { path: 'documentos', component: GestionDocumentosComponent },
      { path: 'grupo', component: GroupInfoComponent },
      { path: 'noticias', component: NewsManagementComponent },
      { path: 'ver-noticias', component: SeeGroupNewsComponent},
      { path: 'evaluation', component: EvaluationPageComponent},
      { path: 'evaluate-task', component: EvaluateTaskComponent},
      { path: 'home-student', component: HomeStudentComponent},
      { path: 'home-teacher', component: HomeTeacherComponent},
      { path: 'initialize-excel', component: InitializeExcelComponent},
      { path: 'asignar-evaluacion', component: AsignarEvaluacionComponent}
      
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
