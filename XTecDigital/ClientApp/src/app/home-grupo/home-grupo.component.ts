import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SessionHandler } from '../helpers/sessionHandler';

@Component({
  selector: 'app-home-grupo',
  templateUrl: './home-grupo.component.html',
  styleUrls: ['./home-grupo.component.css']
})
export class HomeGrupoComponent implements OnInit {

  links: any[];

  constructor(private route: ActivatedRoute, private router: Router) { }

  //obtener el id del grupo
  groupId = 1;
  userType: string;

  ngOnInit(): void {
    this.userType = SessionHandler.getUserType();
    // let groupId = this.route.snapshot.params.id
  //   let userType = SessionHandler.getUserType();
    
  //   if (userType == null) {
  //     this.router.navigate['/']
  //     console.log('Error');
  //     return;
  //   }

  //   this.links = [
  //     {
  //       target: `/documentos/${groupId}`,
  //       title: 'Documentos'
  //     },
  //     {
  //       target: `/evaluaciones/${groupId}`,
  //       title: 'Evaluaciones'
  //     },
  //     {
  //       target: `/noticias/${groupId}`,
  //       title: 'Noticias'
  //     },
  //     {
  //       target: `/estudiantes/${groupId}`,
  //       title: 'Lista de Estudiantes'
  //     },
  //   ]; 

  }

  /**
   * Metodo para cerrar sesion
   */
  onLogout() {
    console.log('logging out..');
    window.localStorage.clear();
    this.router.navigate(['']);
  }

}
