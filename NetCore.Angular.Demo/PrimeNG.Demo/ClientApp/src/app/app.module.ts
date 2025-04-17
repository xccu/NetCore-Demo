import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { InputSwitchModule } from 'primeng/inputswitch';
import { PanelModule } from 'primeng/panel';
import { MenuModule } from 'primeng/menu';
import { PanelMenuModule } from 'primeng/panelmenu';
import { ButtonModule } from 'primeng/button';
import { MenubarModule } from 'primeng/menubar';
import { StyleClassModule } from 'primeng/styleclass';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { InputSwitchComponent } from './samples/input-switch/input-switch.component';
import { InputTextComponent } from './samples/input-text/input-text.component';
import { PanelComponent } from './samples/panel/panel.component';
import { MenuComponent } from './samples/menu/menu.component';
import { MenuBarComponent } from './samples/menu-bar/menu-bar.component';
import { ButtonComponent } from './samples/button/button.component';
import { LogInterceptor } from './helpers/log.interceptor';
import { PanelMenuComponent } from './samples/panel-menu/panel-menu.component';



@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    InputSwitchComponent,
    InputTextComponent,
    PanelComponent,
    MenuComponent,
    PanelMenuComponent,
    MenuBarComponent,
    ButtonComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    InputSwitchModule,
    PanelModule,
    BrowserAnimationsModule,
    MenuModule,
    PanelMenuModule,
    MenubarModule,
    ButtonModule,
    StyleClassModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'input-switch', component: InputSwitchComponent },
      { path: 'input-text', component: InputTextComponent },
      { path: 'panel', component: PanelComponent },
      { path: 'menu', component: MenuComponent },
      { path: 'panel-menu', component: PanelMenuComponent },
      { path: 'menu-bar', component: MenuBarComponent },
      { path: 'button', component: ButtonComponent }
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: LogInterceptor, multi: true } // LogInterceptor 注册语句
  ],   
  bootstrap: [AppComponent]
})
export class AppModule { }
