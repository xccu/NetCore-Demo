import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { Value } from '../../models/value.model';

@Component({
  selector: 'app-api-demo-component',
  templateUrl: './api-demo.component.html',
  styleUrls: ['./api-demo.component.css']
})
export class ApiDemoComponent {

  public values: string | undefined;
  private baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public getValues() {
    this.http.get<Value[]>(this.baseUrl + '/api/values').subscribe(result => {
      this.values = JSON.stringify(result);
    }, error => console.error(error));
  }

  public postValues() {
    this.http.post<any>(this.baseUrl + "/api/values", new Value(3,"Name3", "Value3")).subscribe(result => {
      this.values = JSON.stringify(result);
    }, error => console.error(error));  
  }

  public putValues() {
    var headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    this.http.put<any>(this.baseUrl + "/api/values/3", "\"123\"", {
      headers: headers
    }).subscribe(result => {
      this.values = JSON.stringify(result);
    }, error => console.error(error));  
  }

  public deleteValues() {
    this.http.delete<any>(this.baseUrl + '/api/values/3').subscribe(result => {
      this.values = JSON.stringify(result);
    }, error => console.error(error));
  }

  public getError() {
    this.http.get<Value[]>(this.baseUrl + '/api/values/error').subscribe(result => {
      this.values = JSON.stringify(result);
    }, err => {
      //console.error(err.error);
      if (err && err.error) {
        this.values = err.error;
      }
      else {
        this.values = err;
      }
    });
  }
}
