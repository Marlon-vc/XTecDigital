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

  userType: string;
  group: any;
  noticias: Noticia[] = [];
  userId = SessionHandler.getUserId();
  selectedNew: Noticia;
  updating: boolean = false;

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
      this.selectedNew.mensaje = mensaje.val() as string;
      this.modifyNewApi(titulo.val() as string);
    } else {
      this.createNewApi(titulo.val() as string, mensaje.val() as string);
    }

  }

  /**
   * Metodo para modificar una noticia desde el API
   */
  modifyNewApi(nuevoTitulo: string) {

    var noticia: any = {
      titulo: this.selectedNew.titulo,
      nuevoTitulo: nuevoTitulo,
      mensaje: this.selectedNew.mensaje,
      autor: this.selectedNew.autor,
      fechaPublicacion: this.selectedNew.fechaPublicacion,
      numero: this.selectedNew.numero,
      curso: this.selectedNew.curso,
      anio: this.selectedNew.anio,
      periodo: this.selectedNew.periodo
    }

    this.api.put(`https://localhost/api/Noticias`, noticia).subscribe(
      (value: any) => {
        this.loadNews();
        document.getElementById('closeButton').click();
        this.updating = false;
        $('#titulo').val('');
        $('#mensaje').val('');
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

    var noticia: any = {
      titulo: titulo,
      mensaje: mensaje,
      autor: this.userId,
      numero: this.group.numero,
      curso: this.group.curso,
      anio: this.group.anio,
      periodo: this.group.periodo
    }


    this.api.post(`https://localhost/api/Noticias`, noticia).subscribe(
      (value: any) => {
        console.log('noticia creada');
        this.loadNews();
        this.updating = false
        $('#titulo').val('');
        $('#mensaje').val('');
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

    console.log(noticia.anio);

    var not: any = {
      titulo: noticia.titulo,
      fechaPublicacion: noticia.fechaPublicacion,
      numero: noticia.numero,
      curso: noticia.curso,
      anio: noticia.anio,
      periodo: noticia.periodo
    } 

    var query = new URLSearchParams(not).toString();

    this.api.delete(`https://localhost/api/Noticias?${query}`).subscribe(
      (value: any) => {
        console.log('noticia eliminada');
        this.loadNews();
      }, (error: any) => {
        console.log(error);
      }
    );
  }



}
