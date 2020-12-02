import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';
import { Curso } from '../models/curso';
import { Estudiante } from '../models/estudiante';
import { Grupo } from '../models/grupo';
import { Periodo } from '../models/periodo';
import { Profesor } from '../models/profesor';

@Component({
  selector: 'app-initialize-semester',
  templateUrl: './initialize-semester.component.html',
  styleUrls: ['./initialize-semester.component.css']
})
export class InitializeSemesterComponent implements OnInit {

  periodos: Periodo[] = [];
  cursos: Curso[] = [];
  profesores: Profesor[] = [];
  grupos = [];
  estudiantes: Estudiante[] = [];

  constructor() { }

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
      var complete = c.saveSemester();
      if (complete) {
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

  saveSemester(): boolean {
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

  loadPeriods() {

  }

  loadCursos() {

  }

  loadProfesores() {

  }

  loadEstudiantes() {

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

    var existente = this.grupos.find(g => g.grupo == grupo.val());
    console.log(existente);

    var grupoNuevo = {
      idCurso: curso.val(),
      numero: grupo.val(),
      profesores: profesores.val()
    }

    this.grupos.push(grupoNuevo);
  }

}
