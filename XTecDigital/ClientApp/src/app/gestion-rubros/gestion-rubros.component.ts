import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { error } from 'protractor';
import { Rubro } from '../models/rubro';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-gestion-rubros',
  templateUrl: './gestion-rubros.component.html',
  styleUrls: ['./gestion-rubros.component.css']
})
export class GestionRubrosComponent implements OnInit {

  update = false;
  selected: Rubro;
  rubros: Rubro[] = [];
  groupId = this.route.snapshot.params.id;
  total:number = 0;

  constructor(private route: ActivatedRoute, private api: ApiService) { }

  ngOnInit(): void {
    this.getRubros();
    $(window).on('click', (event) => {
      $('#context-menu').css('display', 'none');
    });
  }
  getRubros() {
    this.api.get(`https://localhost/api/Rubros/Grupo/${this.groupId}`).subscribe(
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

  onEliminar() {
    this.api.delete(`https://localhost/api/Rubros/${this.selected.id}`).subscribe(
      (value:any) => {
        console.log('eliminado');
        this.getRubros();
      }, (error: any) => {
        console.log(error);
        
      }
    );
  }

  showContextMenu(item, event) {
    console.log(item);
    this.selected = item;
    $('#context-menu').css({
      'display': 'block',
      'top': `${event.pageY}px`,
      'left': `${event.pageX}px`
    }).focus();
    return false;
  }

  modifyRubro() {
    var nombre = $('#nombreRubro');
    var porcentaje = $('#porc-rubro');
    var tag = $('#porc');

    console.log('nuevo nombre ' + nombre.val());
    console.log('nuevo porcentaje ' + porcentaje.val());


    var total = 0;
    this.rubros.forEach(element => {
      if (this.selected.id != element.id)
        total += element.porcentaje;
    });

    total += Number.parseInt(porcentaje.val() as string);

    console.log(total);
  
    if (total > 100) {
      tag.css('display', 'block');
      tag.css('color', 'red');
      tag.text('El porcentaje del rubro no es valido');
      
    }

    if (porcentaje.val() == '' || nombre.val() == '') {
      tag.text('Por favor complete todos los campos');
      tag.css('display', 'block');
      tag.css('color', 'red');
    }

    if (this.update) {
      this.modifyRubroApi(nombre, porcentaje);
    } else {
      this.addRubroApi(nombre, porcentaje);
    }
  }

  addRubroApi(nombre: JQuery<HTMLElement>, porcentaje: JQuery<HTMLElement>) {
    console.log(this.groupId);
    var id = Number.parseInt(this.groupId as string);
    console.log('id ' + id);  
    var rubro = {
      nombre: nombre.val() as string,
      idGrupo: id,
      porcentaje: Number.parseInt(porcentaje.val() as string)
    };

    this.api.post(`https://localhost/api/Rubros`, rubro).subscribe(
      (value: any) => {
        document.getElementById('closeButton').click();
        console.log('agregado');
        this.getRubros();
      }, (error: any) => {
        console.log(error);
      }
    );
  }

  modifyRubroApi(nombre: JQuery<HTMLElement>, porcentaje: JQuery<HTMLElement>) {
    this.selected.nombre = nombre.val() as string;
    this.selected.porcentaje = Number.parseInt(porcentaje.val() as string);

    console.log(this.selected);

    this.api.put(`https://localhost/api/Rubros/${this.selected.id}`, this.selected).subscribe(
      (value:any) => {
        document.getElementById('closeButton').click();
      }, (error:any) => {
        console.log(error);
      });
  }

  updating(){
    this.update = true;
  }
  


}
