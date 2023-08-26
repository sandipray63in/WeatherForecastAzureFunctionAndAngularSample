import { Component, OnDestroy, OnInit } from '@angular/core';
import { AppWeatherForecastService } from '../services/app.weather-forecast-service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-weather-forecast-response',
  templateUrl: './app.weather-forecast-response.component.html'
})
export class AppWeatherForecastResponseComponent implements OnInit, OnDestroy {
  defaultColDef = {
    sortable: true,
    filter: true
  };
  columnDefs = [
    { headerName: 'Date', field: 'dayDate' },
    { headerName: 'Low Temparature', field: 'dayLowTemperature' },
    { headerName: 'High Temperature', field: 'dayHighTemperature' },
    { headerName: 'Weather Messages', field: 'dayWeatherMessages' }
  ];
  rowData: any;
  weatherForecastSubscription!: Subscription;
  constructor(private appWeatherForecastService: AppWeatherForecastService) { }
      
  ngOnInit(): void {
    this.appWeatherForecastService.getWeatherFoecastResponseObservable().subscribe(
      responseData => {
        console.log(responseData);
        this.rowData = responseData;
      }
    )
  }

  ngOnDestroy(): void {
    this.weatherForecastSubscription.unsubscribe();
  }
}
