import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptorService implements HttpInterceptor{

  intercept(req: HttpRequest<any>, next:HttpHandler){
    const modifiedRequest = req.clone({
      headers: req.headers.append("auth_key","3faecab1-02e4-42c3-b7f0-11c74499cba5")
    });
    return next.handle(modifiedRequest);
  }
}
