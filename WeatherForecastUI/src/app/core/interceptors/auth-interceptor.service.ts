import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptorService implements HttpInterceptor{

  intercept(req: HttpRequest<any>, next:HttpHandler): Observable<HttpEvent<any>>{
    const modifiedRequest = req.clone({
      headers: req.headers.append("auth_key","3faecab1-02e4-42c3-b7f0-11c74499cba5")
    });
    return next.handle(modifiedRequest);
  }
}
