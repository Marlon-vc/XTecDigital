import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-gestion-documentos',
  templateUrl: './gestion-documentos.component.html',
  styleUrls: ['./gestion-documentos.component.css']
})
export class GestionDocumentosComponent implements OnInit {

  selected: any;

  archivos: any[] = [
    {
      nombre: "Examen1.docx",
      tamanio: "1 MB",
      modificado: "2020-12-01" 
    }
  ];
  carpetas: any[] = [
    {
      nombre: 'Presentaciones',
      modificado: '2020-11-24'
    }
  ];

  comandos: any[] = [
    {
      nombre: "Descargar",
      comando: () => {
        console.log('descargando ' + this.selected.nombre);
        
      }
    },
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

}
