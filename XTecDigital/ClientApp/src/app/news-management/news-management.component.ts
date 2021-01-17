import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { valHooks } from 'jquery';
import { SessionHandler } from '../helpers/sessionHandler';
import { Grupo } from '../models/grupo';
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
  userId = SessionHandler.getUserId();
  selectedNew: Noticia;
  updating: boolean = false;
  actualGroup: Grupo;

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
        // console.log(value);
        this.noticias = value;
        this.loadAutorName();
      }, (error: any) => {
        console.log(error);
      }
    );

  }

  /**
   * Metodo para cargar el nombre del autor
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
        }, (error) => {
          console.log("Error loading teachers...");
          console.log(error);
        });
  }

  /**
   * Metodo para modificar una noticia
   * @param noticia Objeto tipo notica
   */
  modify(noticia: Noticia) {
    this.updating = true;
    this.selectedNew = noticia;
    $('#titulo').val(noticia.titulo);
    $('#mensaje').val(noticia.mensaje);
  }

  /**
   * Metodo para modificar una noticia o agregar una nueva noticia
   */
  modifyNew() {
    var titulo = $('#titulo');
    var mensaje = $('#mensaje');
    var tag = $('#errorMsj');

    if (titulo.val() == '' || mensaje.val() == '') {
      tag.text('Por favor complete todos los campos');
      tag.css('display', 'block');
      tag.css('color', 'red');
    }

    if (this.updating) {
      this.selectedNew.titulo = titulo.val() as string;
      this.selectedNew.mensaje = mensaje.val() as string;
      this.modifyNewApi();
    } else {
      this.createNewApi(titulo.val() as string, mensaje.val() as string);
    }

  }

  /**
   * Metodo para modificar una noticia desde el API
   */
  modifyNewApi() {
    this.api.put(`https://localhost/api/Noticias/${this.selectedNew.id}`, this.selectedNew).subscribe(
      (value: any) => {
        this.loadNews();
        document.getElementById('closeButton').click();
        this.updating = false;
      }, (error: any) => {
        console.log(error);
      }
    );
  }

  /**
   * Metodo para crear una nueva noticia desde el api
   * @param titulo Titulo de la noticia
   * @param mensaje Mensaje de la noticia
   */
  createNewApi(titulo: string, mensaje: string) {
    var noticia = new Noticia();
    noticia.idGrupo = Number.parseInt(this.groupId as string);
    noticia.titulo = titulo;
    noticia.mensaje = mensaje;
    noticia.autor = this.userId;
    // noticia.fechaPublicacion = new Date();


    this.api.post(`https://localhost/api/Noticias`, noticia).subscribe(
      (value: any) => {
        console.log('noticia creada');
        this.loadNews();
        document.getElementById('closeButton').click();
      }, (error: any) => {
        console.log(error);
      }
    );
  }

  /**
   * Metodo para eliminar una noticia
   * @param noticia Objeto tipo noticia
   */
  delete(noticia: Noticia) {
    this.api.delete(`https://localhost/api/Noticias/${noticia.id}`).subscribe(
      (value: any) => {
        console.log('noticia eliminada');
        this.loadNews();
      }, (error: any) => {
        console.log(error);
      }
    );
  }



}
