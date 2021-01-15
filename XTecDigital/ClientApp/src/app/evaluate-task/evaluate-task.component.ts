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

  guardarNotas() { }

  publicarNotas() { }

  feedbackDoubleClicked(asignacion, event) { }

  gradeDoubleClicked(asignacion, event) { }

  onAgregarNota() { }

  onAgregarObserva() { }

  onCancel(tipo) { }
}

