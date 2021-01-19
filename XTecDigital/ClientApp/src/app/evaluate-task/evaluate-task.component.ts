import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SessionHandler } from '../helpers/sessionHandler';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-evaluate-task',
  templateUrl: './evaluate-task.component.html',
  styleUrls: ['./evaluate-task.component.css']
})
export class EvaluateTaskComponent implements OnInit {

  evaluaciones = [];
  asignaciones = [];
  userType: string;
  idUser: string;
  group: any;

  constructor(private route: ActivatedRoute, private api: ApiService) { }

  ngOnInit(): void {
    this.userType = SessionHandler.getUserType();
    this.idUser = SessionHandler.getUserId();

    let groupInfo = JSON.parse(window.localStorage.getItem('group'));

    this.group = {
      numero: Number.parseInt(groupInfo.numeroGrupo),
      curso: groupInfo.codigo,
      anio: Number.parseInt(groupInfo.anioSemestre),
      periodo: groupInfo.periodoSemestre
    }

    this.getInfoEvaluaciones();
  }

  getInfoEvaluaciones() {
    var info: any = {
      Numero: this.group.numero,
      Curso: this.group.curso,
      Anio: this.group.anio,
      Periodo: this.group.periodo,
      Profesor: this.idUser
    }

    var query = new URLSearchParams(info).toString();
    this.api.get(`https://localhost/api/Evaluaciones/eval-prof?${query}`).subscribe(
      (value: any) => {
        console.log(value);
        this.evaluaciones = value;
      }, (error: any) => {
        console.log(error);
      }
    );
  }

  loadAsignaciones() {
    var evaluacion = $('#eval').val().toString();
    var array = evaluacion.split(',');
    console.log(array);
  }

  /**
   * Metodo para guardar las notas sin publicarlas
   */
  guardarNotas() { }

  /**
   * Metodo para publicar las notas
   */
  publicarNotas() { }

  /**
   * Metodo para mostrar modal de las observaciones
   * @param asignacion Objeto evaluacion
   * @param event Evento de la interfaz
   */
  feedbackDoubleClicked(asignacion, event) { }

  /**
   * Metodo para mostrar modal de la nota
   * @param asignacion Objeto evaluacion
   * @param event Evento de la interfaz
   */
  gradeDoubleClicked(asignacion, event) { }

  /**
   * Metodo para agregar la nota de la evaluacion
   */
  onAgregarNota() { }

  /**
   * Metodo para agregar observaciones a la evaluacion
   */
  onAgregarObserva() { }

  /**
   * Metodo para cerrar modal
   * @param tipo tipo de modal
   */
  onCancel(tipo) { }
}

