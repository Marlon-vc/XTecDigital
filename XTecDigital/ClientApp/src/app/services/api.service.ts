import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { observable, Observable } from 'rxjs';

/* Este servicio se utiliza para solicitar, enviar y recibir datos del servidor
mediante solicitudes HTTP como GET, POST, PUT y DELETE */

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  public PORT = 5001;
  private options = {
    headers : {
      'Content-Type': 'application/json'
    }
  };

  constructor(protected http: HttpClient) { }

  /**
   * Solicitud HTTP POST
   * @param url url con el request para el api
   * @param body Objeto a enviar al api
   */
  post(url:string, body:Object) {
    // console.log('Creando...');
    return this.http.post(url, JSON.stringify(body), this.options);
  }

  /**
   * Solicitud HTTP GET
   * @param url url con el request para el api
   */
  get(url:string) {
    // console.log("Obteniendo...");
    return this.http.get(url, this.options);
  }

  /**
   * Solicitud HTTP PUT
   * @param url url con el request para el api
   * @param body Objeto a enviar al api
   */
  put(url:string, body:Object) {
    // console.log("Actualizando...");
    return this.http.put(url, JSON.stringify(body), this.options);
  }

  /**
   * Solicitud HTTP DELETE
   * @param url url con el request para el api
   */
  delete(url:string) {
    // console.log("Eliminando...")
    return this.http.delete(url, this.options);
  }

  getWithBody(url: string, body: object) {
    return this.sendRequest('GET', url, body);
  }

  deleteWithBody(url: string, body: object) {
    return this.sendRequest('DELETE', url, body);
  }

  putWithBody(url: string, body: object) {
    return this.sendRequest('PUT', url, body);
  }

  sendRequest(method: string, url: string, body: object) {
    return new Promise((resolve, reject) => {
      let req = new XMLHttpRequest();
      req.open(method, url, true);
      req.send(JSON.stringify(body));
      req.onreadystatechange = () => {
        if (req.status >= 200 && req.status < 300) {
          resolve(req.response);
        } else {
          reject(req.response);
        }
      }
    });
  }
}