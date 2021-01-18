import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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

  constructor(private router:Router) { }

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

    if (this.userType == 'estudiante' || this.userType == 'profesor') {
      this.loadSemestres();
    }
    this.getActualUserInfo();

  }

  /**
   * Metodo para cargar los semestres
   */
  loadSemestres() {
    // cargar semestres del estudiante o profesor actual
    var grupo1 = new Grupo();
    var grupo2 = new Grupo();
    var grupo3 = new Grupo();
    var grupo4 = new Grupo();

    grupo1.curso = 'CE1';
    grupo1.numero = 1;
    grupo2.curso = 'CE2';
    grupo2.numero = 2;
    grupo3.curso = 'CE3';
    grupo3.numero = 3;
    grupo4.curso = 'CE4';
    grupo4.numero = 4;

    var sem = new Semestre();
    sem.anio = 2020;
    sem.periodo = '1';
    sem.grupos = [grupo1, grupo2, grupo3, grupo4];
    
    this.semestres.push(sem);
  }

  /**
   * Metodo para obtener la informacion del usuario actual
   */
  getActualUserInfo() {
    // obtener nombre del usuario logueado
    var user = {
      nombre: 'Paola Villegas',
      tipo: this.userType.toLocaleUpperCase()
    }

    this.actualUser = user;
  }

  /**
   * Metodo para cerrar sesion
   */
  onLogout() {
    console.log('logging out..');
    window.localStorage.clear();
    this.router.navigate(['']);
  }


}
