import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { default as config } from 'src/app/auth-config.json'

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptorService implements HttpInterceptor{

  authKeyUnderscoreValue: string = "__authKey__";

  intercept(req: HttpRequest<any>, next:HttpHandler): Observable<HttpEvent<any>>{
    const modifiedRequest = req.clone({
      headers: req.headers.append("auth_key", config.authKey.value === this.authKeyUnderscoreValue ? config.authKey.default : config.authKey.value)
    });
    return next.handle(modifiedRequest);
  }
}
