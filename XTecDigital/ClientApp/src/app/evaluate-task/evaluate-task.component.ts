import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-evaluate-task',
  templateUrl: './evaluate-task.component.html',
  styleUrls: ['./evaluate-task.component.css']
})
export class EvaluateTaskComponent implements OnInit {

  groupId = this.route.snapshot.params.id;
  asiganciones = [];

  constructor(private route: ActivatedRoute, private api: ApiService) { }

  ngOnInit(): void {
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

