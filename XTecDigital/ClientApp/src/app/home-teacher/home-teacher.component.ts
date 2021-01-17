import { Component, OnInit } from '@angular/core';
import { Grupo } from '../models/grupo';
import { Semestre } from '../models/semestre';

@Component({
  selector: 'app-home-teacher',
  templateUrl: './home-teacher.component.html',
  styleUrls: ['./home-teacher.component.css']
})
export class HomeTeacherComponent implements OnInit {

  semestres: Semestre[] = [];

  constructor() { }

  ngOnInit(): void {
    this.loadSemestres();
  }

  /**
   * Metodo para cargar los semestres y cursos del profesor actual
   */
  loadSemestres() {
    // cargar semestres del estudiante o profesor actual
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

}
