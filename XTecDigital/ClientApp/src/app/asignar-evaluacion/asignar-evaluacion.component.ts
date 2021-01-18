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
  group: any;
  rubros = [];
  estudiantes = [];
  evaluaciones = [];

  constructor(private api: ApiService) { }

  ngOnInit(): void {
    this.userType = SessionHandler.getUserType();
    let groupInfo = JSON.parse(window.localStorage.getItem('group'));

    this.group = {
      numero: Number.parseInt(groupInfo.numeroGrupo),
      curso: groupInfo.codigo,
      anio: Number.parseInt(groupInfo.anioSemestre),
      periodo: groupInfo.periodoSemestre
    }

    this.getRubros();
    this.getEstudiantes();

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

  /**
   * Metodo para ocultar el campo de estudiantes en la interfaz
   */
  clearGroups() {
    document.getElementById('grupalOption').style.display = 'none';
  }

  /**
  * Metodo para mostrar el campo de estudiantes en la interfaz
  */
  setGroups() {
    document.getElementById('grupalOption').style.display = 'flex';
  }

  getInfoAsignaciones() {

  }

  /**
   * Metodo para crear una nueva asignacion
   */
  async onAction() { 
    // var nombre = $('#nombre').val().toString();
    var nombre = $('#nombre').toString();
    var rubro = $('#rubro').toString();
    var peso = Number.parseInt($('#peso').val().toString());
    var fecha = $('#fecha').toString();
    var file: File = $('espec').prop('files')[0];
    var individual = Number.parseInt($('#individual').val().toString());
    var estudiantes = $('#groups').val();

    if (file == undefined) {
      console.log('no file selected');
      return;
    }

    var fileData = await this.readFile(file);

    var check = this.validateData(nombre, rubro, peso, fecha, individual, estudiantes);

  }
  validateData(nombre: string, rubro: string, peso: number, fecha: string, individual: number, estudiantes: string | number | string[]) {
    throw new Error('Method not implemented.');
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
