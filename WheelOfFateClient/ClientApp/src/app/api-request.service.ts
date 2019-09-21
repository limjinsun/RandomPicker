import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class ApiRequestService {
  http: HttpClient;
  apiUrl: string;

  constructor(http: HttpClient) {
    this.http = http;
    this.apiUrl = environment.apiUrl;
  }

  makeGetRequest() {
    return this.http.get(this.apiUrl + '/rota');
  }

  makeEngineerInfoGetRequest(engineerId : number) {
    return this.http.get(this.apiUrl + '/rota/' + engineerId );
  }

}
