import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ApiDemoComponent } from './samples/api-demo/api-demo.component';
import { RxjsDemoComponent } from './samples/rxjs-demo/rxjs-demo.component';

import { LogInterceptor } from './interceptors/log.interceptor';
import { TestInterceptor } from './interceptors/test.interceptor';
import { ErrorInterceptor } from './interceptors/error.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    ApiDemoComponent,
    RxjsDemoComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'api-demo', component: ApiDemoComponent },
      { path: 'rxjs-demo', component: RxjsDemoComponent },
    ])
  ],
  providers: [
    // Interceptor 注册语句
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }, 
    { provide: HTTP_INTERCEPTORS, useClass: LogInterceptor, multi: true }, 
    { provide: HTTP_INTERCEPTORS, useClass: TestInterceptor, multi: true } 
  ], 
  bootstrap: [AppComponent]
})
export class AppModule { }
