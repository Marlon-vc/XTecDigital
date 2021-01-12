import { Component, OnInit } from '@angular/core';
import { Evaluacion } from '../models/evaluacion';
import { Rubro } from '../models/rubro';

@Component({
  selector: 'app-evaluation-page',
  templateUrl: './evaluation-page.component.html',
  styleUrls: ['./evaluation-page.component.css']
})
export class EvaluationPageComponent implements OnInit {

  rubros: Rubro[] = [];
  change = false;

  constructor() { }

  ngOnInit(): void {
    var ev = new Evaluacion();
    ev.id = 1;
    ev.nombre = 'evaluacion prueba';
    var ev1 = new Evaluacion();
    ev1.id = 2;
    ev1.nombre = 'evaluacion prueba1';
    var ev2 = new Evaluacion();
    ev2.id = 3;
    ev2.nombre = 'evaluacion prueba2';
    var ev3 = new Evaluacion();
    ev3.id = 4;
    ev3.nombre = 'evaluacion prueba3';
    var rub = new Rubro();
    rub.id = 1;
    rub.nombre = 'Rubro';
    rub.porcentaje = 30;
    rub.evaluaciones = [ev, ev1, ev2, ev3];



    this.rubros.push(rub);
  }

  changeIcon() {
    console.log(this.change);
    this.change = !this.change;
    if (this.change) {
      $('.change-icon').css({
        '-webkit-transform': 'rotate(180deg)',
        '-ms-transform': 'rotate(180deg)',
        '-o-transform': 'rotate(180deg)',
        'transform': 'rotate(180deg)'
      });
    } else {
      $('.change-icon').css({
        '-webkit-transform': 'rotate(0deg)',
        '-ms-transform': 'rotate(0deg)',
        '-o-transform': 'rotate(0deg)',
        'transform': 'rotate(0deg)'
      });
    }
    
  }

}
