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

  clearGroups() {
    document.getElementById('grupalOption').style.display = 'none'; 
   }

  setGroups() { 
    document.getElementById('grupalOption').style.display = 'flex'; 
  }

  onAction() { }

  onCancelEdit() { }

  onRightClick(evaluacion, $event) { }

  onEliminarConfirm() { }

  onModificar($event) { }

  onEliminar($event) { }
}
