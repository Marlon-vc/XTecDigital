import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SessionHandler } from '../helpers/sessionHandler';
import { ApiService } from '../services/api.service';
import {  } from 'jquery';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private router: Router, private api: ApiService) { }

  ngOnInit(): void {
        if(SessionHandler.isLoggedIn()) {
          //Ir a página de inicio
          this.router.navigate(['home']);
        }
  }

  logIn() {
    console.log('logging in...');
    
    var user = $('#user').val();
    var pass = $('#pass').val();

    var auth = {
      user: user,
      pass: pass
    };

    this.api.post(`https://localhost:${this.api.PORT}/api/Login`, auth)
    .subscribe((data) => {
        console.log('success');
        
      }, (error) => {
        console.log('error logging in');
        
      });
  }
}
