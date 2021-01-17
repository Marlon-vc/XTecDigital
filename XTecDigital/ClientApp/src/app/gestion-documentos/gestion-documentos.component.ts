import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-gestion-documentos',
  templateUrl: './gestion-documentos.component.html',
  styleUrls: ['./gestion-documentos.component.css']
})
export class GestionDocumentosComponent implements OnInit {

  userType = window.localStorage.getItem('user-type');
  selected: any;
  inFolder: boolean;
  currentFolder: any;
  rootFolder: any;
  groupId: number;

  archivos: any[];
  carpetas: any[];

  constructor(private route: ActivatedRoute, private api: ApiService) { }

  ngOnInit(): void {
    this.groupId = this.route.snapshot.params.id;

    $(window).on('click', () => {
      $('#context-menu').css('display', 'none');
    });

    this.loadDocumentos();
  }

  loadDocumentos() {
    this.api.get(`https://localhost/api/Archivos/root/${this.groupId}`)
      .subscribe((data: any) => {
        console.log(data);
        this.carpetas = data.folders;
        this.archivos = data.files;
        this.rootFolder = data.root;
      }, (error) => {
        console.log('Error retrieving files');
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


    let folderId;
    if (this.currentFolder != null) {
      folderId = this.currentFolder.id;
    } else {
      folderId = this.rootFolder.id;
    }

    console.log(`Current folder Id: ${folderId}`);

    var fileData = {
      FileData: await this.readFile(file),
      Name: file.name,
      Size: file.size,
      FolderId: folderId,
      GroupId: new Number(this.groupId)
    };

    this.api.post(`https://localhost/api/Archivos/file`, fileData)
      .subscribe((success) => {
        if (this.inFolder) {
          this.loadFolderContents(this.currentFolder.id);
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
      GroupId: new Number(this.groupId),
      Name: nombre
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
    this.loadFolderContents(folder.id);
  }

  loadFolderContents(id: number) {
    this.api.get(`https://localhost/api/Archivos/folder/${id}`)
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

  fileDoubleClicked(file, event) {
    console.log(file);
    this.api.get(`https://localhost/api/Archivos/file/${file.id}`)
      .subscribe((data) => {
        console.log(data);
        
      }, (error) => {
        console.log('Error downloading file');
        console.log(error);
      });
  }

  onDescargar(event) {
    console.log('descargando');

    window.location.replace(`https://localhost/api/Archivos/file/${this.selected.id}`);
    return;
  }

  onModificar(event) {
    console.log('modificando');
    //@ts-ignore
    $('#modal').modal();
  }

  onEliminar(event) {
    console.log('eliminando');

    let folder = (this.selected.raiz != undefined) ? 'folder' : 'file';

    let info = new FormData();
    info.append('FileId', this.selected.id);
    info.append('GroupId', `${this.groupId}`);
    
    this.api.delete(`https://localhost/api/Archivos/${folder}/${this.selected.id}`)
      .subscribe((success) => {
        console.log('File deleted!');

        if (this.inFolder) {
          this.loadFolderContents(this.currentFolder.id);
        } else {
          this.loadDocumentos();
        }
      }, (error) => {
        console.log(error);
        
      });
    
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
