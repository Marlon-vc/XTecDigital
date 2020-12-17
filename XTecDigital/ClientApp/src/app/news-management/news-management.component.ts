import { not } from '@angular/compiler/src/output/output_ast';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { valHooks } from 'jquery';
import { Noticia } from '../models/noticia';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-news-management',
  templateUrl: './news-management.component.html',
  styleUrls: ['./news-management.component.css']
})
export class NewsManagementComponent implements OnInit {

  noticias: Noticia[] = [];
  groupId = this.route.snapshot.params.id;
  selectedNew: Noticia;
  updating: boolean = false;

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

  modify(noticia: Noticia) {
    this.updating = true;
    this.selectedNew = noticia;
    $('#titulo').val(noticia.titulo);
    $('#mensaje').val(noticia.mensaje);
  }

  modifyNew() {
    var titulo = $('#titulo');
    var mensaje = $('#mensaje');
    var tag = $('#errorMsj');

    if (titulo.val() == '' || mensaje.val() == '') {
      tag.text('Por favor complete todos los campos');
      tag.css('display', 'block');
      tag.css('color', 'red');
    }

    this.selectedNew.titulo = titulo.val() as string;
    this.selectedNew.mensaje = mensaje.val() as string;

    if (this.updating) {
      this.modifyNewApi();
    } else {
      this.createNewApi(titulo.val() as string, mensaje.val() as string);
    }
    
  }

  modifyNewApi() {
    this.api.put(`https://localhost/api/Noticias/${this.selectedNew.id}`, this.selectedNew).subscribe(
      (value:any) => {
        document.getElementById('closeButton').click();
        this.updating = false;
      }, (error:any) => {
        console.log(error);
      }
    );
  }

  createNewApi(titulo: string, mensaje: string) {
    var noticia = new Noticia();
    noticia.idGrupo = this.groupId;
    noticia.titulo = titulo;
    noticia.mensaje = mensaje;
    noticia.autor = '';
    noticia.fechaPublicacion = new Date().toLocaleString();

    this.api.post(`https://localhost/api/Noticias`, noticia).subscribe(
      (value:any) => {
        console.log('noticia creada');
      }, (error:any) => {
        console.log(error);
      }
    );
  }
  

  delete(noticia: Noticia) {
    this.api.delete(`https://localhost/api/Noticias/${noticia.id}`).subscribe(
      (value:any) => {
        console.log('noticia eliminada');
      }, (error:any) => {
        console.log(error);
      }
    );
  }



}
