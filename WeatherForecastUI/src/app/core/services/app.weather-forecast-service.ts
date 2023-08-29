import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { Observable, Subject } from 'rxjs';
import { AppSubjectService } from './app-subject.service';

@Injectable({
  providedIn: 'root'
})
export class AppWeatherForecastService extends AppSubjectService{
  constructor(private httpClient: HttpClient) {
      super();
  }

fetchWeatherForecastData(city: string, numberOfDaysToForecast: number, shouldIncludeToday: string) : Observable<any> {
    let queryParams = new HttpParams();
    queryParams = queryParams.append('city', city);
    queryParams = queryParams.append('numberOfDaysToForecast', numberOfDaysToForecast);
    queryParams = queryParams.append('shouldIncludeToday', shouldIncludeToday);
    return this.httpClient.get("", {
      params: queryParams
    });
  }
}
