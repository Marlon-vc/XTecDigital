import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Noticia } from '../models/noticia';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-see-group-news',
  templateUrl: './see-group-news.component.html',
  styleUrls: ['./see-group-news.component.css']
})
export class SeeGroupNewsComponent implements OnInit {

  noticias: Noticia[] = [];
  groupId = this.route.snapshot.params.id;

  constructor(private route: ActivatedRoute, private api: ApiService) { }

  ngOnInit(): void {
    this.loadNews();
    console.log(this.noticias.length);
  }

  /**
   * Metodo para cargar las noticias de un grupo
   */
  loadNews() {
    this.api.get(`https://localhost/api/Noticias/Grupo/${this.groupId}`).subscribe(
      (value: any) => {
        console.log(value);
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
