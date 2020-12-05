import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';
import { Curso } from '../models/curso';
import { Estudiante } from '../models/estudiante';
import { Grupo } from '../models/grupo';
import { Periodo } from '../models/periodo';
import { Profesor } from '../models/profesor';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-initialize-semester',
  templateUrl: './initialize-semester.component.html',
  styleUrls: ['./initialize-semester.component.css']
})
export class InitializeSemesterComponent implements OnInit {

  periodos: Periodo[] = [];
  cursos: Curso[] = [];
  profesores: Profesor[] = [];
  grupos: Grupo[] = [];
  estudiantes: Estudiante[] = [];

  constructor(private api: ApiService) { }

  ngOnInit(): void {
    this.init();
    this.setCourseOption();
    this.loadPeriods();
    this.loadCursos();
    this.loadProfesores();
    this.loadEstudiantes();
  }

  init() {
    var current_fs, next_fs, previous_fs; //fieldsets
    var opacity;
    var c = this;
    $(".next").on('click', function() {
      c.next(current_fs, next_fs, opacity, this);
    });
    
    $(".previous").on('click', function(){
      c.previous(current_fs, previous_fs, opacity, this);
    });
    
    $('.radio-group .radio').on('click', function(){
      $(this).parent().find('.radio').removeClass('selected');
      $(this).addClass('selected');
    });
    var c = this;
    $('.submit').on('click', function(){
      console.log('Registro completado.');
      var complete = c.checkSemester();
      if (complete) {
        c.saveSemester(current_fs, next_fs, opacity, this);
      } else {
        console.log('Complete todos los campos'); // TODO mostrar modal
        return;
      }
    });
  }

  next(current_fs, next_fs, opacity, actual) {
    current_fs = $(actual).parent();
      next_fs = $(actual).parent().next();
      
      //Add Class Active
      $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");
      
      //show the next fieldset
      next_fs.show();
      //hide the current fieldset with style
      current_fs.animate({opacity: 0}, {
        step: function(now) {
          // for making fielset appear animation
          opacity = 1 - now;
          current_fs.css({
            'display': 'none',
            'position': 'relative'
          });
          next_fs.css({'opacity': opacity});
        },
        duration: 600
      });
  }

  previous(current_fs, previous_fs, opacity, actual) {
    current_fs = $(actual).parent();
      previous_fs = $(actual).parent().prev();
      
      //Remove class active
      $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");
      
      //show the previous fieldset
      previous_fs.show();
      
      //hide the current fieldset with style
      current_fs.animate({opacity: 0}, {
        step: function(now) {
          // for making fielset appear animation
          opacity = 1 - now;

          current_fs.css({
            'display': 'none',
            'position': 'relative'
          });
          previous_fs.css({'opacity': opacity});
        },
        duration: 600
      });
  }

  checkSemester(): boolean {
    var year = $('#year');
    var period = $('#period');
    
    var groupLength = this.grupos.length;

    console.log('year ' + year.val());
    console.log('period ' + period.val());

    if (year.val() == '' || period.val() == 'Seleccione un periodo' || groupLength == 0) {
      return false;
    }

    this.grupos.forEach(grupo => {
      if (grupo.estudiantes.length == 0) {
        return false;
      }
    });

    return true;
  }

  saveSemester(current_fs, next_fs, opacity, actual) {

    var year = $('#year');
    var period = $('#period');

    var semestreInfo = {
      Anio : Number.parseInt(year.val() as string),
      IdPeriodo : Number.parseInt(period.val() as string),
      Grupos : this.grupos
    }

    console.log(semestreInfo);

    console.log('saving semester');
    this.api.post(`https://localhost:5001/api/Semestres`, semestreInfo)
      .subscribe((data: any) => {
        console.log('Semestre creado correctamente');
        this.next(current_fs, next_fs, opacity, this);
      }, (error) => {
        console.log("Error creating semester...");
        console.log(error);
      });

  }

  loadPeriods() {
    console.log('loading periods');
    this.api.get(`https://localhost:5001/api/Periodos`)
      .subscribe((data: any[]) => {
        this.periodos = data;
      }, (error) => {
        console.log("Error loading periods...");
        // console.log(error);
      });
  }

  loadCursos() {
    console.log('loading courses');
    this.api.get(`https://localhost:5001/api/Cursos`)
      .subscribe((data: any[]) => {
        this.cursos = data;
        console.log(this.cursos);
      }, (error) => {
        console.log("Error loading courses...");
        // console.log(error);
      });
  }

  loadProfesores() {
    console.log('loading teachers');
    this.api.get(`https://localhost:5001/api/Profesores`)
      .subscribe((data: any[]) => {
        this.profesores = data;
      }, (error) => {
        console.log("Error loading teachers...");
        // console.log(error);
      });
  }

  loadEstudiantes() {
    console.log('loading students');
    this.api.get(`https://localhost:5001/api/Estudiantes`)
      .subscribe((data: any[]) => {
        this.estudiantes = data;
        console.log(this.estudiantes);
      }, (error) => {
        console.log("Error loading students...");
        // console.log(error);
      });
  }

  setCourseOption() {
    $('#curso').on('change', function() {
      $('#courseInfo').css('display', 'flex');
    });
  }

  saveCourse() {
    $('#courseInfo').css('display', 'none');
    var curso = $('#curso');
    var grupo = $('#group');
    var profesores = $('#profesor').val() as string[];

    if (profesores.length < 1) {
      return;
    }
    
    var grupoN = new Grupo();
    grupoN.idCurso = curso.val().toString();
    grupoN.numero = Number.parseInt(grupo.val() as string);
    grupoN.profesores = profesores;

    this.grupos.push(grupoN);
  }

  saveGroupStudents() {
    var students = $('#estudiantes').val() as string[];
    var groupInfo = $('#grupo').val().toString();  
    var info = groupInfo.split(', ', 2);

    var numeroGrupo = Number.parseInt(info[0]);
    var idCursoG = info[1];
   

    var actualGroup = this.grupos.find(g => g.numero == numeroGrupo && g.idCurso == idCursoG);
    
    actualGroup.estudiantes = students;

  }

  deleteGroup(grupo: Grupo) {
    var index = this.grupos.indexOf(grupo);
    if (index > -1) {
      this.grupos.splice(index, 1);
   }
  }

  deleteStudents(grupo: Grupo) {
    grupo.estudiantes = [];
  }
}
