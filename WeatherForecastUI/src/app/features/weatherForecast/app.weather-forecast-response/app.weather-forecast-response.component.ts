import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AppWeatherForecastService } from '../../../core/services/app.weather-forecast-service';

@Component({
  selector: 'app-weather-forecast-response',
  templateUrl: './app.weather-forecast-response.component.html'
})
export class AppWeatherForecastResponseComponent implements OnInit, OnDestroy {

  private defaultHeaderRowHeightInPx: number = 20;
  private defaultRowHeightInPx: number = 35;
  gridHeightInPx: number = 0;
  defaultColDef = {
    sortable: true,
    filter: true
  };
  columnDefs = [
    { headerName: 'Date', field: 'dayDate' },
    { headerName: 'Low Temparature', field: 'dayLowTemperature' },
    { headerName: 'High Temperature', field: 'dayHighTemperature' },
    { headerName: 'Weather Messages', field: 'dayWeatherMessages', valueFormatter: this.dayWeatherMessagesFormatter, tooltipField: 'dayWeatherMessages' }
  ];
  rowData: any;
 

  constructor(private readonly appWeatherForecastService: AppWeatherForecastService) { }
      
  ngOnInit(): void {
      this.appWeatherForecastService.getSubjectAsObservable().subscribe(
      responseData => {
          this.rowData = responseData;
          if (this.rowData !== null) {
            this.gridHeightInPx = this.defaultHeaderRowHeightInPx + (this.rowData.length * this.defaultRowHeightInPx);
          }
      }
    )
  }

  ngOnDestroy(): void {
    this.appWeatherForecastService.unSubscribe();
  }

  dayWeatherMessagesFormatter(params : any) {
    return (params.value as string[]).join("\n");
  }
}
