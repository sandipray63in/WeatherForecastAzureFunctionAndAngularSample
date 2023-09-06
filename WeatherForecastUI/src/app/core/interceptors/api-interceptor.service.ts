import { Injectable } from "@angular/core";
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
} from "@angular/common/http";
import { Observable } from "rxjs";
import { environment } from "../environments/environment";
import { default as config } from 'src/app/auth-config.json'

@Injectable({ providedIn: "root" })
export class ApiInterceptorService implements HttpInterceptor {
  weatherForecastAPIUrlUnderscoreValue: string = "__weatherForecastAPIUrl__";

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const apiReq = req.clone({ url: config.weatherForecastAPIUrl.value === this.weatherForecastAPIUrlUnderscoreValue ? config.weatherForecastAPIUrl.default : config.weatherForecastAPIUrl.value });
    return next.handle(apiReq);
  }
}
