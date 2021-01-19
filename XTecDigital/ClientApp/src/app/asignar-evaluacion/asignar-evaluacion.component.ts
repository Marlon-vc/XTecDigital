import { Component, OnInit } from '@angular/core';
import { SessionHandler } from '../helpers/sessionHandler';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-asignar-evaluacion',
  templateUrl: './asignar-evaluacion.component.html',
  styleUrls: ['./asignar-evaluacion.component.css']
})
export class AsignarEvaluacionComponent implements OnInit {

  userType: string;
  idUser: string;
  group: any;
  rubros = [];
  estudiantes = [];
  evaluaciones = [];
  grupos = [];

  constructor(private api: ApiService) { }

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

    this.getRubros();
    this.getEstudiantes();
    this.getInfoAsignaciones();

    $(window).on('click', (event) => {
      $('#context-menu').css('display', 'none');
    });
  }

  /**
   * Metodo para obtener los rubros de un grupo
   */
  getRubros() {
    var query = new URLSearchParams(this.group).toString();
    console.log(query);

    this.api.get(`https://localhost/api/Rubros/Grupo?${query}`).subscribe(
      (value: any) => {
        console.log(value);
        this.rubros = value;
      }, (error: any) => {
        console.log(error);
      }
    );
  }

  /**
   * Metodo para obtener todos los estudiantes de un grupo
   */
  getEstudiantes() {
    var query = new URLSearchParams(this.group).toString();
    console.log(query);

    this.api.get(`https://localhost/api/Estudiantes/grupo?${query}`).subscribe(
      (value: any) => {
        console.log(value);
        value.forEach(student => {
          this.api.get(`https://localhost/api/Estudiantes/${student.estudiante}`)
            .subscribe(
              (data: any) => {
                this.estudiantes.push(data)
                console.log(this.estudiantes);
              }, (error) => {
                console.log("Error loading students...");
                // console.log(error);
              }
            );
        });
        console.log(this.estudiantes);
      }, (error: any) => {
        console.log(error);
      }
    );
  }

  getInfoAsignaciones() {
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

/**
 * Metodo para crear una nueva asignacion
 */
async onAction() {
  // var nombre = $('#nombre').val().toString();
  var nombre = $('#nombre').val().toString();
  var rubro = $('#rubro').val().toString();
  var peso = Number.parseInt($('#peso').val().toString());
  var fecha = $('#fecha').val().toString();
  var file: File = $('#espec').prop('files')[0];
  var individual = $('input[name=\"individual\"]:checked').val() === "1"


  if (file == undefined) {
    console.log('no file selected');
    return;
  }

  var fileData = await this.readFile(file);

  // var check = this.validateData(nombre, rubro, peso, fecha, individual, estudiantes);

  if (!individual && this.grupos.length == 0) {
    console.log('no hay grupos');
    return;
  }

  if (individual) {
    this.grupos = [];
  }

  var data = {
    nombreEvaluacion: nombre,
    fechaEntrega: fecha,
    pesoNota: peso,
    grupal: !individual,
    nombreEspec: file.name,
    rubro: rubro,
    numero: this.group.numero,
    curso: this.group.curso,
    anio: this.group.anio,
    periodo: this.group.periodo,
    fileData: fileData,
    size: file.size,
    estudiantes: this.grupos
  }

  console.log(data);

  this.api.post(`https://localhost/api/Evaluaciones/asignacion`, data)
    .subscribe((data: any) => {
      console.log('Asignacion creada correctamente');
      this.grupos = [];
      $('#nombre').val('');
      $('#rubro').val('');
      $('#peso').val('');
      $('#fecha').val('');
      this.getInfoAsignaciones();
    }, (error) => {
      console.log("Error...");
      console.log(error);
    });

}

/**
   * Metodo para ocultar el campo de estudiantes en la interfaz
   */
  clearGroups() {
    document.getElementById('grupalOption').style.display = 'none';
    this.grupos = []; 
   }

   /**
   * Metodo para mostrar el campo de estudiantes en la interfaz
   */
  setGroups() { 
    document.getElementById('grupalOption').style.display = 'flex'; 
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

guardarGrupo() {
  var grupo = $('#groups').val();
  this.grupos.push(grupo);
}

/**
 * Metodo para cancelar la edicion de una asignacion
 */
onCancelEdit() { }

/**
 * Metodo para mostrar el menu contextual al dar click
 * @param evaluacion Objeto de tipo evaluacion
 * @param $event Evento de interfaz
 */
onRightClick(evaluacion, $event) { }

/**
 * Metodo para confirmar la eliminacion de una asignacion
 */
onEliminarConfirm() { }

/**
 * Metodo para modificar una asignacion
 * @param $event  Evento de la interfaz
 */
onModificar($event) { }

/**
 * Metodo para eliminar una asignacion
 * @param $event Evento de la interfaz
 */
onEliminar($event) { }
}
