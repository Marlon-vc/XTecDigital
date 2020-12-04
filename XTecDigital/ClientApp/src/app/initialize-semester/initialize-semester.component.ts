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
    
    // $(".next").click(function(){
    $(".next").on('click', function() {
      current_fs = $(this).parent();
      next_fs = $(this).parent().next();
      
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
    });
    
    // $(".previous").click(function(){
    $(".previous").on('click', function(){
      current_fs = $(this).parent();
      previous_fs = $(this).parent().prev();
      
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
    });
    
    // $('.radio-group .radio').click(function(){
    $('.radio-group .radio').on('click', function(){
      $(this).parent().find('.radio').removeClass('selected');
      $(this).addClass('selected');
    });
    var c = this;
    // $(".submit").click(function(){
    $('.submit').on('click', function(){
      console.log('Registro completado.');
      var complete = c.checkSemester();
      if (complete) {
        c.saveSemester();
        current_fs = $(this).parent();
        next_fs = $(this).parent().next();
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
      } else {
        console.log('Complete todos los campos'); // TODO mostrar modal
        return;
      }
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
      if (grupo.grupoEstudiantes.length == 0) {
        return false;
      }
    });

    return true;
  }

  saveSemester() {

    var year = $('#year');
    var period = $('#period');

    var semestreInfo = {
      Anio : year.val(),
      IdPeriodo : period.val(),
      Grupos : this.grupos
    }

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
    var profesores = $('#profesor');

    var existente = this.grupos.find(g => g.numero == grupo.val());
    console.log(existente);

    
    var grupoN = new Grupo();
    grupoN.idCurso = curso.val().toString();
    grupoN.numero = Number(grupo.val());
    grupoN.grupoProfesores = profesores.val() as string[];

    this.grupos.push(grupoN);
  }

  saveGroupStudents() {
    var students = $('#estudiantes');
    var idGroup = $('#grupo');

    var actualGroup = this.grupos.find(g => g.numero == Number(idGroup.val()));
    
    actualGroup.grupoEstudiantes = students.val() as string[];

  }
}
