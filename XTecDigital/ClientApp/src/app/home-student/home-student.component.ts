import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SessionHandler } from '../helpers/sessionHandler';
import { Grupo } from '../models/grupo';
import { Semestre } from '../models/semestre';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-home-student',
  templateUrl: './home-student.component.html',
  styleUrls: ['./home-student.component.css']
})
export class HomeStudentComponent implements OnInit {

  semestres: any[] = [];

  constructor(private api: ApiService, private router: Router) { }

  ngOnInit(): void {
    this.loadSemestres();
  }

  loadSemestres() {
    this.api.get(`https://localhost/api/Semestres/estudiante/${SessionHandler.getUserId()}`).subscribe(
      (groups: any[]) => {
        console.log(groups);

        let dict = {};

        for (let i = 0; i < groups.length; i++) {
          let current = groups[i];

          let key = `${current.anioSemestre}-${current.periodoSemestre}`;

          if (dict[key] == undefined) {
            dict[key] = [];
          }
          dict[key].push(current);
        }

        let keys = Object.keys(dict);
        for (let i = 0; i < keys.length; i++) {
          let key = keys[i];
          let values = dict[key];

          let semestreInfo = key.split('-');

          let semestre = {
            anio: semestreInfo[0],
            periodo: semestreInfo[1],
            grupos: values
          }

          this.semestres.push(semestre);
        }
        console.log(dict);
      }, (error) => {
        console.log(error);
      }
    );
  }

  onGroupClicked(group: any) {
    console.log(group);
    window.localStorage.setItem('group', JSON.stringify(group));
    this.router.navigate(['grupo']);
  }
}
