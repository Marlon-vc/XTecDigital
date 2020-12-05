import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-gestion-documentos',
  templateUrl: './gestion-documentos.component.html',
  styleUrls: ['./gestion-documentos.component.css']
})
export class GestionDocumentosComponent implements OnInit {

  selected: any;
  groupId: number;

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

  constructor(private api: ApiService) { }

  ngOnInit(): void {
    //DEBUG: obtener codigo actual del grupo
    this.groupId = 1;

    $(window).on('click', (event) => {
      $('#context-menu').css('display', 'none');
    });

    this.loadArchivos();
  }

  loadArchivos() {
    //TODO: obtener id del grupo actual para obtener sus archivos
  }

  readFile(file: File) {
    let promise = new Promise((resolve) => {
      var reader = new FileReader();
      reader.onload = (data) => {
        resolve(data.target.result);
      };
      reader.readAsDataURL(file);
    });
    return promise;
  }

  async onSubirArchivo() {
    console.log('subir archivo');
    // @ts-ignore
    var file: File = $('#upload-file').prop('files')[0];
    console.log(file);

    if (file == undefined) {
      console.log('no file selected');
      return;
    }

    // @ts-ignore
    $('#upload-modal').modal('hide');
    
    var fileData = {
      data: await this.readFile(file),
      name: file.name,
      size: file.size,
      lastModified: file.lastModified
    };

    //TODO: enviar datos al api
  }

  onCancelUpload() {
    console.log(' upload cancelled');
    $('#upload-file').val('');
    
  }

  onCrearCarpeta() {
    console.log('crear carpeta');
    
  }

  folderDoubleClicked(folder, event) {
    console.log('Double clicked');
    console.log(folder);
  }

  fileDoubleClicked(file, event) {
    console.log(file);
    
  }

  onDescargar(event) {
    console.log('descargando');
    
  }

  onModificar(event) {
    console.log('modificando');
    
  }

  onEliminar(event) {
    console.log('eliminando');
    
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
