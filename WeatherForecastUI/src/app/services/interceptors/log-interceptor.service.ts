import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEventType } from '@angular/common/http';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LogInterceptorService {

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    console.log('Outgoing Request');
    console.log(req.url);
    return next.handle(req).pipe(
      tap(event => {
        if (event.type === HttpEventType.Response) {
          console.log('Incoming Response');
          console.log(event.body);
        }
      })
    );
  }
}
