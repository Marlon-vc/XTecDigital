import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SessionHandler } from '../helpers/sessionHandler';
import { Noticia } from '../models/noticia';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-see-group-news',
  templateUrl: './see-group-news.component.html',
  styleUrls: ['./see-group-news.component.css']
})
export class SeeGroupNewsComponent implements OnInit {

  userType: string;
  group: any;
  noticias: Noticia[] = [];

  constructor(private route: ActivatedRoute, private api: ApiService) { }

  ngOnInit(): void {
    this.userType = SessionHandler.getUserType();
    let groupInfo = JSON.parse(window.localStorage.getItem('group'));

    this.group = {
      numero: Number.parseInt(groupInfo.numeroGrupo),
      curso: groupInfo.codigo,
      anio: Number.parseInt(groupInfo.anioSemestre),
      periodo: groupInfo.periodoSemestre
    }
    this.loadNews();
    console.log(this.noticias.length);
  }

  /**
   * Metodo para cargar las noticias de un grupo
   */
  loadNews() {

    var query = new URLSearchParams(this.group).toString();
    console.log(query);

    this.api.get(`https://localhost/api/Noticias/Grupo?${query}`).subscribe(
      (value: any) => {
        // console.log(value);
        this.noticias = value;
        this.loadAutorName();
      }, (error: any) => {
        console.log(error);
      }
    );

  }

  /**
   * Metodo para cargar los nombres de los autores
   */
  loadAutorName() {
    this.noticias.forEach(noticia => {
      this.getAutor(noticia);
    });

  }

  /**
   * Metodo para obtener el autor de una noticia
   * @param noticia Objeto tipo noticia
   */
  getAutor(noticia: Noticia) {
    console.log('loading teachers');
    this.api.get(`https://localhost/api/Profesores/${noticia.autor}`)
      .subscribe(
        (data: any) => {
          noticia.nombreAutor = data.nombre;
          noticia.fecha = noticia.fechaPublicacion.toString().replace('T', ' - ');
          noticia.fecha = noticia.fecha.substr(0, noticia.fecha.lastIndexOf(':'));

          console.log(noticia);

        }, (error) => {
          console.log("Error loading teachers...");
          // console.log(error);
        });
  }

}
