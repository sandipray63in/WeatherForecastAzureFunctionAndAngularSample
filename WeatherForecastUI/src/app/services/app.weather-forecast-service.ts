import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { Observable, Subject, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AppWeatherForecastService {
  weatherFoecastResponseSubject = new Subject();

  constructor(private httpClient: HttpClient) { }

  fetchWeatherForecastData(city: string, numberOfDaysToForecast: number, shouldIncludeToday: string) {
    let queryParams = new HttpParams();
    queryParams = queryParams.append('city', city);
    queryParams = queryParams.append('numberOfDaysToForecast', numberOfDaysToForecast);
    queryParams = queryParams.append('shouldIncludeToday', shouldIncludeToday);
    return this.httpClient.get(environment.Weather_Forecast_API_URL, {
      params: queryParams
    }).subscribe(
      {
        next: (responseData) => {
          this.weatherFoecastResponseSubject.next({hasError: false, responseData});
        },
        error: (err: any) => {
          this.weatherFoecastResponseSubject.next({hasError: true, err});
        },
        complete: () => { }
      }
    );
  }

  getWeatherFoecastResponseObservable(): Observable<any> {
    return this.weatherFoecastResponseSubject.asObservable();
  }
}
