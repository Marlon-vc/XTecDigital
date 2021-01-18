import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SessionHandler } from '../helpers/sessionHandler';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-gestion-documentos',
  templateUrl: './gestion-documentos.component.html',
  styleUrls: ['./gestion-documentos.component.css']
})
export class GestionDocumentosComponent implements OnInit {

  userType: string;
  group: any;
  selected: any;
  inFolder: boolean;
  currentFolder: any;
  rootFolder: any;

  archivos: any[];
  carpetas: any[];

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

    $(window).on('click', () => {
      $('#context-menu').css('display', 'none');
    });

    this.loadDocumentos();
  }

  loadDocumentos() {

    var query = new URLSearchParams(this.group).toString();
    console.log(query);

    this.api.get(`https://localhost/api/Archivos/root?${query}`)
      .subscribe((data: any) => {
        this.carpetas = data.folders;
        this.archivos = data.files;
        this.rootFolder = data.root;
      }, error => {
        console.log(error);
      });
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

    var fileData: any;

    if (this.inFolder) {
      fileData = {
        FileData: await this.readFile(file),
        Name: file.name,
        Size: file.size,
        Carpeta: this.currentFolder.nombre,
        TipoCarpeta: this.currentFolder.tipo,
        Numero: this.group.numero,
        Curso: this.group.curso,
        Anio: this.group.anio,
        Periodo: this.group.periodo
      };
    } else {
      fileData = {
        FileData: await this.readFile(file),
        Name: file.name,
        Size: file.size,
        Carpeta: this.rootFolder.nombre,
        TipoCarpeta: this.rootFolder.tipo,
        Numero: this.group.numero,
        Curso: this.group.curso,
        Anio: this.group.anio,
        Periodo: this.group.periodo
      };
    }

    this.api.post(`https://localhost/api/Archivos/file`, fileData)
      .subscribe((success) => {
        if (this.inFolder) {
          this.loadFolderContents(this.currentFolder);
        } else {
          this.loadDocumentos();
        }
      }, (error) => {
        console.log('Could not upload file...');
        console.log(error);

      })
  }

  getPrettySize(size) {
    if (size < 1000) {
      return `${new Number(size).toFixed(1)} b`
    } else if (size < 100000) {
      return `${(size / 1000).toFixed(1)} kb`;
    } else if (size < 100000000) {
      return `${(size / 100000).toFixed(1)} mb`;
    }

    return 'TOO_BIG';
  }

  getPrettyDate(date: string) {
    return date.substring(0, date.indexOf('T'));
  }

  onCancelUpload() {
    console.log(' upload cancelled');
    $('#upload-file').val('');  
  }

  onCrearCarpeta() {
    console.log('crear carpeta');
    let nombre = $('input#folder-name').val() as string;

    if (nombre == '')
      return;
    
    //@ts-ignore
    $('#folder-modal').modal('hide');
    $('input#folder-name').val('');

    let data = {
      Nombre: nombre,
      Numero: this.group.numero,
      Curso: this.group.curso,
      Anio: this.group.anio,
      Periodo: this.group.periodo
    };

    this.api.post('https://localhost/api/Archivos/folder', data)
      .subscribe((success) => {
        if (this.inFolder) {
          this.loadFolderContents(this.currentFolder.id);
        } else {
          this.loadDocumentos();
        }
      }, (error) => {
        console.log(error);
        
      });

  }

  folderDoubleClicked(folder) {
    console.log('Double clicked');
    console.log(folder);
    this.inFolder = true;
    this.currentFolder = folder;
    this.archivos = [];
    this.carpetas = [];
    this.loadFolderContents(folder);
  }

  loadFolderContents(folder: any) {

    var query = new URLSearchParams(folder).toString();
    console.log(query);
     
    this.api.get(`https://localhost/api/Archivos/contents?${query}`)
      .subscribe((data: any[]) => {
        console.log(data);
        this.archivos = data;
      }, (error) => {
        console.log('Could not load folder contents');
        console.log(error);
      });
  }

  toHome() {
    this.inFolder = false;
    this.currentFolder = null;
    this.loadDocumentos();
  }

  onDescargarArchivo(file: any) {
    this.selected = file;
    this.onDescargar();
  }

  onDescargar() {
    console.log('descargando');
    var query = new URLSearchParams(this.selected).toString();
    window.location.replace(`https://localhost/api/Archivos/download?${query}`);
  }

  onModificar() {
    console.log('modificando');
    //@ts-ignore
    $('#modal').modal();
  }

  onEliminar() {
    console.log('eliminando');

    let folder = (this.selected.tamanio != undefined) ? 'file' : 'folder';

    let query = new URLSearchParams(this.selected).toString();

    this.api.delete(`https://localhost/api/Archivos/${folder}?${query}`).subscribe(
      (success) => {
        console.log('File or folder deleted');
        if (this.inFolder) {
          this.loadFolderContents(this.currentFolder);
        } else {
          this.loadDocumentos();
        }
      }, (error) => {
        console.log(error);
      }
    );
  }


  showContextMenu(item: any, event: any) {
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
