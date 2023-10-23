import { Component } from '@angular/core';
import { MessageService } from 'primeng/api';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  providers: [MessageService]
})
export class HomeComponent {
  items: MenuItem[];

  constructor(private messageService: MessageService) { }

  ngOnInit() {
    this.items = [
      {
        label: 'test',
        items: []
      },
    ];
  }
}
