import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-initialize-excel',
  templateUrl: './initialize-excel.component.html',
  styleUrls: ['./initialize-excel.component.css']
})
export class InitializeExcelComponent implements OnInit {

  localUrl: any[];

  constructor(private api: ApiService) { }

  ngOnInit(): void { }

  
  /**
   * Metodo para cargar una imagen
   * @param event 
   */
  load(event:any) {
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();
      reader.onload = (event: any) => {
          this.localUrl = event.target.result;
      }
      reader.readAsDataURL(event.target.files[0]);
  }
}

  /**
   * Metodo para inicializar semestre
   */
  upload() {
    
    var excelData = this.localUrl.toString();

    this.api.post(`https://localhost/api/Semestres/file`, excelData)
      .subscribe((data: any) => {
        console.log("Semestre creado correctamente")
        this.showModal();
      }, (error) => {
        console.log("Error creating semester...");
        console.log(error);
      });
  }

  /**
   * Metodo para mostrar un modal
   * @param title Titulo de la modal
   * @param body Cuerpo de la modal
   * @param okText Texto del boton
   */
  showModal() {
    // @ts-ignore
    $('#confirm-modal').modal('show');
  }

}
