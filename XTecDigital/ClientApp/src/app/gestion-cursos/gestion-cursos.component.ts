import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-gestion-cursos',
  templateUrl: './gestion-cursos.component.html',
  styleUrls: ['./gestion-cursos.component.css']
})
export class GestionCursosComponent implements OnInit {

  cursos: any[];
  modificando: boolean;
  currentItem: any;

  constructor(private api: ApiService) { }

  ngOnInit(): void {
    this.loadCursos();
    this.modificando = false;

    $(window).on('click', (event) => {
      $('#context-menu').css('display', 'none');
    });
  }

  loadCursos() {
    console.log('loading courses');
    this.api.get(`https://localhost:5001/api/Cursos`)
      .subscribe((data: any[]) => {
        this.cursos = data;
      }, (error) => {
        console.log("Error loading courses...");
      });
  }

  onRightClick(item, event) {
    $('#context-menu').css({
      'display': 'block',
      'top': `${event.pageY}px`,
      'left': `${event.pageX}px`
    }).focus();
    this.currentItem = item;
    return false;
  }

  onModificar(event) {
    this.modificando = true;

    $('#action-title').text('Modificar');
    $('#submit-button').text('Guardar');
    $('#cancel-button').css('display', 'inline-block');

    $('#codigo').prop('readonly', true).val(this.currentItem.codigo);
    $('#nombre').val(this.currentItem.nombre);
    $('#carrera').val(this.currentItem.carrera);

    if (this.currentItem.habilitado) {
      $('#habilitado1').prop('checked', true);
    } else {
      $('#habilitado2').prop('checked', true);
    }
    return false;
  }

  onEliminar(event) {
    // @ts-ignore
    $('#delete-modal').modal('show');
  }

  onEliminarConfirm() {
    console.log("eliminando...");
    // @ts-ignore
    $('#delete-modal').modal('hide');
    this.api.delete(`https://localhost:5001/api/Cursos/${this.currentItem.codigo}`)
      .subscribe((success) => {
        this.showModal('Éxito', 'Curso eliminado correctamente.');
        this.loadCursos();
      }, (error) => {
        console.log(error);
        this.showModal('Error', 'Ocurrió un error al eliminar el curso seleccionado.');
      });
  }

  onCancelEdit(event) {
    this.modificando = false;
    $('#action-title').text('Crear curso');
    $('#submit-button').text('Crear');
    $('#cancel-button').css('display', 'none');
    $('#codigo').prop('readonly', false).val('');
    $('#nombre').val('');
    $('#carrera').val('');
    $('#habilitado1').prop('checked', true); 
  }

  onAction() {
    //TODO: validar entrada

    let codigo = $('#codigo').val();
    let nombre = $('#nombre').val();
    let carrera = $('#carrera').val();

    if (this.isEmpty(codigo as string) || this.isEmpty(nombre as string) || this.isEmpty(carrera as string)) {
      this.showModal('Error', 'Debes completar todos los campos');
      return;
    }

    var data = {
      Codigo: codigo,
      Nombre: nombre,
      Carrera: carrera,
      Habilitado: $('input[name=\"habilitado\"]:checked').val() === "1"
    };

    console.log(data);
    
    if (this.modificando) {
      this.api.put(`https://localhost:5001/api/Cursos/${data.Codigo}`, data)
        .subscribe((data) => {
          this.onCancelEdit(null);
          this.loadCursos();
          this.showModal('Éxito', 'Curso modificado correctamente');
        }, (error) => {
          console.log(error);
          this.showModal('Error', 'Ocurrió un error al modificar el curso');
        });
    } else {
      this.api.post(`https://localhost:5001/api/Cursos`, data)
        .subscribe((data) => {
          this.onCancelEdit(null);
          this.loadCursos();
          this.showModal('Éxito', 'Curso creado correctamente');
        }, (error) => {
          console.log(error);
          this.showModal('Error', 'Ocurrió un error al crear el curso');
        });
    }
  }

  isEmpty(str: string) {
    return str === null || str.match(/^ *$/) !== null;
  }

  showModal(title: string, body: string, okText: string = "Aceptar") {
    $('#modal-title').text(title);
    $('#modal-body').text(body);
    $('#ok-button').text(okText);
    // @ts-ignore
    $('#info-modal').modal('show');
  }

}
