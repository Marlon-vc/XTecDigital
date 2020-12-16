import { Component, OnInit } from '@angular/core';
import { Rubro } from '../models/rubro';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-gestion-rubros',
  templateUrl: './gestion-rubros.component.html',
  styleUrls: ['./gestion-rubros.component.css']
})
export class GestionRubrosComponent implements OnInit {


  selected: any;

  rubros: Rubro[] = [
    {
      nombre: 'Quices',
      porcentaje: 30,
      idGrupo: 1
    }, 
    {
      nombre: 'Examenes',
      porcentaje: 30,
      idGrupo: 1
    }, 
    {
      nombre: 'Proyectos',
      porcentaje: 40,
      idGrupo: 1
    }
  ];

  comandos: any[] = [
    {
      nombre: "Editar",
      comando: () => {
        console.log('editando ' + this.selected.nombre);
      }
    },
    {
      nombre: 'Eliminar',
      comando: () => {
        console.log('eliminando ' + this.selected.nombre);
      }
    }
  ];


  constructor(private api: ApiService) { }

  ngOnInit(): void {
    $(window).on('click', (event) => {
      $('#context-menu').css('display', 'none');
    });
  }

  folderDoubleClicked(folder, event) {
    console.log('Double clicked');
    console.log(folder);
  }

  fileDoubleClicked(file, event) {
    console.log(file);
    
  }

  onModificar(event) {

  }

  onEliminar(event) {

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

    

    var total = 0;
    this.rubros.forEach(element => {
      total += element.porcentaje;
    });

    if (total != 100) {
      return false;
    }
  }


}
