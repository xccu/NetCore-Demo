import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-menu',
  templateUrl: './panel-menu.component.html',
  styleUrls: ['./panel-menu.component.css']
})
export class PanelMenuComponent implements OnInit {

  items: MenuItem[];
  public message: string;
  ngOnInit() {
    this.items = [
      {
        label: 'File',
        icon: 'pi pi-fw pi-file',
        command: () => this.handleMenuClick('File'),
        items: [
          {
            label: 'New',
            icon: 'pi pi-fw pi-plus',
            command: () => this.handleMenuClick('New'),
            items: [
              {
                label: 'Bookmark',
                icon: 'pi pi-fw pi-bookmark',
                command: () => this.handleMenuClick('Bookmark')
              },
              {
                label: 'Video',
                icon: 'pi pi-fw pi-video',
                command: () => this.handleMenuClick('Video')
              }
            ]
          },
          {
            label: 'Delete',
            icon: 'pi pi-fw pi-trash',
            command: () => this.handleMenuClick('Delete')
          },
          //{
          //  separator: true
          //},
          {
            label: 'Export',
            icon: 'pi pi-fw pi-external-link',
            command: () => this.handleMenuClick('Export')
          }
        ]
      },
      {
        label: 'Edit',
        icon: 'pi pi-fw pi-pencil',
        command: () => this.handleMenuClick('Edit'),
        items: [
          {
            label: 'Left',
            icon: 'pi pi-fw pi-align-left',
            command: () => this.handleMenuClick('Left')
          },
          {
            label: 'Right',
            icon: 'pi pi-fw pi-align-right',
            command: () => this.handleMenuClick('Right')
          },
          {
            label: 'Center',
            icon: 'pi pi-fw pi-align-center',
            command: () => this.handleMenuClick('Center')
          },
          {
            label: 'Justify',
            icon: 'pi pi-fw pi-align-justify',
            command: () => this.handleMenuClick('Justify')
          }
        ]
      },
      {
        label: 'Users',
        icon: 'pi pi-fw pi-user',
        command: () => this.handleMenuClick('Users'),
        items: [
          {
            label: 'New',
            icon: 'pi pi-fw pi-user-plus',
            command: () => this.handleMenuClick('New')
          },
          {
            label: 'Delete',
            icon: 'pi pi-fw pi-user-minus',
            command: () => this.handleMenuClick('Delete')
          },
          {
            label: 'Search',
            icon: 'pi pi-fw pi-users',
            command: () => this.handleMenuClick('Search'),
            items: [
              {
                label: 'Filter',
                icon: 'pi pi-fw pi-filter',
                command: () => this.handleMenuClick('Filter'),
                items: [
                  {
                    label: 'Print',
                    icon: 'pi pi-fw pi-print',
                    command: () => this.handleMenuClick('Print')
                  }
                ]
              },
              {
                icon: 'pi pi-fw pi-bars',
                label: 'List',
                command: () => this.handleMenuClick('List')
              }
            ]
          }
        ]
      },
      {
        label: 'Events',
        icon: 'pi pi-fw pi-calendar',
        command: () => this.handleMenuClick('Calendar'),
        items: [
          {
            label: 'Edit',
            icon: 'pi pi-fw pi-pencil',
            command: () => this.handleMenuClick('Edit'),
            items: [
              {
                label: 'Save',
                icon: 'pi pi-fw pi-calendar-plus',
                command: () => this.handleMenuClick('Save')
              },
              {
                label: 'Delete',
                icon: 'pi pi-fw pi-calendar-minus',
                command: () => this.handleMenuClick('Delete')
              }
            ]
          },
          {
            label: 'Archieve',
            icon: 'pi pi-fw pi-calendar-times',
            command: () => this.handleMenuClick('Archieve'),
            items: [
              {
                label: 'Remove',
                icon: 'pi pi-fw pi-calendar-minus',
                command: () => this.handleMenuClick('Remove')
              }
            ]
          }
        ]
      },
      {
        label: 'Quit',
        icon: 'pi pi-fw pi-power-off',
        command: () => this.handleMenuClick('Quit')
      }
    ];
  }

  public handleMenuClick(message:string) {
    this.message=message;
  }

}
