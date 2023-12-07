import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';

@Component({
  selector: 'app-values-component',
  templateUrl: './values.component.html'
})
export class ValuesComponent {

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



interface Value {
  Name: string;
  Value: string;
}
