import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent, HttpResponse } from '@angular/common/http'
import { Observable } from "rxjs";
import { mergeMap } from "rxjs/operators";

//https://blog.csdn.net/evanyanglibo/article/details/122368884
//https://www.jianshu.com/p/8b080a2587c2
//https://angular.io/guide/http-intercept-requests-and-responses
@Injectable()
export class LogInterceptor implements HttpInterceptor {

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    req = this.handleRequest(req);
    return next.handle(req).pipe(
      mergeMap(evt => this.handleResponse(evt))
    );
  }

  /**
   * 请求参数拦截处理
   */
  handleRequest(req: any) {
    console.log(`拦截器A在请求发起前的拦截处理`);
    return req;
  }

  /**
   * 返回结果拦截处理
   */
  handleResponse(evt: any) {
    return new Observable<HttpEvent<any>>(observer => {
      if (evt instanceof HttpResponse) {
        console.log("拦截器A在数据返回后的拦截处理");
      } else {
        console.log(`拦截器A接收到请求发出状态：${JSON.stringify(evt)}`);
      }
      observer.next(evt);
    });
  }
}
