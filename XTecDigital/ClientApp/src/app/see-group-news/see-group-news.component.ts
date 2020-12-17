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

  loadNews() {
    var not1 = new Noticia();
    not1.idGrupo = 1;
    not1.titulo = 'Noticia prueba';
    not1.autor = 'Paola Villegas Chacon';
    not1.fechaPublicacion = new Date().toLocaleString();
    not1.mensaje = 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Reiciendis aliquid atque, nulla? Quos cum ex quis soluta, a laboriosam. Dicta expedita corporis animi vero voluptate voluptatibus possimus, veniam magni quis!';
    this.noticias.push(not1);

    // this.api.get(`https://localhost/api/Noticias`).subscribe(
    //   (value:any) => {
    //     console.log(value);
    //     this.noticias = value;
    //   }, (error:any) => {
    //     console.log(error);
    //   }
    // );

  }

}
