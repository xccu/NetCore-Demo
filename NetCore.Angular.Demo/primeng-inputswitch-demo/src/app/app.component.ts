import { Component } from '@angular/core';
import { MenuItem} from 'primeng/api';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent { 
    checked1: boolean = false;

    checked2: boolean = true;
}
