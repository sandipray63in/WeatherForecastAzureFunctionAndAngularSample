import { Component, OnDestroy, OnInit } from '@angular/core';
import { AppWeatherForecastService } from '../services/app.weather-forecast-service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-weather-forecast-response',
  templateUrl: './app.weather-forecast-response.component.html'
})
export class AppWeatherForecastResponseComponent implements OnInit, OnDestroy {

  private defaultRowHeightInPx: number = 20;
  gridHeightInPx: number = 0;
  errorMessage: string | null = null;
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
    this.weatherForecastSubscription = this.appWeatherForecastService.getWeatherFoecastResponseObservable().subscribe(
      responseData => {
        if (responseData.hasError) {
          this.errorMessage = responseData.err; 
        } else {
          this.rowData = responseData.responseData;
          this.gridHeightInPx = this.defaultRowHeightInPx + (this.rowData.length * this.defaultRowHeightInPx);
        }
      }
    )
  }

  ngOnDestroy(): void {
    this.weatherForecastSubscription.unsubscribe();
  }
}
