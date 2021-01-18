import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { error } from 'protractor';
import { SessionHandler } from '../helpers/sessionHandler';
import { Rubro } from '../models/rubro';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-gestion-rubros',
  templateUrl: './gestion-rubros.component.html',
  styleUrls: ['./gestion-rubros.component.css']
})
export class GestionRubrosComponent implements OnInit {

  userType: string;
  group: any;
  update = false;
  selected: Rubro;
  rubros: Rubro[] = [];
  total:number = 0;

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

    this.getRubros();

    $(window).on('click', (event) => {
      $('#context-menu').css('display', 'none');
    });
  }

  /**
   * Metodo para obtener los rubros de un grupo
   */
  getRubros() {
    var query = new URLSearchParams(this.group).toString();
    console.log(query);
    
    this.api.get(`https://localhost/api/Rubros/Grupo?${query}`).subscribe(
      (value: any) => {
        console.log(value);
        this.total = 0;
        this.rubros = value;
        this.rubros.forEach(element => {
          this.total += element.porcentaje;
        });
      }, (error: any) => {
        console.log(error);
      } 
    );
  }

  /**
   * Metodo para eliminar un rubro
   */
  onEliminar() {

    var request: any = {
      nombre: this.selected.nombre,
      numero: this.selected.numero,
      curso: this.selected.curso,
      anio: this.selected.anio,
      periodo: this.selected.periodo
    }

    var query = new URLSearchParams(request).toString();

    this.api.delete(`https://localhost/api/Rubros?${query}`).subscribe(
      (value:any) => {
        console.log('eliminado');
        this.getRubros();
      }, (error: any) => {
        console.log(error);
      }
    );
  }

  /**
   * Menu para mostrar el menu contextual
   * @param item Item seleccionado
   * @param event Evento de la interfaz
   */
  showContextMenu(item, event) {
    console.log(item);
    this.selected = item;
    console.log('selected ' + this.selected);
    $('#context-menu').css({
      'display': 'block',
      'top': `${event.pageY}px`,
      'left': `${event.pageX}px`
    }).focus();
    return false;
  }

  /**
   * Metodo para modificar un rubro
   */
  modifyRubro() {
    var nombre = $('#nombreRubro');
    var porcentaje = $('#porc-rubro');
    var tag = $('#porc');

    console.log('nuevo nombre ' + nombre.val());
    console.log('nuevo porcentaje ' + porcentaje.val());


    var total = 0;
    this.rubros.forEach(element => {
      // if (this.selected.id != element.id)
        total += element.porcentaje;
    });

    

    console.log(total);

    if (this.update) {
      total -= this.selected.porcentaje;
    }
    console.log(total);

    total += Number.parseInt(porcentaje.val() as string);

    console.log(total);
  
    if (total > 100) {
      tag.css('display', 'block');
      tag.css('color', 'red');
      tag.text('El porcentaje del rubro no es valido');
      return;
    }

    if (porcentaje.val() == '' || nombre.val() == '') {
      tag.text('Por favor complete todos los campos');
      tag.css('display', 'block');
      tag.css('color', 'red');
      return;
    }

    console.log('updating' + this.update);

    if (this.update) {
      this.modifyRubroApi(nombre, porcentaje);
    } else {
      this.addRubroApi(nombre, porcentaje);
    }
  }

  /**
   * Metodo para agregar un rubro
   * @param nombre Nombre del rubro
   * @param porcentaje Porcentaje del rubro
   */
  addRubroApi(nombre: JQuery<HTMLElement>, porcentaje: JQuery<HTMLElement>) {
    
    
    var rubro = {
      nombre: nombre.val() as string,
      porcentaje: Number.parseInt(porcentaje.val() as string),
      numero: this.group.numero,
      curso: this.group.curso,
      anio: this.group.anio,
      periodo: this.group.periodo
    };

    console.log(rubro);

    this.api.post(`https://localhost/api/Rubros`, rubro).subscribe(
      (value: any) => {
        document.getElementById('closeButton').click();
        console.log('agregado');
        this.update = false;
        nombre.val('');
        porcentaje.val('');
        this.getRubros();
      }, (error: any) => {
        console.log(error);
      }
    );
  }

  /**
   * Metodo para modificar un rubro
   * @param nombre Nuevo nombre del rubro
   * @param porcentaje Nuevo porcentaje del rubro
   */
  modifyRubroApi(nombre: JQuery<HTMLElement>, porcentaje: JQuery<HTMLElement>) {
    

    console.log(this.selected.nombre);
    

    var rubro = {
      nombre: this.selected.nombre,
      nuevoNombre: nombre.val() as string, 
      porcentaje: Number.parseInt(porcentaje.val() as string),
      numero: this.group.numero,
      curso: this.group.curso,
      anio: this.group.anio,
      periodo: this.group.periodo
    };

    console.log(rubro);


    this.api.put(`https://localhost/api/Rubros`, rubro).subscribe(
      (value:any) => {
        document.getElementById('closeButton').click();
        this.getRubros();
        this.update = false;
        nombre.val('');
        porcentaje.val('');
      }, (error:any) => {
        console.log(error);
      });
  }

  /**
   * Metodo para colocar que se esta actualizando
   */
  updating(){
    this.update = true;
  }
  


}
