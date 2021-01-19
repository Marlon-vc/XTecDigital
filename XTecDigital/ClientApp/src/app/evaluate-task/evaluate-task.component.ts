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
  actualAsignacion;

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
    var evalInfo = evaluacion.split(','); //[Nombre, Rubro]

    var info: any = {
      Numero: this.group.numero,
      Curso: this.group.curso,
      Anio: this.group.anio,
      Periodo: this.group.periodo,
      Profesor: this.idUser,
      Evaluacion: evalInfo[0],
      Rubro: evalInfo[1]
    }

    var query = new URLSearchParams(info).toString();
    this.api.get(`https://localhost/api/Evaluaciones/evaluar?${query}`).subscribe(
      (value: any) => {
        console.log(value);
        this.asignaciones = value;
      }, (error: any) => {
        console.log(error);
      }
    );


  
  }

  /**
   * Metodo para guardar las notas sin publicarlas
   */
  async guardarNotas() { 

    this.api.put(`https://localhost/api/Evaluaciones/guardar`, this.asignaciones).subscribe(
      (value:any) => {
        console.log('Notas guardadas');

      }, (error: any) => {
        console.log('error');
        console.log(error);
        
      }
    );
  }


  /**
 * Metodo para leer un archivo
 * @param file 
 */
readFile(file: File) {
  let promise = new Promise((resolve) => {
    var reader = new FileReader();
    reader.onload = (data) => {
      resolve(data.target.result);
    };
    reader.readAsDataURL(file);
  });
  return promise;
}

  /**
   * Metodo para publicar las notas
   */
  publicarNotas() { 
    var data = {
      Numero: this.group.numero,
      Curso: this.group.curso,
      Anio: this.group.anio,
      Periodo: this.group.periodo,
      Nombre: this.actualAsignacion.nombreEvaluacion,
      Rubro: this.actualAsignacion.rubro
    }

    this.api.put(`https://localhost/api/Evaluaciones/publicar`, data).subscribe(
      (value:any) => {
        console.log('Notas publicadas');

      }, (error: any) => {
        console.log('error');
        console.log(error);
        
      }
    );

    // this.guardarNotas();

  }

  entregableDoubleClicked(asignacion, $event) {
    var data: any = {
      Nombre: asignacion.entregable,
      Carpeta: asignacion.carpetaEntregable,
      TipoCarpeta: asignacion.tipoCarpetaEntregable,
      Numero: this.group.numero,
      Curso: this.group.curso,
      Anio: this.group.anio,
      Periodo: this.group.periodo,
    }

    var query = new URLSearchParams(data).toString();
    window.location.replace(`https://localhost/api/Archivos/download?${query}`);
  }

  /**
   * Metodo para mostrar modal de las observaciones
   * @param asignacion Objeto evaluacion
   * @param event Evento de la interfaz
   */
  feedbackDoubleClicked(asignacion, event) {
    console.log('feedback double click');
    console.log(asignacion);
    this.actualAsignacion = asignacion;

    //@ts-ignore
    $('#feedback-modal').modal('show');
   }

  /**
   * Metodo para mostrar modal de la nota
   * @param asignacion Objeto evaluacion
   * @param event Evento de la interfaz
   */
  gradeDoubleClicked(asignacion, event) { 
    console.log('grade double click');
    console.log(asignacion);
    this.actualAsignacion = asignacion;

    //@ts-ignore
    $('#grade-modal').modal('show');
  }

  detalleDoubleClicked(asignacion, $event) {
    this.actualAsignacion = asignacion;

    //@ts-ignore
    $('#detalle-modal').modal('show');
  }

  /**
   * Metodo para agregar la nota de la evaluacion
   */
  onAgregarNota() {
    var nota = Number.parseFloat($('#grade').val().toString());

    var result = this.asignaciones.findIndex(a => a.estudiante = this.actualAsignacion.estudiante);
    this.asignaciones[result].nota = nota;
    //@ts-ignore
    $('#grade-modal').modal('hide');
  }

  /**
   * Metodo para agregar observaciones a la evaluacion
   */
  onAgregarObserva() {
    var feedback = $('#feedback').val().toString();

    var result = this.asignaciones.findIndex(a => a.estudiante = this.actualAsignacion.estudiante);
    this.asignaciones[result].observaciones = feedback;
    //@ts-ignore
    $('#feedback-modal').modal('hide');
  }

  async onAgregarDetalle() {
    var file : File = $('#detalleFile').prop('files')[0];
    if (file == undefined) {
      console.log('no file selected');
      return;
    }

    var fileData = await this.readFile(file);

    var result = this.asignaciones.findIndex(a => a.estudiante = this.actualAsignacion.estudiante);
    this.asignaciones[result].fileData = fileData;
    this.asignaciones[result].fileName = file.name;
    this.asignaciones[result].detalle = file.name;
    this.asignaciones[result].fileSize = file.size;

    //@ts-ignore
    $('#detalle-modal').modal('hide');
  }

  /**
   * Metodo para cerrar modal
   * @param tipo tipo de modal
   */
  onCancel(tipo) { }
}

