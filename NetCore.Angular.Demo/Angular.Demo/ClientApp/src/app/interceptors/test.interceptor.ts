import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent, HttpResponse } from '@angular/common/http'
import { Observable } from "rxjs";
import { filter, map, mergeMap } from "rxjs/operators";

//https://blog.csdn.net/evanyanglibo/article/details/122368884
//https://www.jianshu.com/p/8b080a2587c2
//https://angular.io/guide/http-intercept-requests-and-responses
@Injectable()
export class TestInterceptor implements HttpInterceptor {
  constructor() { }

  //private header: string;

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    req = this.handleRequest(req);
    return next.handle(req).pipe(
      mergeMap(evt => this.handle(evt))
    );
  }


  /**
   * 请求参数拦截处理
   */
  handleRequest(req: any) {
    console.log("[Test Request] " + req);
    return req;
  }

  /**
   * 返回结果拦截处理
   */
  handle(evt: any) {
    return new Observable<HttpEvent<any>>(observer => {
      if (evt instanceof HttpResponse) {
        console.log("[Test Response] " + evt);
      }
      else
      {
        //console.log("[Test Request After] " + JSON.stringify(evt));
      }
      observer.next(evt);
    });
  }
}
