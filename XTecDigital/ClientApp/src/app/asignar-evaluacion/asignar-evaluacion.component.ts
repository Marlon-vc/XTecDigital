import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-asignar-evaluacion',
  templateUrl: './asignar-evaluacion.component.html',
  styleUrls: ['./asignar-evaluacion.component.css']
})
export class AsignarEvaluacionComponent implements OnInit {

  rubros = [];
  estudiantes = [];
  evaluaciones = [];

  constructor() { }

  ngOnInit(): void {
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

  /**
   * Metodo para crear una nueva asignacion
   */
  onAction() { }

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
