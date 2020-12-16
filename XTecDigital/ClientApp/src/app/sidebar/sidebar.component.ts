import { Component, OnInit } from '@angular/core';
import { Grupo } from '../models/grupo';
import { Semestre } from '../models/semestre';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {

  userType = window.localStorage.getItem('user-type');
  user = window.localStorage.getItem('user-id');
  semestres: Semestre[] = [];
  actualUser;

  constructor() { }

  ngOnInit(): void {

    // this.userType = 'profesor';
    // this.userType = 'admin';
    // this.userType = 'estudiante';

    $("#close-sidebar").on('click', function() {
      $(".page-wrapper").removeClass("toggled");
    });
    $("#show-sidebar").on('click', function() {
      $(".page-wrapper").addClass("toggled");
    });
    console.log("settings handlers");

    $(function() {
      $('li.sidebar-dropdown>a').on('click', function() {
        console.log('clicked');
        $(".sidebar-submenu").slideUp(200);
        if (
          $(this)
            .parent()
            .hasClass("active")
        ) {
          $(".sidebar-dropdown").removeClass("active");
          $(this)
            .parent()
            .removeClass("active");
        } else {
          $(".sidebar-dropdown").removeClass("active");
          $(this)
            .next(".sidebar-submenu")
            .slideDown(200);
          $(this)
            .parent()
            .addClass("active");
        }
        return false;
      });
    });

    if (this.userType == 'estudiante') {
      this.loadSemestres();
    }
    this.getActualUserInfo();

  }

  loadSemestres() {
    // cargar semestres del estudiante actual
    var grupo1 = new Grupo();
    var grupo2 = new Grupo();
    var grupo3 = new Grupo();
    var grupo4 = new Grupo();

    grupo1.idCurso = 'CE1';
    grupo1.numero = 1;
    grupo2.idCurso = 'CE2';
    grupo2.numero = 2;
    grupo3.idCurso = 'CE3';
    grupo3.numero = 3;
    grupo4.idCurso = 'CE4';
    grupo4.numero = 4;

    var sem = new Semestre();
    sem.anio = 2020;
    sem.id = 1;
    sem.periodo = '1';
    sem.grupos = [grupo1, grupo2, grupo3, grupo4];
    
    this.semestres.push(sem);
  }

  getActualUserInfo() {
    var user = {
      nombre: 'Paola Villegas',
      tipo: 'Estudiante'
    }

    this.actualUser = user;
  }

  onLogout() {
    console.log('logging out..');
    window.localStorage.clear();
  }


}