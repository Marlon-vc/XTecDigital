import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Evaluacion } from '../models/evaluacion';
import { InfoEvaluacion } from '../models/infoEvaluacion';
import { Rubro } from '../models/rubro';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-evaluation-page',
  templateUrl: './evaluation-page.component.html',
  styleUrls: ['./evaluation-page.component.css']
})

export class EvaluationPageComponent implements OnInit {

  rubros: Rubro[] = [];
  change = false;
  groupId = this.route.snapshot.params.id;

  constructor(private route: ActivatedRoute, private api: ApiService) { }

  ngOnInit(): void {
    this.loadRubros();
  }

  /**
   * Metodo para cargar los rubros
   */
  getRubros() {
    this.api.get(`https://localhost/api/Rubros/Grupo/${this.groupId}`).subscribe(
      (rubros: Rubro[]) => {
        // console.log(rubros);
        rubros.forEach(rubro => {
          // console.log(rubro);
          this.api.get(`https://localhost/api/Evaluaciones/Rubro/`).subscribe(
            (evaluaciones:Evaluacion[]) => {
              // console.log(evaluaciones);
              rubro.evaluaciones = evaluaciones;
              rubro.evaluaciones.forEach(evaluacion => {
                this.api.get(`https://localhost/api/Evaluaciones/Info/${evaluacion.id}`).subscribe(
                  (info:InfoEvaluacion) => {
                    console.log(info);
                    evaluacion.info = info;
                  }, (error:any) => {
                    console.log(error);
                  }
                );
              });
              this.rubros = rubros;
              console.log(this.rubros);
            }, (error: any) => {
              console.log(error);
            }
          );
        });
      }, (error: any) => {
        console.log(error);
      }
    );
  }

  /**
   * Metodo asincrono para cargar rubros
   */
  async loadRubros() {
    this.rubros = await this.getRubros2();
    console.log(this.rubros);
    
  }

  /**
   * Metodo con promesa para obtener rubros
   */
  getRubros2(): Promise<Rubro[]> {
    return new Promise((resolve, reject) => {
      this.api.get(`https://localhost/api/Rubros/Grupo/${this.groupId}`).subscribe(
        async (rubros: Rubro[]) => {
          for (let i = 0; i < rubros.length; i++) {
            const current = rubros[i];
            current.evaluaciones = await this.getEvaluacionesRubro(current);
          }
          resolve(rubros);
        }, (error) => {
          console.log(error);
          reject(error);
        }
      );
    });
  }

  /**
   * Metodo asincrono para obtener las evaluaciones de un rubro
   * @param rubro Objeto rubro para buscar evaluaciones
   */
  async getEvaluacionesRubro(rubro: Rubro): Promise<Evaluacion[]> {
    return new Promise((resolve, reject) => {
      this.api.get(`https://localhost/api/Evaluaciones/Rubro/`).subscribe(
        async (evaluaciones: Evaluacion[]) => {
          for (let i = 0; i < evaluaciones.length; i++) {
            const evaluacion = evaluaciones[i];
            evaluacion.info = await this.getInfoEvaluacion(evaluacion);
          }
          resolve(evaluaciones);
        }, (error) => {
          console.log(error);
          reject(error);
        }
      );
    });
  }

  /**
   * Metodo asincrono para obtener la informacion de una evaluacion
   * @param evaluacion  objeto tipo evaluacion
   */
  async getInfoEvaluacion(evaluacion: Evaluacion): Promise<InfoEvaluacion> {
    return new Promise((resolve, reject) => {
      this.api.get(`https://localhost/api/Evaluaciones/Info/${evaluacion.id}`).subscribe(
        (info: InfoEvaluacion) => {
          resolve(info);
        }, (error) => {
          console.log(error);
          reject(error);
        }
      );
    });
  }

  /**
   * Metodo para realizar animacion de icono
   */
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
